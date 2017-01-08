using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Common.Interfaces;
using TaskManager.DataLayer.Common.Exceptions;
using TaskManager.DataLayer.Common.Interfaces;
using TaskManager.DataLayer.MsSql.Dto;

namespace TaskManager.DataLayer.MsSql
{
    /// <summary>
    /// Реализация репозитория, работающая с MS SQL Server
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    /// <typeparam name="TDto">Тип DTO</typeparam>
    public class CrudSqlRepository<TEntity, TKey, TDto> : SqlRepositoryBase, IRepository<TEntity, TKey> 
        where TEntity : IEntityWithId<TKey> 
        where TDto : SqlDto
    {
        private readonly IEntityDtoConverter<TEntity, TDto> _converter;
        private readonly CrudCommandsBundle _commands;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="converter">Конвертер для перевода сущностей в DTO и обратно</param>
        /// <param name="commands">Связка команд SQL</param>
        /// <param name="connectionStringName">Имя строки подключения в конфигурационном файле</param>
        public CrudSqlRepository(IEntityDtoConverter<TEntity, TDto> converter, CrudCommandsBundle commands, string connectionStringName)
            : base(connectionStringName)
        {
            Contract.Requires(converter != null);
            Contract.Requires(commands != null);
            Contract.Requires(!string.IsNullOrEmpty(connectionStringName));

            this._converter = converter;
            this._commands = commands;
        }

        /// <summary>
        /// Возвращает все сущности репозитория
        /// </summary>
        public async Task<TEntity[]> GetAllAsync()
        {
            TDto[] result = (await UsingConnectionAsync<TDto>(this._commands.GetAllCommand, null)).ToArray();
            return result.Select(this._converter.Convert).ToArray();
        }

        /// <summary>
        /// Возвращает сущность по её идентификатору
        /// </summary>
        /// <param name="id">Уникальный идентификатор</param>
        /// <returns>Найденная сущность или null</returns>
        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            TDto[] result = (await UsingConnectionAsync<TDto>(this._commands.GetByIdCommand, new {Id = id})).ToArray();
            return result.Any() ? this._converter.Convert(result.First()) : default(TEntity);
        }

        /// <summary>
        /// Добавляет новую сущность в репозиторий и возвращает её идентификатор
        /// </summary>
        /// <param name="entity">Сущность для добавления</param>
        /// <returns>Идентификатор новой сущности</returns>
        /// <exception cref="EntityCreateException">Если не удалось добавить новую сущность</exception>
        public async Task<TKey> CreateAsync(TEntity entity)
        {
            try
            {
                var result =
                    (await UsingConnectionAsync<dynamic>(this._commands.CreateCommand, this._converter.Convert(entity).GetParametersForInsert()))
                        .ToArray();

                if (result.Any())
                {
                    return (TKey) result.First().Result;
                }
                throw new EntityCreateException();
            }
            catch (SqlException ex)
            {
                throw new EntityCreateException(ex);
            }
        }

        /// <summary>
        /// Обновляет сущность в репозитории
        /// </summary>
        /// <param name="entity">Изменённая сущность</param>
        /// <returns>true, если операция затронула > 0 сущностей. false в противном случае</returns>
        /// <exception cref="ConcurrentUpdateException">При попытке обновить сущность с устаревшим временем последнего обновления</exception>
        public async Task<bool> UpdateAsync(TEntity entity)
        {
            dynamic[] result = (await
                UsingConnectionAsync<dynamic>(this._commands.UpdateCommand,
                    this._converter.Convert(entity).GetParametersForUpdate())).ToArray();
            if (result.Any())
            {
                int rowsAffected = (int) result.First().Result;
                return rowsAffected > 0;
            }

            return false;
        }

        /// <summary>
        /// Удаляет сущность из репозитория
        /// </summary>
        /// <param name="id">Идентификатор сущности, которую необходимо удалить</param>
        /// <returns>true, если операция затронула > 0 сущностей. false в противном случае</returns>
        public async Task<bool> DeleteAsync(TKey id)
        {
            var result = (await UsingConnectionAsync<dynamic>(this._commands.DeleteCommand, new { Id = id })).FirstOrDefault() > 0;
            if (result.Any())
            {
                int rowsAffected = (int)result.First().Result;
                return rowsAffected > 0;
            }

            return false;
        }
    }
}