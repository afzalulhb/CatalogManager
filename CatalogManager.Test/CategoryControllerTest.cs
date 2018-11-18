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
using System.Threading.Tasks;
using NSubstitute;
using System.Web.Http.Results;

namespace CatalogManager.Test
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class CategoryControllerTest
    {
        private ICategoryProductAppService _appService;

        public CategoryControllerTest()
        {
            _appService = Substitute.For<ICategoryProductAppService>();
        }

        /// <summary>
        /// Gets the categories test.
        /// </summary>
        [TestMethod]
        public async Task GetCategoriesTest()
        {
            // Arrange 
            var dtos = new List<CategoryDto>{new CategoryDto()
            {
                Name = "Test Category",
                ParentCategoryId = 1,
                Products = new List<ProductDto>()
            } };
            _appService.GetCategoriesAsync().Returns(dtos);

            var controller = new CategoryController(_appService);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            //Act
            var response = await controller.GetCategories();
            var result = response as OkNegotiatedContentResult<IEnumerable<CategoryDto>>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content.Count() > 0);
        }

        /// <summary>
        /// Gets the category hierarchy test.
        /// </summary>
        [TestMethod]
        public async Task GetCategoryHierarchyTest()
        {
            // Arrange 
            var dtos = new List<CategoryDto>{new CategoryDto()
            {
                Id  = 1,
                Name = "Test Category",
                ParentCategoryId = null,
                Products = new List<ProductDto>()},
                new CategoryDto()
            {
                Id=2,
                Name = "Test Category",
                ParentCategoryId = 1,
                Products = new List<ProductDto>()} };
            _appService.GetCategoryHierarchyAsync().Returns(dtos);

            var controller = new CategoryController(_appService);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            //Act
            var response = await controller.GetCategoryHierarchy();
            var result = response as OkNegotiatedContentResult<IEnumerable<CategoryDto>>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content.Count() > 0);
            Assert.IsTrue(result.Content.Where(x=> x.Id == 1).Count() == 1);
            Assert.IsTrue(result.Content.Where(x => x.Id == 2).Count() == 1);
            Assert.IsTrue(result.Content.Where(x => x.ParentCategoryId == 1).Count() == 1);
        }

        /// <summary>
        /// Gets the top level categories test.
        /// </summary>
        [TestMethod]
        public async Task GetTopLevelCategoriesTest()
        {
            // Arrange 
            var dtos = new List<CategoryDto>{new CategoryDto()
            {
                Id  = 1,
                Name = "Test Category",
                ParentCategoryId = null,
                Products = new List<ProductDto>()} };
            _appService.GetTopLevelCategoriesAsync().Returns(dtos);

            var controller = new CategoryController(_appService);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            //Act
            var response = await controller.GetTopLevelCategories();
            var result = response as OkNegotiatedContentResult<IEnumerable<CategoryDto>>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content.Count() > 0);
            Assert.IsTrue(result.Content.ElementAt(0).ParentCategoryId == null);
        }

        /// <summary>
        /// Gets the categories by parent should retrun one or more.
        /// </summary>
        [TestMethod]
        public async Task GetCategoriesByParentShouldRetrunOneOrMore()
        {
            // Arrange
            var parentId = 1;
            var dtos = new List<CategoryDto>{
                new CategoryDto()
            {
                Id=2,
                Name = "Test Category",
                ParentCategoryId = 1,
                Products = new List<ProductDto>()} };
            _appService.GetCategoriesByParentAsync(Arg.Any<int>()).Returns(dtos);

            var controller = new CategoryController(_appService);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            //Act
            var response = await controller.GetCategoriesByParent(parentId);
            var result = response as OkNegotiatedContentResult<IEnumerable<CategoryDto>>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content.Count() > 0);
            Assert.IsTrue(result.Content.ElementAt(0).ParentCategoryId == 1);
        }

        /// <summary>
        /// Creates the category should create one.
        /// </summary>
        [TestMethod]
        public async Task CreateCategoryShouldCreateOne()
        {
            // Arrange 
            var dto = new CategoryDto()
            {
                Id  = 1,
                Name = "Test Category",
                ParentCategoryId = null,
                Products = new List<ProductDto>()};
            _appService.CreateCategoryAsync(Arg.Any<CategoryDto>()).Returns(dto);

            var controller = new CategoryController(_appService);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            //Act
            var response = await controller.CreateCategory(dto);
            var result = response as OkNegotiatedContentResult<CategoryDto>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content.Id == 1);            
        }

        /// <summary>
        /// Gets the category by identifier should return one.
        /// </summary>
        [TestMethod]
        public async Task GetCategoryByIdShouldReturnOne()
        {
            // Arrange 
            var id = 1;
            var dto = new CategoryDto()
            {
                Id = 1,
                Name = "Test Category",
                ParentCategoryId = null,
                Products = new List<ProductDto>()
            };
            _appService.GetCategoryByIdAsync(Arg.Any<int>()).Returns(dto);

            var controller = new CategoryController(_appService);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            //Act
            var response = await controller.GetCategoryById(id);
            var result = response as OkNegotiatedContentResult<CategoryDto>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content.Id == id);
        }

        /// <summary>
        /// Updates the category should update.
        /// </summary>
        [TestMethod]
        public async Task UpdateCategoryShouldUpdate()
        {
            // Arrange 
            var dto = new CategoryDto()
            {
                Id = 1,
                Name = "Test Category",
                ParentCategoryId = null,
                Products = new List<ProductDto>()
            };
             
            var updatedDto = new CategoryDto()
            {
                Id = 1,
                Name = "Updated Test Category",
                ParentCategoryId = null,
                Products = new List<ProductDto>()
            };
            _appService.UpdateCategoryAsync(Arg.Any<CategoryDto>()).Returns(updatedDto);

            var controller = new CategoryController(_appService);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            //Act
            var response = await controller.UpdateCategory(dto);
            var result = response as OkNegotiatedContentResult<CategoryDto>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content.Id == 1);
            Assert.IsTrue(result.Content.Name == "Updated Test Category");
           
        }

        /// <summary>
        /// Deletes the category should delete.
        /// </summary>
        [TestMethod]
        public async Task DeleteCategoryShouldDelete()
        {
            // Arrange 
            var id = 1;

            var controller = new CategoryController(_appService);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            //Act
            var response = await controller.DeleteCategory(id);
            var result = response as OkNegotiatedContentResult<string>;

            //Assert
            Assert.IsNotNull(result.Content);
            Assert.IsTrue(result.Content == "Successfully deleted.");

        }
    }
}
