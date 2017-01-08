using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using TaskManager.Common.Entities;
using TaskManager.Common.Interfaces;
using TaskManager.DataLayer.Common.Filters;
using TaskManager.DataLayer.Common.Interfaces;

namespace TaskManager.BusinessLayer
{
    public class CategoriesService : EntityServiceBase<Category, int>, ICategoriesService
    {
        private readonly IFilteredRepository<Category, CategoriesByUserFilter> categoriesByUsersFilter;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="repository">Репозитория для сущностей типа <see cref="Category"/></param>
        /// <param name="categoriesByUsersFilter">Фильтр категорий по пользователям</param>
        public CategoriesService(
            IRepository<Category, int> repository,
            IFilteredRepository<Category, CategoriesByUserFilter> categoriesByUsersFilter) : base(repository)
        {
            Contract.Requires(repository != null);
            Contract.Requires(categoriesByUsersFilter != null);

            this.categoriesByUsersFilter = categoriesByUsersFilter;
        }

        /// <summary>
        /// Получение всех категорий пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Найденные категории пользователя или пустой массив</returns>
        public async Task<Category[]> GetUserCategoriesAsync(string userId)
        {
            return await ExecAsync(() => this.categoriesByUsersFilter.FilterAsync(new CategoriesByUserFilter(userId)));
        }

        /// <summary>
        /// Получение категории по id
        /// </summary>
        /// <param name="id">Идентификатор категории</param>
        /// <returns>Найденную категорию или null</returns>
        public async Task<Category> GetCategoryById(int id)
        {
            return await ExecOnRepositoryAsync(r => r.GetByIdAsync(id));
        }

        /// <summary>
        /// Добавление новой категории
        /// </summary>
        /// <param name="category">Новая категория</param>
        /// <returns>Идентификатор категории</returns>
        public async Task<int> AddCategoryAsync(Category category)
        {
            return await ExecOnRepositoryAsync(r => r.CreateAsync(category));
        }

        /// <summary>
        /// Редактирование категории
        /// </summary>
        /// <param name="category">Измененная категория</param>
        /// <returns>Признак успеха операции</returns>
        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            return await ExecOnRepositoryAsync(r => r.UpdateAsync(category));
        }

        /// <summary>
        /// Удаление категории
        /// </summary>
        /// <param name="id">Идентификатор категории</param>
        /// <returns>Признак успеха операции</returns>
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            return await ExecOnRepositoryAsync(r => r.DeleteAsync(id));
        }
    }
}