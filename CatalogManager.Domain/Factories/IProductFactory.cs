using CatalogManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogManager.Domain.Factories
{
    public interface IProductFactory
    {
        Product CreateProduct(string name, string description, decimal price, Category category);
    }
}
