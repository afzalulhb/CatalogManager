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
                var result = new ProductDto() { Id = product.Id, Name = product.Name, Description = product.Description, Price= product.Price, Category = product.Category.ProjectedAs<CategoryDto>()};
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
                var result = new Product() { Id = dto.Id, Name = dto.Name, Description = dto.Description, Price = dto.Price, Category = dto.Category.ProjectedAs<Category>() };
                return (TTarget)Convert.ChangeType(result, typeof(TTarget));

            }

        }


        public TTarget Adapt<TTarget>(object source) where TTarget : class,new()
        {
            if (source.GetType() == typeof(Category))
            {
                var categories = (List<Category>)source;
                var dtos = new List<CategoryDto>();

                foreach (var item in categories)
                {
                    var dto = item.ProjectedAs<CategoryDto>();
                    dtos.Add(dto);
                }
                return (TTarget)Convert.ChangeType(dtos, typeof(TTarget));
            }
            else if (source.GetType() == typeof(Product))
            {
                var products = (List<Product>)source;
                var dtos = new List<ProductDto>();

                foreach (var item in products)
                {
                    var dto = item.ProjectedAs<ProductDto>();
                    dtos.Add(dto);
                }
                return (TTarget)Convert.ChangeType(dtos, typeof(TTarget));
            }
            else if (source.GetType() == typeof(CategoryDto))
            {
                var categories = new List<Category>();
                var dtos = (List<CategoryDto>)source;

                foreach (var item in dtos)
                {
                    var category = item.ProjectedAs<Category>();
                    categories.Add(category);
                }
                return (TTarget)Convert.ChangeType(categories, typeof(TTarget));
            }
            else
            {
                var dtos = (List<ProductDto>)source;
                var products = new List<Product>();

                foreach (var item in dtos)
                {
                    var product = item.ProjectedAs<Product>();
                    products.Add(product);
                }
                return (TTarget)Convert.ChangeType(products, typeof(TTarget));
            }


        }
    }
}
