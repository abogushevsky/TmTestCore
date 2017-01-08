using System.Data;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using TaskManager.BusinessLayer;
using TaskManager.Common.Entities;
using TaskManager.Common.Interfaces;
using TaskManager.DataLayer.Common.Filters;
using TaskManager.DataLayer.Common.Interfaces;
using TaskManager.DataLayer.MsSql;
using TaskManager.DataLayer.MsSql.Dto;
using TaskManager.DataLayer.MsSql.Specialized;
using TaskManager.Web.Controllers;
using TaskManager.Web.Hubs;

namespace TaskManager.Web
{
    /// <summary>
    /// Вспомогательный класс для инициализации контейнера внедрения зависимостей на основе Simple Injector
    /// http://simpleinjector.codeplex.com/documentation
    /// </summary>
    public static class SimpleInjectorWebApiInitializer
    {
        private const string CONNECTION_STRING_NAME = "DefaultConnection";

        private static Container _container;

        public static void Initialize()
        {
            if (_container != null)
                return;
            _container = new Container();
            _container.Options.AllowOverridingRegistrations = true;

            InitSignalR();
            InitConverters();
            InitRepositories();
            InitEntityServices();

            _container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            _container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(_container);
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(_container));

            _container.Register<AccountController>(() => new AccountController());
        }

        private static void InitConverters()
        {
            _container.Register<IEntityDtoConverter<Category, CategoryDto>>(() => new CategoryConverter());
            _container.Register<IEntityDtoConverter<UserTask, UserTaskDto>>(() => new UserTaskConverter());
            //_container.Register(typeof(IEntityDtoConverter<,>), new[] { Assembly.Load("TaskManager.DataLayer.MsSql") }, Lifestyle.Singleton);
        }

        private static void InitRepositories()
        {
            //Названия хранимых процедур, используемых слоем DAL собраны в одном месте.  
            //Их можно вынести в какое-нибудь внешнее хранилище, или в конфигурационный файл.
            CrudCommandsBundle categoryCommandsBundle = new CrudCommandsBundle()
            {
                GetAllCommand = new SqlCommandInfo("sp_GetAllCategories", CommandType.StoredProcedure),
                GetByIdCommand = new SqlCommandInfo("sp_GetCategoryById", CommandType.StoredProcedure),
                CreateCommand = new SqlCommandInfo("sp_CreateCategory", CommandType.StoredProcedure),
                UpdateCommand = new SqlCommandInfo("sp_UpdateCategory", CommandType.StoredProcedure),
                DeleteCommand = new SqlCommandInfo("sp_DeleteCategory", CommandType.StoredProcedure)
            };

            CrudCommandsBundle taskCommandsBundle = new CrudCommandsBundle()
            {
                GetAllCommand = new SqlCommandInfo("sp_GetAllTasks", CommandType.StoredProcedure),
                GetByIdCommand = new SqlCommandInfo("sp_GeTaskById", CommandType.StoredProcedure),
                CreateCommand = new SqlCommandInfo("sp_CreateTask", CommandType.StoredProcedure),
                UpdateCommand = new SqlCommandInfo("sp_UpdateTask", CommandType.StoredProcedure),
                DeleteCommand = new SqlCommandInfo("sp_DeleteTask", CommandType.StoredProcedure)
            };
            
            _container.Register<IRepository<Category, int>>(() => new CrudSqlRepository<Category, int, CategoryDto>(
                Resolve<IEntityDtoConverter<Category, CategoryDto>>(), 
                categoryCommandsBundle, 
                CONNECTION_STRING_NAME), Lifestyle.Singleton);
            _container.Register<IRepository<UserTask, int>>(() => new CrudSqlRepository<UserTask, int, UserTaskDto>(
                Resolve<IEntityDtoConverter<UserTask, UserTaskDto>>(), 
                taskCommandsBundle, 
                CONNECTION_STRING_NAME), Lifestyle.Singleton);

            _container.Register<IFilteredRepository<Category, CategoriesByUserFilter>>(() => new SqlFilteredRepository<Category, CategoryDto, CategoriesByUserFilter>(
                new SqlCommandInfo("sp_GetUserCategories", CommandType.StoredProcedure), 
                CONNECTION_STRING_NAME,
                Resolve<IEntityDtoConverter<Category, CategoryDto>>()), Lifestyle.Singleton);

            _container.Register<IFilteredRepository<UserTask, TasksByUserFilter>>(() => new SqlFilteredRepository<UserTask, UserTaskDto, TasksByUserFilter>(
                new SqlCommandInfo("sp_GetUserTasks", CommandType.StoredProcedure),
                CONNECTION_STRING_NAME,
                Resolve<IEntityDtoConverter<UserTask, UserTaskDto>>()), Lifestyle.Singleton);
            _container.Register<IFilteredRepository<UserTask, TasksByCategoryFilter>>(() => new SqlFilteredRepository<UserTask, UserTaskDto, TasksByCategoryFilter>(
                new SqlCommandInfo("sp_GetTasksByCategory", CommandType.StoredProcedure), 
                CONNECTION_STRING_NAME,
                Resolve<IEntityDtoConverter<UserTask, UserTaskDto>>()), Lifestyle.Singleton);
        }

        private static void InitEntityServices()
        {
            _container.Register<ICategoriesService>(() => new CategoriesService(
                Resolve<IRepository<Category, int>>(), 
                Resolve<IFilteredRepository<Category, CategoriesByUserFilter>>()), Lifestyle.Singleton);
            _container.Register<ITaskService>(() => new TaskService(
                Resolve<IRepository<UserTask, int>>(), 
                Resolve<IFilteredRepository<UserTask, TasksByUserFilter>>(),
                Resolve<IFilteredRepository<UserTask, TasksByCategoryFilter>>()), Lifestyle.Singleton);
        }

        private static void InitSignalR()
        {
            _container.Register<Microsoft.AspNet.SignalR.IDependencyResolver>(() => new SignalRSimpleInjectorDependencyResolver(_container));
            _container.Register<TasksHub>();
        }

        public static T Resolve<T>() where T : class
        {
            return _container.GetInstance<T>();
        }
    }
}