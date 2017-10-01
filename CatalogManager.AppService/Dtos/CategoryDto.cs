using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogManager.AppService.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class CategoryDto
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
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public List<ProductDto> Products { get; set; }
        /// <summary>
        /// Gets or sets the child categories.
        /// </summary>
        /// <value>
        /// The child categories.
        /// </value>
        public List<CategoryDto> ChildCategories { get; set; }
    }
}
