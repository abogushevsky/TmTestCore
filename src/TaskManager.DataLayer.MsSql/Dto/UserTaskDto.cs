using System;
using System.Diagnostics.Contracts;
using TaskManager.Common.Entities;
using TaskManager.DataLayer.Common.Interfaces;

namespace TaskManager.DataLayer.MsSql.Dto
{
    public class UserTaskDto : SqlDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public DateTime? DueDate { get; set; }

        public string UserId { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public int? CategoryId { get; set; }

        public string CategoryName { get; set; }

        public byte[] ModifiedTimestamp { get; set; }

        #region Overrides of SqlDto

        /// <summary>
        /// Метод формирует набор параметров для передачи в хранимую процедуру добавления
        /// новой записи. Названия свойств должны совпадать с названиями параметров процедуры
        /// </summary>
        /// <returns></returns>
        public override object GetParametersForInsert()
        {
            return new
            {
                Title = this.Title,
                Details = this.Details,
                DueDate = this.DueDate,
                CategoryId = this.CategoryId,
                UserId = this.UserId,
            };
        }

        /// <summary>
        /// Метод формирует набор параметров для передачи в хранимую процедуру редактирования
        /// записи. Названия свойств должны совпадать с названиями параметров процедуры
        /// </summary>
        /// <returns></returns>
        public override object GetParametersForUpdate()
        {
            return new
            {
                Id = this.Id,
                Title = this.Title,
                Details = this.Details,
                DueDate = this.DueDate,
                CategoryId = this.CategoryId,
                UserId = this.UserId,
                ModifiedTimestamp = this.ModifiedTimestamp
            };
        }

        #endregion
    }

    public class UserTaskConverter : IEntityDtoConverter<UserTask, UserTaskDto>
    {
        /// <summary>
        /// Коневертирует сущность в DTO
        /// </summary>
        public UserTaskDto Convert(UserTask entity)
        {
            Contract.Requires(entity.User != null);

            return new UserTaskDto()
            {
                Id = entity.Id,
                Title = entity.Title,
                Details = entity.Details,
                DueDate = entity.DueDate,
                CategoryId = entity.Category != null ? entity.Category.Id : (int?)null,
                CategoryName = entity.Category != null ? entity.Category.Name : null,
                UserId = entity.User.Id,
                UserFirstName = entity.User.FirstName,
                UserLastName = entity.User.LastName,
                ModifiedTimestamp = entity.ModifiedTimestamp
            };
        }

        /// <summary>
        /// Конвертирует DTO в сущность
        /// </summary>
        public UserTask Convert(UserTaskDto dto)
        {
            return new UserTask()
            {
                Id = dto.Id,
                Title = dto.Title,
                Details = dto.Details,
                DueDate = dto.DueDate,
                Category = dto.CategoryId.HasValue ? new Category()
                {
                    Id = dto.CategoryId.Value,
                    Name = dto.CategoryName
                } : null,
                User = new UserInfo()
                {
                    Id = dto.UserId,
                    FirstName = dto.UserFirstName,
                    LastName = dto.UserLastName
                },
                ModifiedTimestamp = dto.ModifiedTimestamp
            };
        }
    }
}