using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.DataLayer.Common.Interfaces;
using TaskManager.DataLayer.MsSql.Dto;

namespace TaskManager.DataLayer.MsSql.Specialized
{
    /// <summary>
    /// Реализация фильтрующего репозитория, работающая с MS SQL Server
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TFilter"></typeparam>
    public class SqlFilteredRepository<TEntity, TDto, TFilter> : SqlRepositoryBase, IFilteredRepository<TEntity, TFilter>
        where TDto : SqlDto
    {
        private readonly SqlCommandInfo _command;
        private readonly IEntityDtoConverter<TEntity, TDto> _converter;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="command">Сведения о запросе, который должен быть выполнен в БД</param>
        /// <param name="connectionStringName">Имя строки подключения в конфигурационном файле</param>
        /// <param name="converter">Конвертер для перевода сущностей в DTO и обратно</param>
        public SqlFilteredRepository(
            SqlCommandInfo command, 
            string connectionStringName, 
            IEntityDtoConverter<TEntity, TDto> converter) : base(connectionStringName)
        {
            Contract.Requires(!string.IsNullOrEmpty(command.Command));
            Contract.Requires(!string.IsNullOrEmpty(connectionStringName));
            Contract.Requires(converter != null);

            this._command = command;
            this._converter = converter;
        }

        /// <summary>
        /// Выполняет фильтрующий запрос
        /// </summary>
        /// <param name="filter">Данные фильтра</param>
        /// <returns>Найденные сущности или пустой массив</returns>
        public async Task<TEntity[]> FilterAsync(TFilter filter)
        {
            TDto[] result = (await UsingConnectionAsync<TDto>(this._command, filter)).ToArray();
            return result.Select(this._converter.Convert).ToArray();
        }
    }
}