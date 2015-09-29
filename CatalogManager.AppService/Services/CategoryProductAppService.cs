using CatalogManager.AppService.Dtos;
using CatalogManager.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// TO DO
        /// </summary>
        /// <param name="category"></param>
        public void CreateCategory(CategoryDto category)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// TO DO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CategoryDto GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        public void UpdateCategory(CategoryDto category)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        public void DeleteCategory(CategoryDto category)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        public void CreateProduct(ProductDto product)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductDto GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        public void UpdateProduct(ProductDto product)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        public void DeleteProduct(ProductDto product)
        {
            throw new NotImplementedException();
        }
    }
}
