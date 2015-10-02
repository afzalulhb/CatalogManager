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
    [RoutePrefix("category")]
    [EnableCors(origins: "http://localhost:58082", headers: "*", methods: "*")]
    public class CategoryController : ApiController
    {
        private readonly ICategoryProductAppService categoryProductAppService;

        public CategoryController()
            : base()
        {
        }


        public CategoryController(ICategoryProductAppService service)
        {
            categoryProductAppService = service;
        }

        [HttpGet]
        public HttpResponseMessage GetCategories()
        {
            var result = categoryProductAppService.GetCategories();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        [HttpGet]
        [Route("hierarchy")]
        public HttpResponseMessage GetCategoryHierarchy()
        {
            var result = categoryProductAppService.GetCategoryHierarchy();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("top")]
        public HttpResponseMessage GetTopLevelCategories()
        {
            var result = categoryProductAppService.GetTopLevelCategories();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("byparent")]
        public HttpResponseMessage GetCategoriesByParent(int id)
        {
            var result = categoryProductAppService.GetCategoriesByParent(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage CreateCategory([FromBody] CategoryDto dto)
        {
            var result = categoryProductAppService.CreateCategory(dto);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [HttpGet]
        [Route("byid")]
        public HttpResponseMessage GetCategoryById(int id)
        {
            var result = categoryProductAppService.GetCategoryById(id);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPut]
        [Route("")]
        public HttpResponseMessage UpdateCategory([FromBody] CategoryDto dto)
        {
            var result = categoryProductAppService.UpdateCategory(dto);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpDelete]
        [Route("")]
        public HttpResponseMessage DeleteCategory(int id)
        {
            categoryProductAppService.DeleteCategory(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
