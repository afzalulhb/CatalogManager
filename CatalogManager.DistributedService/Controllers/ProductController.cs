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

namespace CatalogManager.DistributedService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("product")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductController : ApiController
    {
        /// <summary>
        /// The category product application service
        /// </summary>
        private readonly ICategoryProductAppService _appService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        public ProductController()
            : base()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public ProductController(ICategoryProductAppService service)
        {
            _appService = service;
        }

        /// <summary>
        /// Gets the products by category.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("bycategory/{categoryId}")]
        public async Task<IHttpActionResult> GetProductsByCategory(int categoryId)
        {
            if (categoryId < 1)
            {
                return BadRequest("Invalid CategoryId.");
            }
            var result = await _appService.GetProductsByCategoryAsync(categoryId);
            return Ok(result);
        }

        /// <summary>
        /// Gets the product by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("byid/{id}")]
        public async Task<IHttpActionResult> GetProductById(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid Id.");
            }
            var result = await _appService.GetProductByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Creates the product.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateProduct([FromBody] ProductDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid dto.");
            }
            var result = await _appService.CreateProductAsync(dto);
            return Ok(result);
        }

        /// <summary>
        /// Updates the product.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> UpdateProduct([FromBody] ProductDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid dto.");
            }
            var result = await _appService.UpdateProductAsync(dto);
            return Ok(result);
        }

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid Id.");
            }
            await _appService.DeleteProductAsync(id);
            return Ok("Successfully deleted.");
        }

    }
}
