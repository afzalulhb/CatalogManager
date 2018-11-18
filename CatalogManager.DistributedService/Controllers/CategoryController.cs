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
        private readonly ICategoryProductAppService _appService;

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
            _appService = service;
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetCategories()
        {
            var result = await _appService.GetCategoriesAsync();
            return Ok(result);
        }

        /// <summary>
        /// Gets the category hierarchy.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("hierarchy")]
        public async Task<IHttpActionResult> GetCategoryHierarchy()
        {
            var result = await _appService.GetCategoryHierarchyAsync();
            return Ok(result);
        }

        /// <summary>
        /// Gets the top level categories.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("top")]
        public async Task<IHttpActionResult> GetTopLevelCategories()
        {
            var result = await _appService.GetTopLevelCategoriesAsync();
            return Ok(result);
        }

        /// <summary>
        /// Gets the categories by parent.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("byparent/{id}")]
        public async Task<IHttpActionResult> GetCategoriesByParent(int id)
        {
            if(id < 1)
            {
                return BadRequest("Invalid Id.");
            }
            var result = await _appService.GetCategoriesByParentAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Creates the category.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateCategory([FromBody] CategoryDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid dto.");
            }

            var result = await _appService.CreateCategoryAsync(dto);
            return Ok(result);
        }


        /// <summary>
        /// Gets the category by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("byid/{id}")]
        public async Task<IHttpActionResult> GetCategoryById(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid Id.");
            }
            var result = await _appService.GetCategoryByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Updates the category.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> UpdateCategory([FromBody] CategoryDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid dto.");
            }
            var result = await _appService.UpdateCategoryAsync(dto);
            return Ok(result);
        }

        /// <summary>
        /// Deletes the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteCategory(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid Id.");
            }
            _appService.DeleteCategory(id);
            return Ok("Successfully deleted.");
        }

    }
}
