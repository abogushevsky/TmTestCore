using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using TaskManager.Common.Interfaces;

namespace TaskManager.DataLayer.Common.Interfaces
{
    /// <summary>
    /// Интерфейс описывает репозиторий сущностей <see cref="TEntity"/>
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    [ContractClass(typeof(IRepositoryContracts<,>))]
    public interface IRepository<TEntity, TKey> where TEntity : IEntityWithId<TKey>
    {
        /// <summary>
        /// Возвращает все сущности репозитория
        /// </summary>
        Task<TEntity[]> GetAllAsync();

        /// <summary>
        /// Возвращает сущность по её идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Найденная сущность или null</returns>
        Task<TEntity> GetByIdAsync(TKey id);

        /// <summary>
        /// Добавляет новую сущность в репозиторий и возвращает её идентификатор
        /// </summary>
        /// <param name="entity">Сущность для добавления</param>
        /// <returns>Идентификатор новой сущности</returns>
        Task<TKey> CreateAsync(TEntity entity);

        /// <summary>
        /// Обновляет сущность в репозитории
        /// </summary>
        /// <param name="entity">Изменённая сущность</param>
        /// <returns>true, если операция затронула > 0 сущностей. false в противном случае</returns>
        Task<bool> UpdateAsync(TEntity entity);

        /// <summary>
        /// Удаляет сущность из репозитория
        /// </summary>
        /// <param name="id">Идентификатор сущности, которую необходимо удалить</param>
        /// <returns>true, если операция затронула > 0 сущностей. false в противном случае</returns>
        Task<bool> DeleteAsync(TKey id);
    }

    [ContractClassFor(typeof(IRepository<,>))]
    internal abstract class IRepositoryContracts<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : IEntityWithId<TKey>
    {
        public Task<TEntity[]> GetAllAsync()
        {
            Contract.Ensures(Contract.Result<TEntity[]>() != null);

            throw new System.NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync(TKey id)
        {
            Contract.Requires(typeof(TKey).IsValueType || id != null);

            throw new System.NotImplementedException();
        }
        
        public Task<TKey> CreateAsync(TEntity entity)
        {
            Contract.Requires(typeof(TEntity).IsValueType || entity != null);
            Contract.Ensures(typeof(TKey).IsValueType || Contract.Result<TKey>() != null);

            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateAsync(TEntity entity)
        {
            Contract.Requires(typeof(TEntity).IsValueType || entity != null);
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteAsync(TKey id)
        {
            Contract.Requires(typeof(TKey).IsValueType || id != null);

            throw new System.NotImplementedException();
        }
    }
}