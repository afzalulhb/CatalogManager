using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogManager.Infrastructure.CrossCutting.Dependencies
{
    public static class DependencyResolver
    {
        private static readonly object Lock = new object();
        private static IDependencyResolver _resolver;

        public static void SetResolver(IDependencyResolver resolver)
        {
            lock (Lock)
            {
                _resolver = resolver;
            }
        }

        public static T Resolve<T>()
        {
            if (_resolver == null) throw new InvalidOperationException("resolver is not initialized");
            return _resolver.Resolve<T>();
        }
        public static T Resolve<T>(string instanceName)
        {
            if (_resolver == null) throw new InvalidOperationException("resolver is not initialized");
            return _resolver.Resolve<T>(instanceName);
        }

        public static T Resolve<T>(params KeyValuePair<string, object>[] constructorParameters)
        {
            if (_resolver == null) throw new InvalidOperationException("resolver is not initialized");
            return _resolver.Resolve<T>(constructorParameters);
        }

        public static T Resolve<T>(params KeyValuePair<Type, object>[] dependencyOverrides)
        {
            if (_resolver == null) throw new InvalidOperationException("resolver is not initialized");
            return _resolver.Resolve<T>(dependencyOverrides);
        }

        /// <summary>
        /// Resolve the provided type. Use ONLY when the type is unknown during compile-time. In all other cases, the type-safe generic methods should be used instead.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object Resolve(Type t)
        {
            if (_resolver == null)
            {
                throw new InvalidOperationException("Resolver is not initialized.");
            }
            return _resolver.Resolve(t);
        }

        public static bool Contains(Type t)
        {
            return _resolver.Contains(t);
        }
    }
}
