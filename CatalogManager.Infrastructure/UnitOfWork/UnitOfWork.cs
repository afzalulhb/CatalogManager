using CatalogManager.Domain.Entities;
using CatalogManager.Infrastructure.Repositories;
using System;

namespace CatalogManager.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private CatalogManagerContext context;
        private IRepository<Category> categoryRepository;
        private IRepository<Product> productRepository;

        public UnitOfWork(CatalogManagerContext context)
        {
            this.context = context;
        }

        public IRepository<Category> Categories
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new GenericRepository<Category>(context);
                }

                return this.categoryRepository;
            }
        }
        public IRepository<Product> Products
        {
            get
            {
                if (this.productRepository == null)
                {
                    this.productRepository = new GenericRepository<Product>(context);
                }

                return this.productRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
