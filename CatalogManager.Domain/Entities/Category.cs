using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogManager.Domain.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent category identifier.
        /// </summary>
        /// <value>
        /// The parent category identifier.
        /// </value>
        public int? ParentCategoryId { get; set; }
        /// <summary>
        /// Gets or sets the parent category.
        /// </summary>
        /// <value>
        /// The parent category.
        /// </value>
        public virtual Category ParentCategory { get; set; }
        
        /// <summary>
        /// Gets or sets the child categories.
        /// </summary>
        /// <value>
        /// The child categories.
        /// </value>
        public virtual ICollection<Category> ChildCategories { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public virtual ICollection<Product> Products { get; set; }

        /// <summary>
        /// Adds the product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        /// <summary>
        /// Removes the product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void RemoveProduct(Product product)
        {
            Products.Remove(product);
        }

    }
}
