using TaskManager.Common.Entities;
using TaskManager.DataLayer.Common.Interfaces;

namespace TaskManager.DataLayer.MsSql.Dto
{
    public class UserInfoDto
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    public class UserInfoConverter : IEntityDtoConverter<UserInfo, UserInfoDto>
    {
        #region Implementation of IEntityDtoConverter<UserInfo,UserInfoDto>

        /// <summary>
        /// Коневертирует сущность в DTO
        /// </summary>
        public UserInfoDto Convert(UserInfo entity)
        {
            return new UserInfoDto()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };
        }

        /// <summary>
        /// Конвертирует DTO в сущность
        /// </summary>
        public UserInfo Convert(UserInfoDto dto)
        {
            return new UserInfo()
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };
        }

        #endregion
    }
}