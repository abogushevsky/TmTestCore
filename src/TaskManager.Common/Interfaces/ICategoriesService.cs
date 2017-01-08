using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using TaskManager.Common.Entities;

namespace TaskManager.Common.Interfaces
{
    /// <summary>
    /// Интерфейс описывает сервис для работы с категориями
    /// </summary>
    [ContractClass(typeof(ICategoriesServiceContracts))]
    public interface ICategoriesService
    {
        /// <summary>
        /// Получение всех категорий пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Найденные категории пользователя или пустой массив</returns>
        Task<Category[]> GetUserCategoriesAsync(string userId);

        /// <summary>
        /// Получение категории по id
        /// </summary>
        /// <param name="id">Идентификатор категории</param>
        /// <returns>Найденную категорию или null</returns>
        Task<Category> GetCategoryById(int id);

        /// <summary>
        /// Добавление новой категории
        /// </summary>
        /// <param name="category">Новая категория</param>
        /// <returns>Идентификатор категории</returns>
        Task<int> AddCategoryAsync(Category category);

        /// <summary>
        /// Редактирование категории
        /// </summary>
        /// <param name="category">Измененная категория</param>
        /// <returns>Признак успеха операции</returns>
        Task<bool> UpdateCategoryAsync(Category category);

        /// <summary>
        /// Удаление категории
        /// </summary>
        /// <param name="id">Идентификатор категории</param>
        /// <returns>Признак успеха операции</returns>
        Task<bool> DeleteCategoryAsync(int id);
    }

    [ContractClassFor(typeof(ICategoriesService))]
    internal abstract class ICategoriesServiceContracts : ICategoriesService
    {
        public Task<Category[]> GetUserCategoriesAsync(string userId)
        {
            Contract.Requires(userId != null);
            Contract.Ensures(Contract.Result<Category[]>() != null);

            throw new System.NotImplementedException();
        }

        public Task<Category> GetCategoryById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> AddCategoryAsync(Category category)
        {
            Contract.Requires(category != null);

            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateCategoryAsync(Category category)
        {
            Contract.Requires(category != null);

            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteCategoryAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}