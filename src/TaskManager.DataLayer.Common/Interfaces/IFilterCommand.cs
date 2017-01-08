using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace TaskManager.DataLayer.Common.Interfaces
{
    /// <summary>
    /// Интерфейс описывает репозиторий, позволяющий выполнять фильтрующие запросы, 
    /// используя данные фильтра типа <see cref="TFilter"/>
    /// </summary>
    /// <typeparam name="TEntity">Тип сущностей</typeparam>
    /// <typeparam name="TFilter">Тип фильтра</typeparam>
    [ContractClass(typeof(IFilteredRepositoryContracts<,>))]
    public interface IFilteredRepository<TEntity, TFilter>
    {
        /// <summary>
        /// Выполняет фильтрующий запрос
        /// </summary>
        /// <param name="filter">Данные фильтра</param>
        /// <returns>Найденные сущности или пустой массив</returns>
        Task<TEntity[]> FilterAsync(TFilter filter);
    }

    [ContractClassFor(typeof(IFilteredRepository<,>))]
    internal abstract class IFilteredRepositoryContracts<TEntity, TFilter> : IFilteredRepository<TEntity, TFilter>
    {
        public Task<TEntity[]> FilterAsync(TFilter filter)
        {
            Contract.Requires(typeof(TFilter).IsValueType || filter != null);
            Contract.Ensures(Contract.Result<TEntity[]>() != null);

            throw new System.NotImplementedException();
        }
    }
}