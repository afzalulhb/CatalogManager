using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CatalogManager.AppService.Services;
using CatalogManager.Infrastructure.UnitOfWork;
using CatalogManager.Infrastructure;
using CatalogManager.AppService.Dtos;

namespace CatalogManager.Test
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class CategoryProductAppServiceTest
    {
        #region Category
        /// <summary>
        /// Gets the top level categories should return.
        /// </summary>
        [TestMethod]
        public void GetTopLevelCategoriesShouldReturn()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);

            // Act
            var categories = appService.GetTopLevelCategories();

            // Assert
            Assert.IsNotNull(categories);
            Assert.IsTrue(categories.Count() > 0);
            foreach (var category in categories)
            {
                Assert.IsNotNull(category);
                Assert.IsTrue(category.ParentCategoryId.HasValue == false);
            }
        }

        /// <summary>
        /// Gets the categories should return all.
        /// </summary>
        [TestMethod]
        public void GetCategoriesShouldReturnAll()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);

            // Act
            var categories = appService.GetCategories();

            // Assert
            Assert.IsNotNull(categories);
            Assert.IsTrue(categories.Count() > 0);
        }

        /// <summary>
        /// Gets the categories by parent return one.
        /// </summary>
        [TestMethod]
        public void GetCategoriesByParentReturnOne()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);
            int parentCategoryId = 1;

            // Act
            var categories = appService.GetCategoriesByParent(parentCategoryId);

            // Assert
            Assert.IsNotNull(categories);
            Assert.IsTrue(categories.Count() > 0);
        }

        /// <summary>
        /// Creates the category should create.
        /// </summary>
        [TestMethod]
        public void CreateCategoryShouldCreate()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);
            var categoryDto = new CategoryDto()
            {
                Name = "Test Category",
                ParentCategoryId = 1,
                Products = new List<ProductDto>()
            };

            // Act
            categoryDto = appService.CreateCategory(categoryDto);

            // Assert
            Assert.IsNotNull(categoryDto);
            Assert.IsTrue(categoryDto.Id > 0);
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
            int id = 1;

            // Act
            var category = appService.GetCategoryById(id);

            // Assert
            Assert.IsNotNull(category);
            Assert.IsTrue(!string.IsNullOrEmpty(category.Name));
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
            string changedName = string.Format("Name changed at {0}", DateTime.Now);
            int id = 1;
            var category = appService.GetCategoryById(id);

            // Act
            Assert.IsTrue(category.Name != changedName);
            category.Name = changedName;
            category = appService.UpdateCategory(category);

            // Assert
            Assert.IsNotNull(category);
            Assert.IsTrue(!string.IsNullOrEmpty(category.Name));
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
            var categoryDto = new CategoryDto()
            {
                Name = "Test Category to Delete",
                ParentCategoryId = 1,
                Products = new List<ProductDto>()
            };
            categoryDto = appService.CreateCategory(categoryDto);
            int createdId = categoryDto.Id;
            Assert.IsTrue(createdId > 0);

            // Act
            appService.DeleteCategory(createdId);
            var deletedCategoryDto = appService.GetCategoryById(createdId);

            // Assert
            Assert.IsNull(deletedCategoryDto);
        }
        
        #endregion

        #region Product

        /// <summary>
        /// Creates the product should create.
        /// </summary>
        [TestMethod]
        public void CreateProductShouldCreate()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);
            var dto = new ProductDto()
            {
                Name = "Test product",
                Description = "",
                Price = 100.00M,
                CategoryId = 1
            };

            // Act
            dto = appService.CreateProduct(dto);

            // Assert
            Assert.IsNotNull(dto);
            Assert.IsTrue(dto.Id > 0);
        }

        /// <summary>
        /// Gets the products by category should return one or more.
        /// </summary>
        [TestMethod]
        public void GetProductsByCategoryShouldReturnOneOrMore()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);
            int categoryId = 1;

            // Act
            var products = appService.GetProductsByCategory(categoryId);

            // Assert
            Assert.IsNotNull(products);
            Assert.IsTrue(products.Count() > 0);
        }

        /// <summary>
        /// Gets the product by identifier should return one.
        /// </summary>
        [TestMethod]
        public void GetProductByIdShouldReturnOne()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);
            int id = 1;

            // Act
            var product = appService.GetProductById(id);

            // Assert
            Assert.IsNotNull(product);
            Assert.IsTrue(product.Id  > 0);
        }

        /// <summary>
        /// Updates the product should update.
        /// </summary>
        [TestMethod]
        public void UpdateProductShouldUpdate()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);
            string changedName = string.Format("Name changed at {0}", DateTime.Now);
            string changedDescription = "Changed Description";
            var product = appService.GetProductById(1);
            var price = 11.11M;

            // Act
            Assert.IsTrue(product.Name != changedName);
            product.Name = changedName;
            product.Description = changedDescription;
            product.Price = price;
            product = appService.UpdateProduct(product);

            // Assert
            Assert.IsNotNull(product);
            Assert.IsTrue(!string.IsNullOrEmpty(product.Name));
            Assert.IsTrue(product.Name == changedName);
            Assert.AreEqual(product.Price, price);
            Assert.IsTrue(product.Description == changedDescription);
        }

        /// <summary>
        /// Deletes the product should delete.
        /// </summary>
        [TestMethod]
        public void DeleteProductShouldDelete()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);
            var dto = new ProductDto()
            {
                Name = "Test product to delete",
                Description = "",
                Price = 100.00M,
                CategoryId = 1
            };
            dto = appService.CreateProduct(dto);
            int createdId = dto.Id;
            Assert.IsTrue(createdId > 0);

            // Act
            appService.DeleteProduct(createdId);
            var deletedProductDto = appService.GetProductById(createdId);

            // Assert
            Assert.IsNull(deletedProductDto);
        }


        #endregion
     
    }
}
