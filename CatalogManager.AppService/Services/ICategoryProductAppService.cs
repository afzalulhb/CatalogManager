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
        CategoryDto CreateCategory(CategoryDto dto);
        CategoryDto GetCategoryById(int id);
        CategoryDto UpdateCategory(CategoryDto dto);
        void DeleteCategory(int id);

        //CRUD Products
        IEnumerable<ProductDto> GetProductsByCategory(int categoryId);
        ProductDto CreateProduct(ProductDto dto);
        ProductDto GetProductById(int id);
        ProductDto UpdateProduct(ProductDto dto);
        void DeleteProduct(int id);
    }
}
