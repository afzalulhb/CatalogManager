using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CatalogManager.AppService;
using CatalogManager.AppService.Services;
using System.Net.Http;
using System.Net;
using CatalogManager.AppService.Dtos;
using System.Web.Http.Cors;
using System.Threading.Tasks;

/// <summary>
/// 
/// </summary>
namespace CatalogManager.DistributedService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("category")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CategoryController : ApiController
    {
        /// <summary>
        /// The category product application service
        /// </summary>
        private readonly ICategoryProductAppService categoryProductAppService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController"/> class.
        /// </summary>
        public CategoryController()
            : base()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController" /> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public CategoryController(ICategoryProductAppService service)
        {
            categoryProductAppService = service;
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetCategories()
        {
            var result = await categoryProductAppService.GetCategoriesAsync();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Gets the category hierarchy.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("hierarchy")]
        public async Task<HttpResponseMessage> GetCategoryHierarchy()
        {
            var result = await categoryProductAppService.GetCategoryHierarchyAsync();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Gets the top level categories.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("top")]
        public async Task<HttpResponseMessage> GetTopLevelCategories()
        {
            var result = await categoryProductAppService.GetTopLevelCategoriesAsync();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Gets the categories by parent.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("byparent/{id}")]
        public async Task<HttpResponseMessage> GetCategoriesByParent(int id)
        {
            var result = await categoryProductAppService.GetCategoriesByParentAsync(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Creates the category.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateCategory([FromBody] CategoryDto dto)
        {
            var result = await categoryProductAppService.CreateCategoryAsync(dto);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        /// <summary>
        /// Gets the category by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("byid/{id}")]
        public async Task<HttpResponseMessage> GetCategoryById(int id)
        {
            var result = await categoryProductAppService.GetCategoryByIdAsync(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Updates the category.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public async Task<HttpResponseMessage> UpdateCategory([FromBody] CategoryDto dto)
        {
            var result = await categoryProductAppService.UpdateCategoryAsync(dto);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Deletes the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage DeleteCategory(int id)
        {
            categoryProductAppService.DeleteCategory(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
