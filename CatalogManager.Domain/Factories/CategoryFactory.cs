using CatalogManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogManager.Domain.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public class CategoryFactory : ICategoryFactory
    {
        /// <summary>
        /// Creates the category.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="parentCategory">The parent category.</param>
        /// <param name="products">The products.</param>
        /// <returns></returns>
        public Category CreateCategory(string name, Category parentCategory, List<Product> products)
        {
            return new Category()
            {
                Name = name,
                ParentCategory = parentCategory,
                Products = products
            };
        }
    }
}
