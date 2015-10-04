using CatalogManager.AppService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogManager.AppService.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICategoryProductAppService
    {
        //CRUD Categories
        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
        /// <summary>
        /// Gets the top level categories.
        /// </summary>
        /// <returns></returns>
        
        Task<IEnumerable<CategoryDto>> GetTopLevelCategoriesAsync();
        /// <summary>
        /// Gets the category hierarchy.
        /// </summary>
        /// <returns></returns>
        
        Task<IEnumerable<CategoryDto>> GetCategoryHierarchyAsync();
        /// <summary>
        /// Gets the categories by parent.
        /// </summary>
        /// <param name="parentId">The parent identifier.</param>
        /// <returns></returns>
        Task<IEnumerable<CategoryDto>> GetCategoriesByParentAsync(int parentId);
        /// <summary>
        /// Creates the category.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        Task<CategoryDto> CreateCategoryAsync(CategoryDto dto);
        /// <summary>
        /// Gets the category by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        CategoryDto GetCategoryById(int id);
        /// <summary>
        /// Updates the category.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        CategoryDto UpdateCategory(CategoryDto dto);
        /// <summary>
        /// Deletes the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteCategory(int id);

        //CRUD Products
        /// <summary>
        /// Gets the products by category.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        IEnumerable<ProductDto> GetProductsByCategory(int categoryId);
        /// <summary>
        /// Creates the product.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        ProductDto CreateProduct(ProductDto dto);
        /// <summary>
        /// Gets the product by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        ProductDto GetProductById(int id);
        /// <summary>
        /// Updates the product.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        ProductDto UpdateProduct(ProductDto dto);
        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteProduct(int id);
    }
}
