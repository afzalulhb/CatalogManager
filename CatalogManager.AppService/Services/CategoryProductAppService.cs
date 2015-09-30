using CatalogManager.AppService.Dtos;
using CatalogManager.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatalogManager.AppService.Helpers;

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
            var categories = unitOfWork.Categories.GetAll().Where(x => x.ParentCategory == null).ToList();
            return categories.ProjectedAsCollection<CategoryDto>();
        }

        public IEnumerable<CategoryDto> GetCategoriesByParent(int parentId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryDto> GetCategories()
        {
            throw new NotImplementedException();
        }
        
        public void CreateCategory(CategoryDto category)
        {
            throw new NotImplementedException();
        }

        public CategoryDto GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateCategory(CategoryDto category)
        {
            throw new NotImplementedException();
        }

        public void DeleteCategory(CategoryDto category)
        {
            throw new NotImplementedException();
        }

        public void CreateProduct(ProductDto product)
        {
            throw new NotImplementedException();
        }

        public ProductDto GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(ProductDto product)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(ProductDto product)
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<ProductDto> GetProductsByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

    }
}
