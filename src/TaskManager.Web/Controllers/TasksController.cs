using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TaskManager.Common.Entities;
using TaskManager.Common.Interfaces;
using TaskManager.Web.Models;
using TaskManager.Web.Properties;

namespace TaskManager.Web.Controllers
{
    [Authorize]
    public class TasksController : TaskManagerApiControllerBase
    {
        private readonly ITaskService taskService;

        public TasksController(ITaskService taskService)
        {
            Contract.Requires(taskService != null);

            this.taskService = taskService;
        }

        // GET api/<controller>
        public async Task<HttpResponseMessage> Get()
        {
            ApplicationUser currentUser = await GetCurrentUser();
            if (currentUser == null)
                return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "");
            UserTask[] tasks = await this.taskService.GetAllUserTasksAsync(currentUser.Id);
            return this.Request.CreateResponse(tasks.Select(t => new TaskListItemModel(t)));
        }

        // GET api/<controller>/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            UserTask task = await this.taskService.GetTaskByIdAsync(id);
            if (task == null)
                return this.Request.CreateErrorResponse(HttpStatusCode.NotFound, Resources.TaskNotFound);
            return this.Request.CreateResponse(new TaskModel(task));
        }

        // POST api/<controller>
        public async Task<HttpResponseMessage> Post([FromBody] TaskModel task)
        {
            if (task == null)
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.DataNotSet);
            int id = await this.taskService.AddTaskAsync(task.ToUserTask(await GetCurrentUser()));
            return this.Request.CreateResponse(id);
        }

        // PUT api/<controller>/5
        public async Task<HttpResponseMessage> Put([FromBody]TaskModel task)
        {
            if (task == null)
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.DataNotSet);

            bool result = await this.taskService.UpdateTaskAsync(task.ToUserTask(await GetCurrentUser()));
            if (!result)
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Resources.DataSaveError);

            return this.Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/<controller>/5
        public async Task<HttpResponseMessage> Delete(int id)
        {
            ApplicationUser currentUser = await GetCurrentUser();
            bool result = await this.taskService.DeleteTaskAsync(id, currentUser.Id);
            if (!result)
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Resources.DataRemoveError);

            return this.Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}