using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogManager.AppService.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITypeAdapter
    {
        //TTarget Adapt<TSource, TTarget>(TSource source, TTarget target)
        //    where TTarget : class,new()
        //    where TSource : class;


        /// <summary>
        /// Adapts the specified source.
        /// </summary>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        TTarget Adapt<TTarget>(object source) where TTarget : class,new();
    }
}
