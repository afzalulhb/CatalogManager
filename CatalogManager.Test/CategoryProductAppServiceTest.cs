using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CatalogManager.AppService.Services;
using CatalogManager.Infrastructure.UnitOfWork;
using CatalogManager.Infrastructure;
using CatalogManager.AppService.Dtos;
using System.Threading.Tasks;

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
        public async Task GetTopLevelCategoriesShouldReturn()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);

            // Act
            var categories = await appService.GetTopLevelCategoriesAsync();

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
        public async Task GetCategoriesAsyncShouldReturnAll()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);

            // Act
            var categories = await appService.GetCategoriesAsync();

            // Assert
            Assert.IsNotNull(categories);
            Assert.IsTrue(categories.Count() > 0);
        }

        /// <summary>
        /// Gets the categories by parent return one.
        /// </summary>
        [TestMethod]
        public async Task GetCategoriesByParentReturnOne()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);
            int parentCategoryId = 1;

            // Act
            var categories = await appService.GetCategoriesByParentAsync(parentCategoryId);

            // Assert
            Assert.IsNotNull(categories);
            Assert.IsTrue(categories.Count() > 0);
        }

        /// <summary>
        /// Creates the category should create.
        /// </summary>
        [TestMethod]
        public async Task CreateCategoryShouldCreate()
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
            categoryDto = await appService.CreateCategoryAsync(categoryDto);

            // Assert
            Assert.IsNotNull(categoryDto);
            Assert.IsTrue(categoryDto.Id > 0);
        }

        /// <summary>
        /// Gets the category by identifier should return one.
        /// </summary>
        [TestMethod]
        public async Task GetCategoryByIdShouldReturnOne()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);
            int id = 1;

            // Act
            var category = await appService.GetCategoryByIdAsync(id);

            // Assert
            Assert.IsNotNull(category);
            Assert.IsTrue(!string.IsNullOrEmpty(category.Name));
        }

        /// <summary>
        /// Updates the category should update.
        /// </summary>
        [TestMethod]
        public async Task UpdateCategoryShouldUpdate()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);
            string changedName = string.Format("Name changed at {0}", DateTime.Now);
            int id = 1;
            var category = await appService.GetCategoryByIdAsync(id);

            // Act
            Assert.IsTrue(category.Name != changedName);
            category.Name = changedName;
            category = await appService.UpdateCategoryAsync(category);

            // Assert
            Assert.IsNotNull(category);
            Assert.IsTrue(!string.IsNullOrEmpty(category.Name));
            Assert.IsTrue(category.Name == changedName);
        }

        /// <summary>
        /// Deletes the category should delete.
        /// </summary>
        [TestMethod]
        public async Task DeleteCategoryShouldDelete()
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
            categoryDto = await appService.CreateCategoryAsync(categoryDto);
            int createdId = categoryDto.Id;
            Assert.IsTrue(createdId > 0);

            // Act
            appService.DeleteCategory(createdId);
            var deletedCategoryDto = await appService.GetCategoryByIdAsync(createdId);

            // Assert
            Assert.IsNull(deletedCategoryDto);
        }
        
        #endregion

        #region Product

        /// <summary>
        /// Creates the product should create.
        /// </summary>
        [TestMethod]
        public async Task CreateProductShouldCreate()
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
            dto = await appService.CreateProductAsync(dto);

            // Assert
            Assert.IsNotNull(dto);
            Assert.IsTrue(dto.Id > 0);
        }

        /// <summary>
        /// Gets the products by category should return one or more.
        /// </summary>
        [TestMethod]
        public async Task GetProductsByCategoryShouldReturnOneOrMore()
        {
            // Arrange 
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            ICategoryProductAppService appService = new CategoryProductAppService(unitOfWork);
            int categoryId = 1;

            // Act
            var products = await appService.GetProductsByCategoryAsync(categoryId);

            // Assert
            Assert.IsNotNull(products);
            Assert.IsTrue(products.Count() > 0);
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
            int id = 1;

            // Act
            var product = await appService.GetProductByIdAsync(id);

            // Assert
            Assert.IsNotNull(product);
            Assert.IsTrue(product.Id  > 0);
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
            string changedName = string.Format("Name changed at {0}", DateTime.Now);
            string changedDescription = "Changed Description";
            int id = 1;
            var product = await appService.GetProductByIdAsync(id);
            var price = 11.11M;

            // Act
            Assert.IsTrue(product.Name != changedName);
            product.Name = changedName;
            product.Description = changedDescription;
            product.Price = price;
            product = await appService.UpdateProductAsync(product);

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
        public async Task DeleteProductShouldDelete()
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
            dto = await appService.CreateProductAsync(dto);
            int createdId = dto.Id;
            Assert.IsTrue(createdId > 0);

            // Act
            await appService.DeleteProductAsync(createdId);
            var deletedProductDto = await appService.GetProductByIdAsync(createdId);

            // Assert
            Assert.IsNull(deletedProductDto);
        }


        #endregion
     
    }
}
