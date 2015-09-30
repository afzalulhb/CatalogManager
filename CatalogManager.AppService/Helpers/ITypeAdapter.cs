using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogManager.AppService.Helpers
{
    public interface ITypeAdapter
    {
        //TTarget Adapt<TSource, TTarget>(TSource source, TTarget target)
        //    where TTarget : class,new()
        //    where TSource : class;


        TTarget Adapt<TTarget>(object source) where TTarget : class,new();
    }
}
