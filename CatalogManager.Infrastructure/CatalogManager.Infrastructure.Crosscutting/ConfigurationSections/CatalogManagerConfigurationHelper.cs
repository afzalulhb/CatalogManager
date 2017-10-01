using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogManager.Infrastructure.CrossCutting
{
    public static class CatalogManagerConfigurationHelper
    {

        public static HashSet<string> GetAssembleyNames()
        {
            var asemblies = (CatalogManagerSettings)ConfigurationManager.GetSection("CatalogManagerSettings");
            return new HashSet<string>((from object assembly in asemblies.Assemblies select ((AssemblyElement)assembly).Name).ToList());
        }

    }
}
