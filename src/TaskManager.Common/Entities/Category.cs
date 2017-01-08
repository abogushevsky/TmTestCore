namespace TaskManager.Common.Entities
{
    /// <summary>
    /// Категория задачи
    /// </summary>
    public class Category : EntityBase
    {
        /// <summary>
        /// Название категории
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Пользователь, которому принадлежит категория
        /// </summary>
        public UserInfo User { get; set; }
    }
}