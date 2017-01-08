using TaskManager.Common.Interfaces;

namespace TaskManager.Common.Entities
{
    /// <summary>
    /// Информация о пользователе
    /// </summary>
    public class UserInfo : IEntityWithId<string>
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
    }
}