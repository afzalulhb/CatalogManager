using CatalogManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogManager.Domain.Factories
{
    public class ProductFactory : IProductFactory
    {
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
