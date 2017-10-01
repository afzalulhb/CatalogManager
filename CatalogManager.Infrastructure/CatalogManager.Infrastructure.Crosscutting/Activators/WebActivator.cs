using CatalogManager.Infrastructure.CrossCutting.Dependencies;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CatalogManager.Infrastructure.CrossCutting
{

    public static class WebActivator
    {
        private static readonly Lazy<IUnityContainer> LazyContainer = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            UnityConfig.RegisterTypes(container);
            return container;
        });

        private static IUnityContainer Container
        {
            get { return LazyContainer.Value; }
        }

        //public static void Start()
        //{
        //    DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        //}

        public static void Register(HttpConfiguration config)
        {
            // custom registrtatons for webactivator
            //Container.RegisterType<ConfigurationContext>(new ContainerControlledLifetimeManager());

            //Container.RegisterType<IProductRepository, ProductRepository>(new HierarchicalLifetimeManager());
            var container = new UnityContainer();
            UnityConfig.RegisterTypes(container);

            //web api resolver
            config.DependencyResolver = new UnityResolver(container);
            //static resolver
            Dependencies.DependencyResolver.SetResolver(container.Resolve<IDependencyResolver>());
        }

    }
}
