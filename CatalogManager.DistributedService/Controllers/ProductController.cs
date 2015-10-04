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
        private readonly ICategoryProductAppService categoryProductAppService;

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
            categoryProductAppService = service;
        }

        /// <summary>
        /// Gets the products by category.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("bycategory/{categoryId}")]
        public async Task<HttpResponseMessage> GetProductsByCategory(int categoryId)
        {
            var result = await categoryProductAppService.GetProductsByCategoryAsync(categoryId);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Gets the product by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("byid/{id}")]
        public async Task<HttpResponseMessage> GetProductById(int id)
        {
            var result = await categoryProductAppService.GetProductByIdAsync(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Creates the product.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateProduct([FromBody] ProductDto dto)
        {
            var result = await categoryProductAppService.CreateProductAsync(dto);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Updates the product.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        public async Task<HttpResponseMessage> UpdateProduct([FromBody] ProductDto dto)
        {
            var result = await categoryProductAppService.UpdateProductAsync(dto);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<HttpResponseMessage> DeleteProduct(int id)
        {
            await categoryProductAppService.DeleteProductAsync(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
