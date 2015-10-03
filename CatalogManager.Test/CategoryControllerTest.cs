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
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class CategoryControllerTest
    {
        /// <summary>
        /// Gets the categories test.
        /// </summary>
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

        /// <summary>
        /// Gets the category hierarchy test.
        /// </summary>
        [TestMethod]
        public void GetCategoryHierarchyTest()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);
            var controller = new CategoryController(appService);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            //Act
            var response = controller.GetCategoryHierarchy();

            //Assert
            IEnumerable<CategoryDto> categories;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.TryGetContentValue<IEnumerable<CategoryDto>>(out categories));
            var parentWithChild = categories.Where(x => x.ChildCategories.Count > 0);
            Assert.IsNotNull(parentWithChild);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(categories.Count() > 0);
            Assert.IsTrue(categories.ElementAt(0).ParentCategoryId.HasValue == false);

        }

        /// <summary>
        /// Gets the top level categories test.
        /// </summary>
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

        /// <summary>
        /// Gets the categories by parent should retrun one or more.
        /// </summary>
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

        /// <summary>
        /// Creates the category should create one.
        /// </summary>
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

        /// <summary>
        /// Gets the category by identifier should return one.
        /// </summary>
        [TestMethod]
        public void GetCategoryByIdShouldReturnOne()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);
            var controller = new CategoryController(appService);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            int id = 1;
            //Act
            var response = controller.GetCategoryById(id);

            //Assert
            CategoryDto category;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.TryGetContentValue<CategoryDto>(out category));
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(category.Id == id);
        }

        /// <summary>
        /// Updates the category should update.
        /// </summary>
        [TestMethod]
        public void UpdateCategoryShouldUpdate()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);
            var controller = new CategoryController(appService);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            string changedName = string.Format("Name changed at {0}", DateTime.Now);
            int id = 1;
            CategoryDto categoryToUpdate;
            var response = controller.GetCategoryById(id);
            response.TryGetContentValue<CategoryDto>(out categoryToUpdate);
            Assert.IsNotNull(categoryToUpdate);
            categoryToUpdate.Name = changedName;

            //Act
            response = controller.UpdateCategory(categoryToUpdate);

            //Assert
            CategoryDto category;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.TryGetContentValue<CategoryDto>(out category));
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(category.Id == id);
            Assert.IsTrue(category.Name == changedName);
        }

        /// <summary>
        /// Deletes the category should delete.
        /// </summary>
        [TestMethod]
        public void DeleteCategoryShouldDelete()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);
            var controller = new CategoryController(appService);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            string name = string.Format("Test Category to delete created at {0}", DateTime.Now);
            int categoryId = 0;

            var categoryDto = new CategoryDto()
            {
                Name = name,
                ParentCategoryId = 1,
                Products = new List<ProductDto>()
            };

            var response = controller.CreateCategory(categoryDto);
            CategoryDto categoryToDelete;
            response.TryGetContentValue<CategoryDto>(out categoryToDelete);
            categoryId = categoryToDelete.Id;
            Assert.IsNotNull(categoryToDelete);
            Assert.IsTrue(categoryToDelete.Id > 0);

            //Act
            response = controller.DeleteCategory(categoryId);
            var getResponse = controller.GetCategoryById(categoryId);

            //Assert
            CategoryDto category;
            Assert.IsNotNull(getResponse);
            Assert.IsTrue(!getResponse.TryGetContentValue<CategoryDto>(out category));
            Assert.IsTrue(getResponse.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(category==null);
        }
    }
}
