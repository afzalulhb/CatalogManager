using CatalogManager.Infrastructure.CrossCutting.Dependencies;
using CatalogManager.Infrastructure.UnitOfWork;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CatalogManager.Infrastructure.CrossCutting
{
    internal class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            var assemblies = CatalogManagerConfigurationHelper.GetAssembleyNames();

            var assembliesToRegister = GetReferencedAssemblies()
                                .Where(x => assemblies.Contains(x.GetName().Name, StringComparer.OrdinalIgnoreCase)).ToArray();



            container.RegisterTypes(
                AllClasses.FromAssemblies(assembliesToRegister),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.PerResolve,
                overwriteExistingMappings: true);

            // Register your types here
            container.RegisterType<IDependencyResolver, DependencyResolver>(new InjectionConstructor(container));
            container.RegisterType<IUnitOfWork, UnitOfWork.UnitOfWork>(new HierarchicalLifetimeManager());
         
        }

        public static void RegisterFromConfig(IUnityContainer container, UnityConfigurationSection config)
        {
            container.LoadConfiguration(config);
            container.RegisterType<IDependencyResolver, DependencyResolver>(new InjectionConstructor(container));
        }

        private static IEnumerable<Assembly> GetReferencedAssemblies()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string privateBinPath = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath;

            var assembliesFromBase =
            from file in Directory.GetFiles(baseDirectory)
            where Path.GetExtension(file) == ".dll"
            select Assembly.LoadFrom(file);

            if (Directory.Exists(privateBinPath))
            {
                var assembliesFromBin = from file in Directory.GetFiles(privateBinPath)
                                        where Path.GetExtension(file) == ".dll"
                                        select Assembly.LoadFrom(file);
                return assembliesFromBase.Concat(assembliesFromBin);
            }

            return assembliesFromBase;
        }
    }
}
