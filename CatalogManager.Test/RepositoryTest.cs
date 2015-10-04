using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CatalogManager.Infrastructure.UnitOfWork;
using CatalogManager.Infrastructure;
using CatalogManager.Domain.Entities;
using System.Threading.Tasks;

namespace CatalogManager.Test
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class RepositoryTest
    {

        /// <summary>
        /// Gets the category by identifier should return one.
        /// </summary>
        [TestMethod]
        public void GetCategoryByIdShouldReturnOne()
        {
            // Arrange
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);

            // Act
            var category = unitOfWork.Categories.GetById(4);
            
            // Assert
            Assert.IsNotNull(category);
            Assert.IsTrue(category.Id > 0);
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
            var category = new Category() { Name = "Test Category", ParentCategory = null, Products = null };

            // Act
            unitOfWork.Categories.Insert(category);
            unitOfWork.Save();

            // Assert
            Assert.IsNotNull(category);
            Assert.IsTrue(category.Id > 0);
        }

        /// <summary>
        /// Creates the child category should create one.
        /// </summary>
        [TestMethod]
        public async Task CreateChildCategoryShouldCreateOne()
        {
            // Arrange
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            int id = 4;
            var parentCategory = await unitOfWork.Categories.GetByIdAsync(id);
            var category = new Category() { Name = "Test Category", ParentCategory = parentCategory, Products = null };

            // Act
            unitOfWork.Categories.Insert(category);
            await unitOfWork.SaveAsync();

            // Assert
            Assert.IsNotNull(category);
            Assert.IsTrue(category.Id > 0);
            Assert.IsTrue(category.ParentCategory != null);
        }

        /// <summary>
        /// Gets all categories should return all.
        /// </summary>
        [TestMethod]
        public void GetAllCategoriesShouldReturnAll()
        {
            // Arrange
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);

            // Act
            var categories = unitOfWork.Categories.GetAll();

            // Assert
            Assert.IsNotNull(categories);
            Assert.IsTrue(categories.Count() > 0);
        }

        /// <summary>
        /// Deletes the category should succeed.
        /// </summary>
        [TestMethod]
        public void DeleteCategoryShouldSucceed()
        {
            // Arrange
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            var name = DateTime.Now.ToString();
            var category = new Category() { Name = name, ParentCategory = null, Products = null };
            unitOfWork.Categories.Insert(category);
            unitOfWork.Save();
            var savedCategory = unitOfWork.Categories.GetAll().FirstOrDefault(x => x.Name == name);
            Assert.IsNotNull(savedCategory);

            // Act
            unitOfWork.Categories.Delete(savedCategory);
            unitOfWork.Save();
            var deletedCategory = unitOfWork.Categories.GetAll().FirstOrDefault(x => x.Name == name);

            // Assert
            Assert.IsNull(deletedCategory);
        }

        /// <summary>
        /// Edits the category should succeed.
        /// </summary>
        [TestMethod]
        public void EditCategoryShouldSucceed()
        {
            // Arrange
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            var category = unitOfWork.Categories.GetById(5);
            var name = category.Name;

            // Act
            category.Name = string.Format("Changed name at {0}", DateTime.Now);
            unitOfWork.Categories.Update(category);
            unitOfWork.Save();

            // Assert
            Assert.AreNotEqual(name, category.Name);
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

            // Act
            var product = unitOfWork.Products.GetById(4);

            // Assert
            Assert.IsNotNull(product);
            Assert.IsTrue(product.Id > 0);
        }

        /// <summary>
        /// Creates the product should create one.
        /// </summary>
        [TestMethod]
        public void CreateProductShouldCreateOne()
        {
            // Arrange
            // TO DO: Make sure Category with id = 4 exists in the db
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            var product = new Product() { Name = "Test Product",Description="This is a test product", CategoryId=4, Price=10.00M };

            // Act
            unitOfWork.Products.Insert(product);
            unitOfWork.Save();

            // Assert
            Assert.IsNotNull(product);
            Assert.IsTrue(product.Id > 0);
        }

        /// <summary>
        /// Gets all productss should return all.
        /// </summary>
        [TestMethod]
        public void GetAllProductssShouldReturnAll()
        {
            // Arrange
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);

            // Act
            var products = unitOfWork.Products.GetAll();

            // Assert
            Assert.IsNotNull(products);
            Assert.IsTrue(products.Count() > 0);
        }

        /// <summary>
        /// Edits the product should succeed.
        /// </summary>
        [TestMethod]
        public void EditProductShouldSucceed()
        {
            // Arrange
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            var product = unitOfWork.Products.GetById(5);
            var name = product.Name;

            // Act
            product.Name = string.Format("Changed name at {0}", DateTime.Now);
            unitOfWork.Products.Update(product);
            unitOfWork.Save();

            // Assert
            Assert.AreNotEqual(name, product.Name);
        }

        /// <summary>
        /// Deletes the product should succeed.
        /// </summary>
        [TestMethod]
        public void DeleteProductShouldSucceed()
        {
            // Arrange
            var context = new CatalogManagerContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            var name = DateTime.Now.ToString();
            var product = new Product() { Name = name, Description = "This is a test product", CategoryId = 4, Price = 10.00M };
            unitOfWork.Products.Insert(product);
            unitOfWork.Save();
            var savedProduct = unitOfWork.Products.GetAll().FirstOrDefault(x => x.Name == name);
            Assert.IsNotNull(savedProduct);

            // Act
            unitOfWork.Products.Delete(savedProduct);
            unitOfWork.Save();
            var deletedProduct = unitOfWork.Products.GetAll().FirstOrDefault(x => x.Name == name);

            // Assert
            Assert.IsNull(deletedProduct);
        }
       
    }
}
