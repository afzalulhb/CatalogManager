
using CatalogManager.Domain.Entities;
using System.Collections.Generic;
namespace CatalogManager.Domain.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public interface  ICategoryFactory
    {
        /// <summary>
        /// Creates the category.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="parentCategory">The parent category.</param>
        /// <param name="products">The products.</param>
        /// <returns></returns>
        Category CreateCategory(string name, Category parentCategory, List<Product> products);
    }
}
