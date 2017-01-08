using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TaskManager.Common.Entities;
using TaskManager.Common.Interfaces;
using TaskManager.Web.Models;
using TaskManager.Web.Properties;

namespace TaskManager.Web.Controllers
{
    public class CategoriesController : TaskManagerApiControllerBase
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            Contract.Requires(categoriesService != null);

            this.categoriesService = categoriesService;
        }

        // GET api/<controller>
        public async Task<HttpResponseMessage> Get()
        {
            ApplicationUser currentUser = await GetCurrentUser();
            if (currentUser == null)
                return this.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "");
            Category[] categories = await this.categoriesService.GetUserCategoriesAsync(currentUser.Id);
            return this.Request.CreateResponse(categories.Select(c => new CategoryModel(c)));
        }

        // GET api/<controller>/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            Category category = await this.categoriesService.GetCategoryById(id);
            if (category == null)
                return this.Request.CreateErrorResponse(HttpStatusCode.NotFound, Resources.CategoryNotFound);
            return this.Request.CreateResponse(new CategoryModel(category));
        }

        // POST api/<controller>
        public async Task<HttpResponseMessage> Post([FromBody] CategoryModel category)
        {
            if (category == null)
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.DataNotSet);
            int id = await this.categoriesService.AddCategoryAsync(category.ToCategory(await GetCurrentUser()));
            return this.Request.CreateResponse(id);
        }

        // PUT api/<controller>/5
        public async Task<HttpResponseMessage> Put([FromBody]CategoryModel category)
        {
            if (category == null)
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.DataNotSet);

            bool result = await this.categoriesService.UpdateCategoryAsync(category.ToCategory(await GetCurrentUser()));
            if (!result)
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Resources.DataSaveError);

            return this.Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/<controller>/5
        public async Task<HttpResponseMessage> Delete(int id)
        {
            bool result = await this.categoriesService.DeleteCategoryAsync(id);
            if (!result)
                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Resources.DataRemoveError);

            return this.Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}