﻿using CatalogManager.Infrastructure.CrossCutting;
using System.Web.Http;

namespace CatalogManager.DistributedService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            WebActivator.Register(config);
            //var container = new UnityContainer();
            //container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
            //container.RegisterType<ICategoryProductAppService, CategoryProductAppService>(new HierarchicalLifetimeManager());
            //config.DependencyResolver = new UnityResolver(container);
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.EnableCors();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
