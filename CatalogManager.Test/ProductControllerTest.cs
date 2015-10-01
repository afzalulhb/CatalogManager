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

namespace CatalogManager.Test
{
    [TestClass]
    public class ProductControllerTest
    {

        [TestMethod]
        public void GetProductsByCategoryShouldRetrunOneOrMore()
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
            var response = controller.GetProductsByCategory(categoryId);

            //Assert
            IEnumerable<ProductDto> products;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.TryGetContentValue<IEnumerable<ProductDto>>(out products));
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(products.Count() > 0);
            Assert.IsTrue(products.ElementAt(0).CategoryId == categoryId);
        }

        [TestMethod]
        public void GetProductByIdShouldReturnOne()
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
            var response = controller.GetProductById(id);

            //Assert
            ProductDto product;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.TryGetContentValue<ProductDto>(out product));
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(product.Id == id);
        }
        
        [TestMethod]
        public void CreateProductShouldCreateOne()
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
            var response = controller.CreateProduct(dto);

            //Assert
            ProductDto product;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.TryGetContentValue<ProductDto>(out product));
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(product.Id > 0);
            Assert.IsTrue(product.Name == name);
            Assert.IsTrue(product.Price  == price);
        }

        [TestMethod]
        public void UpdateProductShouldUpdate()
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
            var response = controller.GetProductById(id);
            response.TryGetContentValue<ProductDto>(out productToUpdate);
            Assert.IsNotNull(productToUpdate);
            productToUpdate.Name = changedName;

            //Act
            response = controller.UpdateProduct(productToUpdate);

            //Assert
            ProductDto product;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.TryGetContentValue<ProductDto>(out product));
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(product.Id == id);
            Assert.IsTrue(product.Name == changedName);
        }

        [TestMethod]
        public void DeleteProductShouldDelete()
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

            var response = controller.CreateProduct(dto);
            ProductDto productToDelete;
            response.TryGetContentValue<ProductDto>(out productToDelete);
            productId = productToDelete.Id;
            Assert.IsNotNull(productToDelete);
            Assert.IsTrue(productToDelete.Id > 0);

            //Act
            response = controller.DeleteProduct(productId);
            var getResponse = controller.GetProductById(productId);

            //Assert
            ProductDto product;
            Assert.IsNotNull(getResponse);
            Assert.IsTrue(!getResponse.TryGetContentValue<ProductDto>(out product));
            Assert.IsTrue(getResponse.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(product == null);
        }
    }
}
