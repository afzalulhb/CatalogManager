
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
namespace CatalogManager.Infrastructure
{
    public class CatalogManagerContext : DbContext
    {
        public CatalogManagerContext()
            : base("CatalogManager") 
        {
            // Prevent checking model change. We are aware of any change
            Database.SetInitializer<CatalogManagerContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
