using CatalogManager.Domain.Entities;
using CatalogManager.Infrastructure.Repositories;
using System;

namespace CatalogManager.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Category> Categories { get; }
        IRepository<Product> Products { get; }
        void Save();
    }
}
