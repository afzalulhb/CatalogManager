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

namespace CatalogManager.DistributedService.Controllers
{
    [RoutePrefix("category")]
    public class CategoryController : ApiController
    {
        private readonly ICategoryProductAppService categoryProductAppService;

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

        //IEnumerable<CategoryDto> GetCategories();
        //IEnumerable<CategoryDto> GetTopLevelCategories();
        //IEnumerable<CategoryDto> GetCategoriesByParent(int parentId);
        //CategoryDto CreateCategory(CategoryDto dto);
        //CategoryDto GetCategoryById(int id);
        //CategoryDto UpdateCategory(CategoryDto dto);
        //void DeleteCategory(int id);

    }
}
