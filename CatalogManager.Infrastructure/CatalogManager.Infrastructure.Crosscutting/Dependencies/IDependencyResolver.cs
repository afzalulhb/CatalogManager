using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogManager.Infrastructure.CrossCutting.Dependencies
{
    public interface IDependencyResolver
    {
        T Resolve<T>();
        T Resolve<T>(params KeyValuePair<string, object>[] constructorParameters);
        T Resolve<T>(params KeyValuePair<Type, object>[] dependencyOverrides);
        object Resolve(Type t);
        T Resolve<T>(string namedInstance);
        bool Contains(Type t);
    }
}
