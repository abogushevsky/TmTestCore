using System.Diagnostics.Contracts;
using TaskManager.Common.Entities;

namespace TaskManager.Web.Models
{
    public class CategoryModel
    {
        public CategoryModel()
        {
            
        }

        public CategoryModel(Category category)
        {
            Contract.Requires(category != null);

            this.Id = category.Id;
            this.Name = category.Name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public Category ToCategory(ApplicationUser user)
        {
            Contract.Requires(user != null);

            return new Category()
            {
                Id = this.Id,
                Name = this.Name,
                User = new UserInfo()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                }
            };
        }
    }
}