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

namespace CatalogManager.DistributedService.Controllers
{
    [RoutePrefix("product")]
    [EnableCors(origins: "http://localhost:58082", headers: "*", methods: "*")]
    public class ProductController : ApiController
    {
        private readonly ICategoryProductAppService categoryProductAppService;

        public ProductController()
            : base()
        {
        }
        public ProductController(ICategoryProductAppService service)
        {
            categoryProductAppService = service;
        }

        [HttpGet]
        [Route("bycategory")]
        public HttpResponseMessage GetProductsByCategory(int categoryId)
        {
            var result = categoryProductAppService.GetProductsByCategory(categoryId);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("byid")]
        public HttpResponseMessage GetProductById(int id)
        {
            var result = categoryProductAppService.GetProductById(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage CreateProduct([FromBody] ProductDto dto)
        {
            var result = categoryProductAppService.CreateProduct(dto);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPut]
        [Route("")]
        public HttpResponseMessage UpdateProduct([FromBody] ProductDto dto)
        {
            var result = categoryProductAppService.UpdateProduct(dto);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpDelete]
        [Route("")]
        public HttpResponseMessage DeleteProduct(int id)
        {
            categoryProductAppService.DeleteProduct(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
