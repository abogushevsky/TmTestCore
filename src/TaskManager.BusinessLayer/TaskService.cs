using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using TaskManager.Common.Entities;
using TaskManager.Common.Interfaces;
using TaskManager.DataLayer.Common.Filters;
using TaskManager.DataLayer.Common.Interfaces;

namespace TaskManager.BusinessLayer
{
    public class TaskService : EntityServiceBase<UserTask, int>, ITaskService
    {
        private readonly IFilteredRepository<UserTask, TasksByUserFilter> tasksByUserFilter;
        private readonly IFilteredRepository<UserTask, TasksByCategoryFilter> tasksByCategoryFilter;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="repository">Репозитория для сущностей типа <see cref="UserTask"/></param>
        /// <param name="tasksByUserFilter">Фильтр по пользователям</param>
        /// <param name="tasksByCategoryFilter">Фильтр по категориям</param>
        public TaskService(
            IRepository<UserTask, int> repository,
            IFilteredRepository<UserTask, TasksByUserFilter> tasksByUserFilter,
            IFilteredRepository<UserTask, TasksByCategoryFilter> tasksByCategoryFilter) : base(repository)
        {
            Contract.Requires(repository != null);
            Contract.Requires(tasksByUserFilter != null);
            Contract.Requires(tasksByCategoryFilter != null);

            this.tasksByUserFilter = tasksByUserFilter;
            this.tasksByCategoryFilter = tasksByCategoryFilter;
        }

        public event EventHandler<TaskChangedEventArgs> TaskChanged;

        /// <summary>
        /// Получение всех задач пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Найденные задачи пользователя, или пустой массив</returns>
        public async Task<UserTask[]> GetAllUserTasksAsync(string userId)
        {
            return await ExecAsync(() => this.tasksByUserFilter.FilterAsync(new TasksByUserFilter(userId)));
        }

        /// <summary>
        /// Получение всех задач по категории
        /// </summary>
        /// <param name="categoryId">Идентификатор категории</param>
        /// <returns>Найденные задачи по категории, или пустой массив</returns>
        public async Task<UserTask[]> GetTasksByCategoryAsync(int categoryId)
        {
            return await ExecAsync(() => this.tasksByCategoryFilter.FilterAsync(new TasksByCategoryFilter(categoryId)));
        }

        /// <summary>
        /// Получение задачи по id
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <returns>Найденная задача или null</returns>
        public async Task<UserTask> GetTaskByIdAsync(int id)
        {
            return await ExecOnRepositoryAsync(r => r.GetByIdAsync(id));
        }

        /// <summary>
        /// Добавление новой задачи
        /// </summary>
        /// <param name="task">Новая задача</param>
        /// <returns>Идентификатор добавленной задачи</returns>
        public async Task<int> AddTaskAsync(UserTask task)
        {
            int result = await ExecOnRepositoryAsync(r => r.CreateAsync(task));
            OnTaskChanged(new TaskChangedEventArgs(task, task.User.Id, ChangeTypes.Added));
            return result;
        }

        /// <summary>
        /// Редактирование задачи
        /// </summary>
        /// <param name="task">Измененная задача</param>
        /// <returns>Признак успеха операции</returns>
        public async Task<bool> UpdateTaskAsync(UserTask task)
        {
            bool result = await ExecOnRepositoryAsync(r => r.UpdateAsync(task));
            OnTaskChanged(new TaskChangedEventArgs(task, task.User.Id, ChangeTypes.Edited));
            return result;
        }

        /// <summary>
        /// Удаление задачи
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <param name="userId">Идентификатор пользователя-владельца задачи</param>
        /// <returns>Призак успеха операции</returns>
        public async Task<bool> DeleteTaskAsync(int id, string userId)
        {
            bool result = await ExecOnRepositoryAsync(r => r.DeleteAsync(id));
            OnTaskChanged(new TaskChangedEventArgs(new UserTask() {Id = id}, userId, ChangeTypes.Deleted));
            return result;
        }

        protected virtual void OnTaskChanged(TaskChangedEventArgs e)
        {
            if (TaskChanged != null)
                TaskChanged(this, e);
        }
    }
}