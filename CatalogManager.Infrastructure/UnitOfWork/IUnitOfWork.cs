using CatalogManager.Domain.Entities;
using CatalogManager.Infrastructure.Repositories;
using System;

namespace CatalogManager.Infrastructure.UnitOfWork
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        IRepository<Category> Categories { get; }
        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        IRepository<Product> Products { get; }
        /// <summary>
        /// Saves this instance.
        /// </summary>
        void Save();
    }
}
