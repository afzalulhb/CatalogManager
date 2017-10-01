using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace CatalogManager.Infrastructure.CrossCutting
{
    public class CatalogManagerSettings : ConfigurationSection
    {
        private static CatalogManagerSettings settings = ConfigurationManager.GetSection("CatalogManagerSettings") as CatalogManagerSettings;

        public static CatalogManagerSettings Settings
        {
            get { return settings; }
        }
        [ConfigurationProperty("assemblies", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(AssemblyCollection),
           AddItemName = "add",
           ClearItemsName = "clear",
           RemoveItemName = "remove")]
        public AssemblyCollection Assemblies
        {
            get { return (AssemblyCollection)base["assemblies"]; }
        }

    }
}
