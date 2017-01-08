using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using TaskManager.Common.Entities;

namespace TaskManager.Common.Interfaces
{
    /// <summary>
    /// Интерфейс описывает сервис для работы с задачами
    /// </summary>
    [ContractClass(typeof(ITaskServiceContracts))]
    public interface ITaskService
    {
        /// <summary>
        /// Событие, возникающее при изменении или добавлении задачи
        /// </summary>
        event EventHandler<TaskChangedEventArgs> TaskChanged;

        /// <summary>
        /// Получение всех задач пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Найденные задачи пользователя, или пустой массив</returns>
        Task<UserTask[]> GetAllUserTasksAsync(string userId);

        /// <summary>
        /// Получение всех задач по категории
        /// </summary>
        /// <param name="categoryId">Идентификатор категории</param>
        /// <returns>Найденные задачи по категории, или пустой массив</returns>
        Task<UserTask[]> GetTasksByCategoryAsync(int categoryId);

        /// <summary>
        /// Получение задачи по id
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <returns>Найденная задача или null</returns>
        Task<UserTask> GetTaskByIdAsync(int id);

        /// <summary>
        /// Добавление новой задачи
        /// </summary>
        /// <param name="task">Новая задача</param>
        /// <returns>Идентификатор добавленной задачи</returns>
        Task<int> AddTaskAsync(UserTask task);

        /// <summary>
        /// Редактирование задачи
        /// </summary>
        /// <param name="task">Измененная задача</param>
        /// <returns>Признак успеха операции</returns>
        Task<bool> UpdateTaskAsync(UserTask task);

        /// <summary>
        /// Удаление задачи
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <param name="userId">Идентификатор пользователя-владельца задачи</param>
        /// <returns>Призак успеха операции</returns>
        Task<bool> DeleteTaskAsync(int id, string userId);
    }

    [ContractClassFor(typeof(ITaskService))]
    internal abstract class ITaskServiceContracts : ITaskService
    {
        public event EventHandler<TaskChangedEventArgs> TaskChanged;

        public Task<UserTask[]> GetAllUserTasksAsync(string userId)
        {
            Contract.Requires(userId != null);
            Contract.Ensures(Contract.Result<UserTask[]>() != null);

            throw new System.NotImplementedException();
        }

        public Task<UserTask[]> GetTasksByCategoryAsync(int categoryId)
        {
            Contract.Ensures(Contract.Result<UserTask[]>() != null);

            throw new System.NotImplementedException();
        }

        public Task<UserTask> GetTaskByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> AddTaskAsync(UserTask task)
        {
            Contract.Requires(task != null);

            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateTaskAsync(UserTask task)
        {
            Contract.Requires(task != null);

            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteTaskAsync(int id, string userId)
        {
            Contract.Requires(userId != null);

            throw new System.NotImplementedException();
        }
    }
}