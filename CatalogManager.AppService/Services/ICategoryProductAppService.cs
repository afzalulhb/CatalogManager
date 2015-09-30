using CatalogManager.AppService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogManager.AppService.Services
{
    public interface ICategoryProductAppService
    {
        //CRUD Categories
        IEnumerable<CategoryDto> GetCategories();
        IEnumerable<CategoryDto> GetTopLevelCategories();
        IEnumerable<CategoryDto> GetCategoriesByParent(int parentId);
        void CreateCategory(CategoryDto category);
        CategoryDto GetCategoryById(int id);
        void UpdateCategory(CategoryDto category);
        void DeleteCategory(CategoryDto category);

        //CRUD Products
        IEnumerable<ProductDto> GetProductsByCategory(int categoryId);
        void CreateProduct(ProductDto product);
        ProductDto GetProductById(int id);
        void UpdateProduct(ProductDto product);
        void DeleteProduct(ProductDto product);
    }
}
