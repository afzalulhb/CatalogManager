﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogManager.AppService.Helpers
{
    public static class ProjectionExtensionMethods
    {
        public static TProjection ProjectedAs<TProjection>(this object item) where TProjection : class,new()
        {
            ITypeAdapter adapter = new CategoryProductAdapter();
            return adapter.Adapt<TProjection>(item);
        }

        /// <summary>
        /// projected a enumerable collection of items
        /// </summary>
        /// <typeparam name="TProjection">The dtop projection type</typeparam>
        /// <param name="items">the collection of entity items</param>
        /// <returns>Projected collection</returns>
        public static List<TProjection> ProjectedAsCollection<TProjection>(this IEnumerable<object> items)
            where TProjection : class,new()
        {
            ITypeAdapter adapter = new CategoryProductAdapter();
            return adapter.Adapt<List<TProjection>>(items);
        }
    }
}