using CatalogManager.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebApplication1.Models.Mapping
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Category");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ParentCategoryId).HasColumnName("ParentCategoryId");

            // Relationships
            this.HasOptional(t => t.ParentCategory)
                .WithMany(t => t.ChildCategories)
                .HasForeignKey(d => d.ParentCategoryId);

        }
    }
}
