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

namespace CatalogManager.Test
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class ProductControllerTest
    {

        /// <summary>
        /// Gets the products by category should retrun one or more.
        /// </summary>
        [TestMethod]
        public async Task GetProductsByCategoryShouldRetrunOneOrMore()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);
            var controller = new ProductController(appService);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            var categoryId = 1;

            //Act
            var response = await controller.GetProductsByCategory(categoryId);

            //Assert
            IEnumerable<ProductDto> products;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.TryGetContentValue<IEnumerable<ProductDto>>(out products));
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(products.Count() > 0);
            Assert.IsTrue(products.ElementAt(0).CategoryId == categoryId);
        }

        /// <summary>
        /// Gets the product by identifier should return one.
        /// </summary>
        [TestMethod]
        public async Task GetProductByIdShouldReturnOne()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);
            var controller = new ProductController(appService);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            int id = 1;
            //Act
            var response = await controller.GetProductById(id);

            //Assert
            ProductDto product;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.TryGetContentValue<ProductDto>(out product));
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(product.Id == id);
        }

        /// <summary>
        /// Creates the product should create one.
        /// </summary>
        [TestMethod]
        public async Task CreateProductShouldCreateOne()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);
            var controller = new ProductController(appService);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            string name = string.Format("Test product created at {0}", DateTime.Now);
            decimal price = 111.11M;

            var dto = new ProductDto()
            {
                Name = name,
                Description = "",
                Price = price,
                CategoryId = 1
            };

            //Act
            var response = await controller.CreateProduct(dto);

            //Assert
            ProductDto product;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.TryGetContentValue<ProductDto>(out product));
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(product.Id > 0);
            Assert.IsTrue(product.Name == name);
            Assert.IsTrue(product.Price  == price);
        }

        /// <summary>
        /// Updates the product should update.
        /// </summary>
        [TestMethod]
        public async Task UpdateProductShouldUpdate()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);
            var controller = new ProductController(appService);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            string changedName = string.Format("Name changed at {0}", DateTime.Now);
            int id = 1;
            ProductDto productToUpdate;
            var response = await  controller.GetProductById(id);
            response.TryGetContentValue<ProductDto>(out productToUpdate);
            Assert.IsNotNull(productToUpdate);
            productToUpdate.Name = changedName;

            //Act
            response = await controller.UpdateProduct(productToUpdate);

            //Assert
            ProductDto product;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.TryGetContentValue<ProductDto>(out product));
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(product.Id == id);
            Assert.IsTrue(product.Name == changedName);
        }

        /// <summary>
        /// Deletes the product should delete.
        /// </summary>
        [TestMethod]
        public async Task DeleteProductShouldDelete()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);
            var controller = new ProductController(appService);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            string name = string.Format("Test product created at {0}", DateTime.Now);
            decimal price = 111.11M;
            int productId = 0;

            var dto = new ProductDto()
            {
                Name = name,
                Description = "",
                Price = price,
                CategoryId = 1
            };

            var response = await controller.CreateProduct(dto);
            ProductDto productToDelete;
            response.TryGetContentValue<ProductDto>(out productToDelete);
            productId = productToDelete.Id;
            Assert.IsNotNull(productToDelete);
            Assert.IsTrue(productToDelete.Id > 0);

            //Act
            response = await controller.DeleteProduct(productId);
            var getResponse = await  controller.GetProductById(productId);

            //Assert
            ProductDto product;
            Assert.IsNotNull(getResponse);
            Assert.IsTrue(!getResponse.TryGetContentValue<ProductDto>(out product));
            Assert.IsTrue(getResponse.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(product == null);
        }
    }
}
