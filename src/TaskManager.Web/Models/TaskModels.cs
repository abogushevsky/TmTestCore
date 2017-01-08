using System;
using System.Diagnostics.Contracts;
using TaskManager.Common.Entities;

namespace TaskManager.Web.Models
{
    public class TaskListItemModel
    {
        public TaskListItemModel()
        {
            
        }

        public TaskListItemModel(UserTask task)
        {
            Contract.Requires(task != null);

            this.Id = task.Id;
            this.Title = task.Title;
            this.Category = task.Category;
            this.DueDate = task.DueDate.HasValue ? task.DueDate.Value : DateTime.MinValue;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public Category Category { get; set; }

        public DateTime DueDate { get; set; }

        
    }

    public class TaskModel : TaskListItemModel
    {
        public TaskModel()
        {

        }

        public TaskModel(UserTask task) : base(task)
        {
            Contract.Requires(task != null);

            this.Details = task.Details;
        }

        public string Details { get; set; }

        public UserTask ToUserTask(ApplicationUser user)
        {
            Contract.Requires(user != null);

            return new UserTask()
            {
                Id = this.Id,
                Title = this.Title,
                Details = this.Details,
                DueDate = this.DueDate == DateTime.MinValue ? (DateTime?) null : this.DueDate,
                Category = this.Category,
                User = new UserInfo()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                }
            };
        }
    }

    public class TaskChangeModel
    {
        public TaskChangeModel()
        {
            
        }

        public TaskChangeModel(TaskChangedEventArgs taskChangeDetails)
        {
            Contract.Requires(taskChangeDetails != null);

            this.Task = new TaskModel(taskChangeDetails.Task);
            this.ChangeType = taskChangeDetails.ChangeType;
        }

        public TaskModel Task { get; set; }

        public ChangeTypes ChangeType { get; set; }
    }
}