using System.Diagnostics.Contracts;
using TaskManager.Common.Entities;
using TaskManager.DataLayer.Common.Interfaces;

namespace TaskManager.DataLayer.MsSql.Dto
{
    public class CategoryDto : SqlDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public byte[] ModifiedTimestamp { get; set; }

        #region Overrides of DtoBase

        /// <summary>
        /// Метод формирует набор параметров для передачи в хранимую процедуру добавления
        /// новой записи. Названия свойств должны совпадать с названиями параметров процедуры
        /// </summary>
        /// <returns></returns>
        public override object GetParametersForInsert()
        {
            return new
            {
                Name = this.Name,
                UserId = this.UserId
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
                Name = this.Name,
                UserId = this.UserId
            };
        }

        #endregion
    }

    public class CategoryConverter : IEntityDtoConverter<Category, CategoryDto>
    {
        /// <summary>
        /// Коневертирует сущность в DTO
        /// </summary>
        public CategoryDto Convert(Category entity)
        {
            Contract.Requires(entity.User != null);

            return new CategoryDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                UserId = entity.User.Id,
                UserFirstName = entity.User.FirstName,
                UserLastName = entity.User.LastName,
                ModifiedTimestamp = entity.ModifiedTimestamp
            };
        }

        /// <summary>
        /// Конвертирует DTO в сущность
        /// </summary>
        public Category Convert(CategoryDto dto)
        {
            return new Category()
            {
                Id = dto.Id,
                Name = dto.Name,
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