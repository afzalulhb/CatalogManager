using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CatalogManager.DistributedService.Controllers;
using CatalogManager.Infrastructure;
using CatalogManager.Infrastructure.UnitOfWork;
using CatalogManager.AppService.Services;
using System.Net;
using System.Net.Http;
using CatalogManager.AppService.Dtos;
using System.Web.Http.Hosting;
using System.Web.Http;

namespace CatalogManager.Test
{
    [TestClass]
    public class CategoryControllerTest
    {
        //IEnumerable<CategoryDto> GetCategories();
        //IEnumerable<CategoryDto> GetTopLevelCategories();
        //IEnumerable<CategoryDto> GetCategoriesByParent(int parentId);

        //CategoryDto CreateCategory(CategoryDto dto);
        //CategoryDto GetCategoryById(int id);
        //CategoryDto UpdateCategory(CategoryDto dto);
        //void DeleteCategory(int id);

        [TestMethod]
        public void GetCategoriesTest()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);
            var controller = new CategoryController(appService);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            //Act
            var response = controller.GetCategories();

            //Assert
            IEnumerable<CategoryDto> categories;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.TryGetContentValue<IEnumerable<CategoryDto>>(out categories));
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(categories.Count() > 0);

        }

        [TestMethod]
        public void GetTopLevelCategoriesTest()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);
            var controller = new CategoryController(appService);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            //Act
            var response = controller.GetTopLevelCategories();

            //Assert
            IEnumerable<CategoryDto> categories;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.TryGetContentValue<IEnumerable<CategoryDto>>(out categories));
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(categories.Count() > 0);
            Assert.IsTrue(categories.ElementAt(0).ParentCategoryId.HasValue == false);

        }

        [TestMethod]
        public void GetCategoriesByParentShouldRetrunOneOrMore()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);
            var controller = new CategoryController(appService);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            var parentId = 1;

            //Act
            var response = controller.GetCategoriesByParent(parentId);

            //Assert
            IEnumerable<CategoryDto> categories;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.TryGetContentValue<IEnumerable<CategoryDto>>(out categories));
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(categories.Count() > 0);
            Assert.IsTrue(categories.ElementAt(0).ParentCategoryId == parentId);
        }
        [TestMethod]
        public void CreateCategoryShouldCreateOne()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);
            var controller = new CategoryController(appService);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            string name = string.Format("Test Category create at {0}", DateTime.Now);

            var categoryDto = new CategoryDto()
            {
                Name = name,
                ParentCategoryId = 1,
                Products = new List<ProductDto>()
            };

            //Act
            var response = controller.CreateCategory(categoryDto);

            //Assert
            CategoryDto category;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.TryGetContentValue<CategoryDto>(out category));
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(category.Id > 0);
            Assert.IsTrue(category.Name == name);
        }
    }
}
