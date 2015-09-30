using CatalogManager.AppService.Dtos;
using CatalogManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogManager.AppService.Helpers
{
    public class CategoryProductAdapter : ITypeAdapter
    {
        public TTarget Adapt<TSource, TTarget>(TSource source, TTarget target)
            where TSource : class
            where TTarget : class, new()
        {
            if (source.GetType() == typeof(Category))
            {
                var category = source as Category;
                var result = new CategoryDto() { Id = category.Id, Name = category.Name, ParentCategory = category.ProjectedAs<CategoryDto>(), Products = category.Products.ProjectedAsCollection<ProductDto>() };
                return (TTarget)Convert.ChangeType(result, typeof(TTarget));
            }
            else if (source.GetType() == typeof(Product))
            {
                var product = source as Product;
                var result = new ProductDto() { Id = product.Id, Name = product.Name, Description = product.Description, Price= product.Price, CategoryId = product.CategoryId};
                return (TTarget)Convert.ChangeType(result, typeof(TTarget));

            }
            else if (source.GetType() == typeof(CategoryDto))
            {
                var categoryDto = source as CategoryDto;
                var result = new Category() { Id = categoryDto.Id, Name = categoryDto.Name, ParentCategory = categoryDto.ProjectedAs<Category>(), Products = categoryDto.Products.ProjectedAsCollection<Product>() };
                return (TTarget)Convert.ChangeType(result, typeof(TTarget));
            }
            else
            {
                var dto = source as ProductDto;
                var result = new Product() { Id = dto.Id, Name = dto.Name, Description = dto.Description, Price = dto.Price, CategoryId = dto.CategoryId };
                return (TTarget)Convert.ChangeType(result, typeof(TTarget));

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
                ParentCategory = category.ProjectedAs<CategoryDto>(),
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
            //if (categories.Any())
            //{
            //    foreach (var item in categories)
            //    {
            //        var dto = ConvertCategoryToCategoryDto(item);
            //        yield return dto;
            //    }
            //}
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
            //if (products.Any())
            //{
            //    foreach (var item in products)
            //    {
            //        var dto = ConvertProductToProductDto(item);
            //        yield return dto;
            //    }
            //}
        }
        private Category ConvertCategoryDtoToCategory(CategoryDto dto)
        {
            if (dto == null)
            {
                return null;
            }
            var result = new Category()
            {
                Id = dto.Id,
                Name = dto.Name,
                ParentCategory = dto.ProjectedAs<Category>(),
               // Products = dto.Products.ProjectedAsCollection<Product>()
            };
            return result;
        }
        private Product ConvertProductDtoToProduct(ProductDto dto)
        {
            if (dto == null)
            {
                return null;
            }
            var result = new Product()
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.CategoryId
            };
            return result;
        }
        //TO DO : Refactor

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
            else if (typeof(TTarget) == typeof(Category))
            {
                var categoryDto = source as CategoryDto;
                return (TTarget)Convert.ChangeType(ConvertCategoryDtoToCategory(categoryDto), typeof(TTarget));
            }
            else if (typeof(TTarget) == typeof(Product))
            {
                var dto = source as ProductDto;
                return (TTarget)Convert.ChangeType(ConvertProductDtoToProduct(dto), typeof(TTarget));

            }

            else if (typeof(TTarget) == typeof(List<CategoryDto>))
            {
                var result = ConvertCategoryListToCategoryDtoList((IEnumerable<Category>)source);
                return result as TTarget;
                //return (TTarget)Convert.ChangeType(result, typeof(TTarget));
            }
            else if (typeof(TTarget) == typeof(List<ProductDto>))
            {
                return (TTarget)Convert.ChangeType(ConvertProductListToProductDtoList((List<Product>)source), typeof(TTarget));
            }
            else if (typeof(TTarget) == typeof(List<Category>))
            {
                var categories = new List<Category>();
                var dtos = (List<CategoryDto>)source;

                foreach (var item in dtos)
                {
                    var category = ConvertCategoryDtoToCategory(item);
                    categories.Add(category);
                }
                return (TTarget)Convert.ChangeType(categories, typeof(TTarget));
            }
            else if (typeof(TTarget) == typeof(List<Product>))
            {
                var dtos = (List<ProductDto>)source;
                var products = new List<Product>();

                foreach (var item in dtos)
                {
                    var product = ConvertProductDtoToProduct(item);
                    products.Add(product);
                }
                return (TTarget)Convert.ChangeType(products, typeof(TTarget));
            }
            else
            {
                return null;
            }

        }
    }
}
