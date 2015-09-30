using CatalogManager.AppService.Dtos;
using CatalogManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogManager.AppService.Helpers
{
    /// <summary>
    /// One-way mapper, from entity to dto
    /// </summary>
    public class CategoryProductAdapter : ITypeAdapter
    {     
               
        public TTarget Adapt<TTarget>(object source) where TTarget : class,new()
        {
            if (typeof(TTarget) == typeof(CategoryDto))
            {
                var category = source as Category;
                return (TTarget)Convert.ChangeType(ConvertCategoryToCategoryDto(category), typeof(TTarget));
            }
            else if (typeof(TTarget) == typeof(ProductDto))
            {
                var product = source as Product;
                return (TTarget)Convert.ChangeType(ConvertProductToProductDto(product), typeof(TTarget));

            }

            else if (typeof(TTarget) == typeof(List<CategoryDto>))
            {
                var result = ConvertCategoryListToCategoryDtoList((IEnumerable<Category>)source);
                return result as TTarget;
            }
            else if (typeof(TTarget) == typeof(List<ProductDto>))
            {
                return (TTarget)Convert.ChangeType(ConvertProductListToProductDtoList((List<Product>)source), typeof(TTarget));
            }
            else
            {
                throw new Exception("This type of conversion is not allowed");
            }

        }


        private CategoryDto ConvertCategoryToCategoryDto(Category category)
        {
            if (category == null)
            {
                return null;
            }

            var result = new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
                ParentCategoryId = category.ParentCategoryId,
                Products = ConvertProductListToProductDtoList(category.Products).ToList()
            };

            return result;
        }

        private IEnumerable<CategoryDto> ConvertCategoryListToCategoryDtoList(IEnumerable<Category> categories)
        {
            var lst = new List<CategoryDto>();
            if (categories.Any())
            {
                foreach (var item in categories)
                {
                    var dto = ConvertCategoryToCategoryDto(item);
                    lst.Add(dto);
                }
            }
            return lst;            
        }

        private ProductDto ConvertProductToProductDto(Product product)
        {
            if (product == null)
            {
                return null;
            }
            var result = new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId
            };
            return result;
        }

        private IEnumerable<ProductDto> ConvertProductListToProductDtoList(IEnumerable<Product> products)
        {
            var lst = new List<ProductDto>();
            if (products.Any())
            {
                foreach (var item in products)
                {
                    var dto = ConvertProductToProductDto(item);
                    lst.Add(dto);
                }
            }
            return lst;
        }
    }
}
