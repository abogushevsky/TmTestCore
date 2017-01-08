using System.Diagnostics.Contracts;

namespace TaskManager.DataLayer.Common.Interfaces
{
    /// <summary>
    /// Интерфейс описывает конвертер сущностей в DTO и обратно
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    /// <typeparam name="TDto">Тип DTO</typeparam>
    [ContractClass(typeof(IEntityDtoConverterContracts<,>))]
    public interface IEntityDtoConverter<TEntity, TDto> where TDto: class
    {
        /// <summary>
        /// Коневертирует сущность в DTO
        /// </summary>
        TDto Convert(TEntity entity);

        /// <summary>
        /// Конвертирует DTO в сущность
        /// </summary>
        TEntity Convert(TDto dto);
    }

    [ContractClassFor(typeof(IEntityDtoConverter<,>))]
    internal abstract class IEntityDtoConverterContracts<TEntity, TDto> : IEntityDtoConverter<TEntity, TDto> where TDto: class
    {
        public TDto Convert(TEntity entity)
        {
            Contract.Requires(typeof(TEntity).IsValueType || entity != null);
            Contract.Ensures(Contract.Result<TDto>() != null);

            throw new System.NotImplementedException();
        }

        public TEntity Convert(TDto dto)
        {
            Contract.Requires(dto != null);
            Contract.Ensures(typeof(TEntity).IsValueType || Contract.Result<TEntity>() != null);

            throw new System.NotImplementedException();
        }
    }
}