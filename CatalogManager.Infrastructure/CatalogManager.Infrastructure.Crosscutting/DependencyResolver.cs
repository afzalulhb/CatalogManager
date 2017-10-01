using CatalogManager.Infrastructure.CrossCutting.Dependencies;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogManager.Infrastructure.CrossCutting
{
    public class DependencyResolver : IDependencyResolver
    {
        private readonly IUnityContainer _container;

        public DependencyResolver(IUnityContainer container)
        {
            _container = container;
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public T Resolve<T>(params KeyValuePair<string, object>[] constructorParameters)
        {
            var parameters = new ParameterOverrides();
            foreach (var param in constructorParameters)
            {
                parameters.Add(param.Key, param.Value);
            }
            return _container.Resolve<T>(parameters);
        }


        public T Resolve<T>(params KeyValuePair<Type, object>[] dependencyOverrides)
        {
            var overrides = new DependencyOverrides();
            foreach (var pair in dependencyOverrides)
            {
                overrides.Add(pair.Key, pair.Value);
            }
            return _container.Resolve<T>(overrides);
        }

        public object Resolve(Type t)
        {
            return _container.Resolve(t);
        }

        public T Resolve<T>(string namedInstance)
        {
            return _container.Resolve<T>(namedInstance);
        }

        public bool Contains(Type t)
        {
            return _container.Registrations.Any(r => r.RegisteredType == t);
        }
    }
}
