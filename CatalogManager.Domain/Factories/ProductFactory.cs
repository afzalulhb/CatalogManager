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
    public class ProductFactory : IProductFactory
    {
        /// <summary>
        /// Creates the product.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="price">The price.</param>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        public Product CreateProduct(string name, string description, decimal price, Category category)
        {
            return new Product()
            {
                Name = name,
                Description = description,
                Price = price,
                Category = category
            };
        }
    }
}
