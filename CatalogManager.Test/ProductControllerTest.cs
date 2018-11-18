using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CatalogManager.Infrastructure;
using CatalogManager.Infrastructure.UnitOfWork;
using CatalogManager.AppService.Services;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Collections.Generic;
using System.Linq;
using CatalogManager.AppService.Dtos;
using CatalogManager.DistributedService.Controllers;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Results;
using NSubstitute;

namespace CatalogManager.Test
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class ProductControllerTest
    {
        private ICategoryProductAppService _appService;
        public ProductControllerTest()
        {
            _appService = Substitute.For<ICategoryProductAppService>();
        }

        /// <summary>
        /// Gets the products by category should retrun one or more.
        /// </summary>
        [TestMethod]
        public async Task GetProductsByCategoryShouldRetrunOneOrMore()
        {
            // Arrange 
            var dtos = new List<ProductDto>{ new ProductDto()
            {
                Name = "Test Product",
                Description = "Test Product",
                Price = 10.00M,
                CategoryId = 1
            }};

            _appService.GetProductsByCategoryAsync(Arg.Any<int>()).Returns(dtos);
            var controller = new ProductController(_appService);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            var categoryId = 1;

            //Act
            var response = await controller.GetProductsByCategory(categoryId);
            var result = response as OkNegotiatedContentResult<IEnumerable<ProductDto>>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content.Count() > 0);
            Assert.IsTrue(result.Content.ElementAt(0).CategoryId == categoryId);
          }

        /// <summary>
        /// Gets the product by identifier should return one.
        /// </summary>
        [TestMethod]
        public async Task GetProductByIdShouldReturnOne()
        {
            // Arrange 
            var dto = new ProductDto()
            {
                Id = 1,
                Name = "Test Product",
                Description = "Test Product",
                Price = 10.00M,
                CategoryId = 1
            };

            _appService.GetProductByIdAsync(Arg.Any<int>()).Returns(dto);

            var controller = new ProductController(_appService);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            int id = 1;
            
            //Act
            var response = await controller.GetProductById(id);
            var result = response as OkNegotiatedContentResult<ProductDto>;
            
            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content.Id == id);
        }

        /// <summary>
        /// Creates the product should create one.
        /// </summary>
        [TestMethod]
        public async Task CreateProductShouldCreateOne()
        {          
            // Arrange 
            var dto = new ProductDto()
            {
                Id = 1,
                Name = "Test Product",
                Description = "Test Product",
                Price = 10.00M,
                CategoryId = 1
            };

            _appService.CreateProductAsync(Arg.Any<ProductDto>()).Returns(dto);
            var controller = new ProductController(_appService);

            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            //Act
            var response = await controller.CreateProduct(dto);
            var result = response as OkNegotiatedContentResult<ProductDto>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content.Id == 1);
        }

        /// <summary>
        /// Updates the product should update.
        /// </summary>
        [TestMethod]
        public async Task UpdateProductShouldUpdate()
        {
            // Arrange 
            var sourceDto = new ProductDto()
            {
                Id = 1,
                Name = "Test Product",
                Description = "Test Product",
                Price = 10.00M,
                CategoryId = 1
            };
            var dto = new ProductDto()
            {
                Id = 1,
                Name = "Update Test Product",
                Description = "Test Product",
                Price = 10.00M,
                CategoryId = 1
            };

            _appService.UpdateProductAsync(Arg.Any<ProductDto>()).Returns(dto);
            var controller = new ProductController(_appService);

            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            //Act
            var response = await controller.UpdateProduct(dto);
            var result = response as OkNegotiatedContentResult<ProductDto>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content.Id == 1);
            Assert.IsTrue(result.Content.Name == "Update Test Product");
        }

        /// <summary>
        /// Deletes the product should delete.
        /// </summary>
        [TestMethod]
        public async Task DeleteProductShouldDelete()
        {
            // Arrange 
            var id = 1;
            await _appService.DeleteProductAsync(Arg.Any<int>());
            var controller = new ProductController(_appService);

            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            //Act
            var response = await controller.DeleteProduct(id);
            var result = response as OkNegotiatedContentResult<string>;

            //Assert
            Assert.IsNotNull(result.Content);
            Assert.IsTrue(result.Content == "Successfully deleted.");
        }
    }
}
