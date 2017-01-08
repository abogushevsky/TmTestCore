namespace TaskManager.Common.Entities
{
    /// <summary>
    /// Аргументы события изменения задачи
    /// </summary>
    public class TaskChangedEventArgs
    {
        public TaskChangedEventArgs()
        {
            
        }

        public TaskChangedEventArgs(UserTask task, string ownerUserId, ChangeTypes changeType)
        {
            this.Task = task;
            this.OwnerUserId = ownerUserId;
            this.ChangeType = changeType;
        }

        /// <summary>
        /// Измененная задача
        /// </summary>
        public UserTask Task { get; set; }

        /// <summary>
        /// Идентификатор пользователя-владельца задачи
        /// </summary>
        public string OwnerUserId { get; set; }

        /// <summary>
        /// Тип изменения
        /// </summary>
        public ChangeTypes ChangeType { get; set; }
    }

    /// <summary>
    /// Типы возможных изменений
    /// </summary>
    public enum ChangeTypes
    {
        Added = 0,
        Edited,
        Deleted
    }
}