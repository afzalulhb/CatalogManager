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
        public HttpResponseMessage GetCategories()
        {
            var result = categoryProductAppService.GetCategories();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Gets the category hierarchy.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("hierarchy")]
        public HttpResponseMessage GetCategoryHierarchy()
        {
            var result = categoryProductAppService.GetCategoryHierarchy();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Gets the top level categories.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("top")]
        public HttpResponseMessage GetTopLevelCategories()
        {
            var result = categoryProductAppService.GetTopLevelCategories();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Gets the categories by parent.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("byparent/{id}")]
        public HttpResponseMessage GetCategoriesByParent(int id)
        {
            var result = categoryProductAppService.GetCategoriesByParent(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Creates the category.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public HttpResponseMessage CreateCategory([FromBody] CategoryDto dto)
        {
            var result = categoryProductAppService.CreateCategory(dto);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        /// <summary>
        /// Gets the category by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("byid/{id}")]
        public HttpResponseMessage GetCategoryById(int id)
        {
            var result = categoryProductAppService.GetCategoryById(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Updates the category.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public HttpResponseMessage UpdateCategory([FromBody] CategoryDto dto)
        {
            var result = categoryProductAppService.UpdateCategory(dto);
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
