using System.Collections.Concurrent;
using System.Diagnostics.Contracts;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using TaskManager.Common.Entities;
using TaskManager.Common.Interfaces;
using TaskManager.Web.Models;

namespace TaskManager.Web.Hubs
{
    //[Authorize]
    public class TasksHub : Hub
    {
        private static object _locker = new object();
        private static ITaskService _taskService;
        private static readonly ConcurrentDictionary<string, ApplicationUser> _userConnectionsDictionary = new ConcurrentDictionary<string, ApplicationUser>();

        public TasksHub(ITaskService taskService)
        {
            Contract.Requires(taskService != null);

            TaskService = taskService;
        }

        private static ITaskService TaskService
        {
            get { return _taskService; }
            set
            {
                if (_taskService == null)
                {
                    lock (_locker)
                    {
                        if (_taskService == null)
                        {
                            _taskService = value;
                            _taskService.TaskChanged += OnTaskChanged;
                        }
                    }
                }
            }
        }

        private static void OnTaskChanged(object sender, TaskChangedEventArgs taskChangedEventArgs)
        {
            SendTaskChanged(taskChangedEventArgs, GlobalHost.ConnectionManager.GetHubContext<TasksHub>().Clients);
        }

        public static void SendTaskChanged(TaskChangedEventArgs taskChangedDetails, IHubConnectionContext<dynamic> connectionContext)
        {
            Contract.Requires(taskChangedDetails != null);
            Contract.Requires(connectionContext != null);

            foreach (string key in _userConnectionsDictionary.Keys)
            {
                ApplicationUser user = _userConnectionsDictionary[key];
                if (user != null && taskChangedDetails.OwnerUserId == user.Id)
                {
                    connectionContext.Client(key).taskUpdated(new TaskChangeModel(taskChangedDetails));
                }
            }
        }

        public override async Task OnConnected()
        {
            IPrincipal user = Context.User;
            if (user == null || user.Identity == null)
            {
                throw new NotAuthorizedException("Пользователь не имеет права доступа к системе");
            }

            ApplicationUserManager userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser appUser = await userManager.FindByNameAsync(user.Identity.Name);

            if (appUser == null)
            {
                throw new NotAuthorizedException("Пользователь не зарегистрирован в системе");
            }
            
            _userConnectionsDictionary[Context.ConnectionId] = appUser;

            await base.OnConnected();
        }

        /// <summary>
        /// Called when a connection disconnects from this hub gracefully or due to a timeout.
        /// </summary>
        /// <param name="stopCalled">true, if stop was called on the client closing the connection gracefully;
        ///             false, if the connection has been lost for longer than the
        ///             <see cref="P:Microsoft.AspNet.SignalR.Configuration.IConfigurationManager.DisconnectTimeout"/>.
        ///             Timeouts can be caused by clients reconnecting to another SignalR server in scaleout.
        ///             </param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task"/>
        /// </returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            ApplicationUser removedUser = null;
            _userConnectionsDictionary.TryRemove(Context.ConnectionId, out removedUser);
            return base.OnDisconnected(stopCalled);
        }
    }
}