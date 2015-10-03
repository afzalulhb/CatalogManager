using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CatalogManager.Domain.Factories;
using CatalogManager.Domain.Entities;
using System.Collections.Generic;

namespace CatalogManager.Test
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class FactoryTest
    {
        /// <summary>
        /// Creates the category should return one.
        /// </summary>
        [TestMethod]
        public void CreateCategoryShouldReturnOne()
        {
            // Arrange
            ICategoryFactory factory = new CategoryFactory();
            string name = "Factory Category Test";
            var parent = new Category();
            var products = new List<Product>();

            // Act
            var category = factory.CreateCategory(name, parent, products);

            // Assert
            Assert.IsNotNull(category);
            Assert.AreEqual(name, category.Name);
        }

        /// <summary>
        /// Creates the product should return one.
        /// </summary>
        [TestMethod]
        public void CreateProductShouldReturnOne()
        {
            // Arrange
            IProductFactory factory = new ProductFactory();
            string name = "Factory Product Test";
            string description = "Factory Product Test description";
            var price = 22.22M;
            var category = new Category();

            // Act
            var product = factory.CreateProduct(name, description, price, category);

            // Assert
            Assert.IsNotNull(product);
            Assert.AreEqual(name, product.Name);
            Assert.AreEqual(description, product.Description);
            Assert.AreEqual(price, product.Price);
        }
    }
}
