using CatalogManager.AppService.Dtos;
using CatalogManager.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatalogManager.AppService.Helpers;
using CatalogManager.Domain.Factories;
using CatalogManager.Domain.Entities;

namespace CatalogManager.AppService.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class CategoryProductAppService : ICategoryProductAppService
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryProductAppService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public CategoryProductAppService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Returns top level Categories
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CategoryDto>> GetTopLevelCategoriesAsync()
        {
            var categories = (await unitOfWork.Categories.GetAllAsync()).Where(x => x.ParentCategory == null);
            return categories.ProjectedAsCollection<CategoryDto>();
        }
        /// <summary>
        /// Gets the category hierarchy.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CategoryDto>> GetCategoryHierarchyAsync()
        {
            var categories = (await unitOfWork.Categories.GetAllAsync()).Where(x => x.ParentCategory == null);
            var categoryDtos = categories.ProjectedAsCollection<CategoryDto>();
            BuildCategoryHierarchy(categoryDtos);
            return categoryDtos;
        }

        /// <summary>
        /// Builds the category hierarchy.
        /// </summary>
        /// <param name="categories">The categories.</param>
        private void BuildCategoryHierarchy(IEnumerable<CategoryDto> categories)
        {
            foreach (var cat in categories)
            {
                var childCategories = unitOfWork.Categories.GetAll().Where(x => x.ParentCategoryId == cat.Id);
                var dtos = childCategories.ProjectedAsCollection<CategoryDto>();
                cat.ChildCategories = dtos;
            }
        }

        /// <summary>
        /// Gets the categories by parent.
        /// </summary>
        /// <param name="parentId">The parent identifier.</param>
        /// <returns></returns>
        public async Task<IEnumerable<CategoryDto>> GetCategoriesByParentAsync(int parentId)
        {
            var categories = (await unitOfWork.Categories.GetAllAsync()).Where(x => x.ParentCategoryId == parentId);
            return categories.ProjectedAsCollection<CategoryDto>();
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {

            var categories = await unitOfWork.Categories.GetAllAsync();
            return categories.ProjectedAsCollection<CategoryDto>();
        }

        /// <summary>
        /// Creates the category.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public async Task<CategoryDto> CreateCategoryAsync(CategoryDto dto)
        {
            ICategoryFactory factory = new CategoryFactory();
            var parentCategory = new Category();
            var productList = new List<Product>();

            parentCategory = dto.ParentCategoryId.HasValue ? await unitOfWork.Categories.GetByIdAsync((int)dto.ParentCategoryId) : null;

            var category = factory.CreateCategory(dto.Name, parentCategory, productList);

            unitOfWork.Categories.Insert(category);
            unitOfWork.SaveAsync();

            return category.ProjectedAs<CategoryDto>();
        }

        /// <summary>
        /// Gets the category by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public CategoryDto GetCategoryById(int id)
        {
            var category = unitOfWork.Categories.GetById(id);
            return category.ProjectedAs<CategoryDto>();
        }

        /// <summary>
        /// Updates the category.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public CategoryDto UpdateCategory(CategoryDto dto)
        {
            var category = unitOfWork.Categories.GetById(dto.Id);
            MaterializeCategory(category, dto);
            unitOfWork.Categories.Update(category);
            unitOfWork.Save();
            return category.ProjectedAs<CategoryDto>();

        }

        /// <summary>
        /// Deletes the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteCategory(int id)
        {
            var category = unitOfWork.Categories.GetById(id);

            // Delete children first
            var children = unitOfWork.Categories.GetAll().Where(x => x.ParentCategoryId == id);
            foreach (var cat in children)
            {
                unitOfWork.Categories.Delete(cat);
            }

            unitOfWork.Categories.Delete(category);
            unitOfWork.Save();
        }

        /// <summary>
        /// Creates the product.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public ProductDto CreateProduct(ProductDto dto)
        {
            IProductFactory factory = new ProductFactory();
            var category = unitOfWork.Categories.GetById(dto.CategoryId);

            var product = factory.CreateProduct(dto.Name,dto.Description, dto.Price,category);

            unitOfWork.Products.Insert(product);
            unitOfWork.Save();

            return product.ProjectedAs<ProductDto>();
        }

        /// <summary>
        /// Gets the product by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ProductDto GetProductById(int id)
        {
            var product = unitOfWork.Products.GetById(id);
            return product.ProjectedAs<ProductDto>();
        }

        /// <summary>
        /// Updates the product.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public ProductDto UpdateProduct(ProductDto dto)
        {
            var product = unitOfWork.Products.GetById(dto.Id);
            MaterializeProduct(product, dto); 
            unitOfWork.Products.Update(product);
            unitOfWork.Save();
            return product.ProjectedAs<ProductDto>();
        }

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteProduct(int id)
        {
            var product = unitOfWork.Products.GetById(id);
            unitOfWork.Products.Delete(product);
            unitOfWork.Save();
        }

        /// <summary>
        /// Gets the products by category.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        public IEnumerable<ProductDto> GetProductsByCategory(int categoryId)
        {
            var products = unitOfWork.Products.GetAll().Where(x => x.CategoryId == categoryId).ToList();
            return products.ProjectedAsCollection<ProductDto>();
        }

        #region private methods

        /// <summary>
        /// Materializes the category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="dto">The dto.</param>
        private void MaterializeCategory(Category category, CategoryDto dto)
        {
            category.Name = dto.Name;
            if (dto.ParentCategoryId.HasValue)
            {
                var parent = unitOfWork.Categories.GetById((int)dto.ParentCategoryId);
                category.ParentCategory = parent;
            }
        }

        /// <summary>
        /// Materializes the product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="dto">The dto.</param>
        private void MaterializeProduct(Product product, ProductDto dto)
        {
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
          
            var category = unitOfWork.Categories.GetById(dto.CategoryId);
            product.Category = category;
        }
        #endregion
    }
}
