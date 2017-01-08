using TaskManager.Common.Interfaces;

namespace TaskManager.Common.Entities
{
    /// <summary>
    /// Базовый класс для всех сущностей
    /// </summary>
    public abstract class EntityBase : IEntityWithId<int>
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        public byte[] ModifiedTimestamp { get; set; }
    }
}