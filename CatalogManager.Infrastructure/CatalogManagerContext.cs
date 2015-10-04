
using CatalogManager.Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WebApplication1.Models.Mapping;
using CatalogManager.Infrastructure.UnitOfWork;
namespace CatalogManager.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class CatalogManagerContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogManagerContext"/> class.
        /// </summary>
        public CatalogManagerContext()
            : base("CatalogManager") 
        {
            // Crate db if not exists
            Database.SetInitializer<CatalogManagerContext>(new CatalogManagerDBInitializer());       
            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseAlways<SchoolDBContext>());
     
            //Database.SetInitializer<CatalogManagerContext>(new DropCreateDatabaseIfModelChanges<CatalogManagerContext>());
            
            // Prevent checking model change. We are aware of any change
            //Database.SetInitializer<CatalogManagerContext>(null);
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new ProductMap());
        }
    }
    public class CatalogManagerDBInitializer : CreateDatabaseIfNotExists<CatalogManagerContext>
    {
        protected override void Seed(CatalogManagerContext context)
        {
            SeedCategory(context);
            base.Seed(context);
        }

        private void SeedCategory(CatalogManagerContext context)
        {
            IList<Category> categories = new List<Category>(){
            new Category()
            {
                Name="Computers", 
                ChildCategories= new List<Category>()
                {
                    new Category()
                    {
                        Name="Desktop-Computers",
                        Products= new List<Product>()
                        {
                            new Product ()
                            {
                                Name="Lenovo Horizon II 27in Portable Desktop i5 1.7GHz 8GB 1TB WiFi",
                                Description="Lenovo Horizon II 27in Portable Desktop i5 1.7GHz 8GB 1TB WiFi",
                                Price=1091.00M
                            },
                            new Product ()
                            {
                                Name="Acer Aspire E 15.6\" Laptop - Black/Iron ",
                                Description="Acer Aspire E 15.6\" Laptop - Black/Iron ",
                                Price=599.99M
                            },
                            new Product ()
                            {
                                Name="HP Pavilion 15.6\" Laptop - Silver (Intel Core i5-5200U / 1TB HDD / 16GB RAM / Windows 8.1)",
                                Description="HP Pavilion 15.6\" Laptop - Silver (Intel Core i5-5200U / 1TB HDD / 16GB RAM / Windows 8.1)",
                                Price=729.99M
                            },
                            new Product ()
                            {
                                Name="ASUS 15.6\" Laptop - Black (Intel Pentium N3540 / 1TB HDD / 8GB RAM / Windows 8.1)",
                                Description="ASUS 15.6\" Laptop - Black (Intel Pentium N3540 / 1TB HDD / 8GB RAM / Windows 8.1)",
                                Price=479.99M
                            },
                            new Product ()
                            {
                                Name="Acer One 10.1\" Touch Convertible Laptop-Iron (Intel Atom Z3735F/32GB eMMc/2GB RAM)",
                                Description="Acer One 10.1\" Touch Convertible Laptop-Iron (Intel Atom Z3735F/32GB eMMc/2GB RAM)",
                                Price=299.99M
                            }
                        }
                    }
                }
            },
            new Category(){Name="Gaming"},
            new Category(){Name="Audio & Video"},
            new Category(){Name="Electronics"}
            };

            IUnitOfWork unitOfWork = new UnitOfWork.UnitOfWork(context);

            foreach (Category cat in categories)
            {
                unitOfWork.Categories.Insert(cat);
            }
            unitOfWork.Save();
        }
    }
}
