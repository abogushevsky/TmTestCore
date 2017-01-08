using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using TaskManager.BusinessLayer.Properties;
using TaskManager.Common.Exceptions;
using TaskManager.Common.Interfaces;
using TaskManager.DataLayer.Common.Exceptions;
using TaskManager.DataLayer.Common.Interfaces;

namespace TaskManager.BusinessLayer
{
    /// <summary>
    /// Базовый класс для сервисов для работы с сущностями
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public abstract class EntityServiceBase<TEntity, TKey> where TEntity: IEntityWithId<TKey>
    {
        private readonly IRepository<TEntity, TKey> repository;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="repository">Репозитория для сущностей типа <see cref="TEntity"/></param>
        protected EntityServiceBase(IRepository<TEntity, TKey> repository)
        {
            Contract.Requires(repository != null);

            this.repository = repository;
        }

        /// <summary>
        /// Метод-обертка для любых операций уровня бизнес-логики, решающий задачу обработки исключений 
        /// и выполняющий базовую для всех операций логику
        /// </summary>
        /// <typeparam name="TResult">Тип результата</typeparam>
        /// <param name="asyncFunc">Асинхронная функция, выполняющая конкретные действия</param>
        /// <returns>Результат выполнения действия</returns>
        protected async Task<TResult> ExecAsync<TResult>(Func<Task<TResult>> asyncFunc)
        {
            try
            {
                return await asyncFunc();
            }
            catch (ConcurrentUpdateException)
            {
                throw new BusinessException(Resources.ConcurrentEditExText);
            }
            catch (RepositoryException)
            {
                throw new BusinessException(Resources.CommonRepositoryExText);
            }
            catch (Exception ex)
            {
                //TODO: log
                throw new BusinessException();
            }
        }

        /// <summary>
        /// Метод-обертка над репозиторием, решающий задачу обработки исключений и выполняющий
        /// базовую для всех операций логику
        /// </summary>
        /// <typeparam name="TResult">Тип результата</typeparam>
        /// <param name="asyncFunc">Асинхронная функция, выполняющая конкретные действия с репозиторием</param>
        /// <returns>Результат выполнения действия над репозиторием</returns>
        protected Task<TResult> ExecOnRepositoryAsync<TResult>(Func<IRepository<TEntity, TKey>, Task<TResult>> asyncFunc)
        {
            return ExecAsync(() => asyncFunc(this.repository));
        }
    }
}