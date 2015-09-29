
using CatalogManager.Domain.Entities;
using System.Collections.Generic;
namespace CatalogManager.Domain.Factories
{
    public interface  ICategoryFactory
    {
        Category CreateCategory(string name, Category parentCategory, List<Product> products);
    }
}
