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
    public class CategoryProductAppService : ICategoryProductAppService
    {
        IUnitOfWork unitOfWork;

        public CategoryProductAppService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Returns top level Categories
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CategoryDto> GetTopLevelCategories()
        {
            var categories = unitOfWork.Categories.GetAll().Where(x => x.ParentCategory == null);
            return categories.ProjectedAsCollection<CategoryDto>();
        }

        public IEnumerable<CategoryDto> GetCategoriesByParent(int parentId)
        {
            var categories = unitOfWork.Categories.GetAll().Where(x => x.ParentCategoryId == parentId);
            return categories.ProjectedAsCollection<CategoryDto>();
        }

        public IEnumerable<CategoryDto> GetCategories()
        {

            var categories = unitOfWork.Categories.GetAll();
            return categories.ProjectedAsCollection<CategoryDto>();
        }

        public CategoryDto CreateCategory(CategoryDto dto)
        {
            ICategoryFactory factory = new CategoryFactory();
            var parentCategory = new Category();
            var productList = new List<Product>();
            if(dto.ParentCategoryId.HasValue)
            {
                parentCategory = unitOfWork.Categories.GetById((int)dto.ParentCategoryId);
            }

            var category = factory.CreateCategory(dto.Name, parentCategory, productList);

            unitOfWork.Categories.Insert(category);
            unitOfWork.Save();

            return category.ProjectedAs<CategoryDto>();
        }

        public CategoryDto GetCategoryById(int id)
        {
            var category = unitOfWork.Categories.GetById(id);
            return category.ProjectedAs<CategoryDto>();
        }

        public CategoryDto UpdateCategory(CategoryDto dto)
        {
            var category = unitOfWork.Categories.GetById(dto.Id);
            MaterializeCategory(category, dto);
            unitOfWork.Categories.Update(category);
            unitOfWork.Save();
            return category.ProjectedAs<CategoryDto>();

        }
        
        public void DeleteCategory(int id)
        {
            var category = unitOfWork.Categories.GetById(id);
            unitOfWork.Categories.Delete(category);
            unitOfWork.Save();
        }

        public ProductDto CreateProduct(ProductDto dto)
        {
            IProductFactory factory = new ProductFactory();
            var category = unitOfWork.Categories.GetById(dto.CategoryId);

            var product = factory.CreateProduct(dto.Name,dto.Description, dto.Price,category);

            unitOfWork.Products.Insert(product);
            unitOfWork.Save();

            return product.ProjectedAs<ProductDto>();
        }

        public ProductDto GetProductById(int id)
        {
            var product = unitOfWork.Products.GetById(id);
            return product.ProjectedAs<ProductDto>();
        }

        public ProductDto UpdateProduct(ProductDto dto)
        {
            var product = unitOfWork.Products.GetById(dto.Id);
            MaterializeProduct(product, dto); 
            unitOfWork.Products.Update(product);
            unitOfWork.Save();
            return product.ProjectedAs<ProductDto>();
        }

        public void DeleteProduct(int id)
        {
            var product = unitOfWork.Products.GetById(id);
            unitOfWork.Products.Delete(product);
            unitOfWork.Save();
        }
        
        public IEnumerable<ProductDto> GetProductsByCategory(int categoryId)
        {
            var products = unitOfWork.Products.GetAll().Where(x => x.CategoryId == categoryId).ToList();
            return products.ProjectedAsCollection<ProductDto>();
        }

        #region private methods

        private void MaterializeCategory(Category category, CategoryDto dto)
        {
            category.Name = dto.Name;
            if (dto.ParentCategoryId.HasValue)
            {
                var parent = unitOfWork.Categories.GetById((int)dto.ParentCategoryId);
                category.ParentCategory = parent;
            }
        }

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
