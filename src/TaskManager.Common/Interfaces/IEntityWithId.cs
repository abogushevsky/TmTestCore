namespace TaskManager.Common.Interfaces
{
    /// <summary>
    /// Интерфейс описывает сущность с уникальным идентификатором
    /// </summary>
    /// <typeparam name="T">Тип идентификатора</typeparam>
    public interface IEntityWithId<T>
    {
        /// <summary>
        /// Уникальный идентифкатор
        /// </summary>
        T Id { get; set; }
    }
}