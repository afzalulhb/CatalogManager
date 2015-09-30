using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CatalogManager.AppService.Services;
using CatalogManager.Infrastructure.UnitOfWork;
using CatalogManager.Infrastructure;

namespace CatalogManager.Test
{
    [TestClass]
    public class CategoryProductAppServiceTest
    {
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
                Assert.IsNull(category.ParentCategory);
            }
        }


        ////CRUD Categories
        //IEnumerable<CategoryDto> GetTopLevelCategories()
        //IEnumerable<CategoryDto> GetCategories();
        //IEnumerable<CategoryDto> GetCategoriesByParent(int parentId);
        //void CreateCategory(CategoryDto category);
        //CategoryDto GetCategoryById(int id);
        //void UpdateCategory(CategoryDto category);
        //void DeleteCategory(CategoryDto category);

        ////CRUD Products
        //IEnumerable<ProductDto> GetProductsByCategory(int categoryId);
        //void CreateProduct(ProductDto product);
        //ProductDto GetProductById(int id);
        //void UpdateProduct(ProductDto product);
        //void DeleteProduct(ProductDto product);

        //[TestMethod]
        //public void CreateCategoryShouldReturnOne()
        //{
        //    // Arrange
        //    ICategoryFactory factory = new CategoryFactory();
        //    string name = "Factory Category Test";
        //    var parent = new Category();
        //    var products = new List<Product>();

        //    // Act
        //    var category = factory.CreateCategory(name, parent, products);

        //    // Assert
        //    Assert.IsNotNull(category);
        //    Assert.AreEqual(name, category.Name);
        //}
    }
}
