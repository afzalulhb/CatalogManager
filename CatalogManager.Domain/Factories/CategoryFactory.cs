using CatalogManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogManager.Domain.Factories
{
    public class CategoryFactory : ICategoryFactory
    {
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
