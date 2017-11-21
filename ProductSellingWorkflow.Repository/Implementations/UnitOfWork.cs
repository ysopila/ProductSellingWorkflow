using ProductSellingWorkflow.Data;
using ProductSellingWorkflow.DataModel;
using ProductSellingWorkflow.Repository.Abstractions;
using System;

namespace ProductSellingWorkflow.Repository.Implementations
{
	public class UnitOfWork : IUnitOfWork
	{
		private IDbContext _context;
		private IProductRepository _productRepository;
		private IRepository<ProductLog> _productLogRepository;
		private IRepository<ProductTag> _productTagRepository;
		private IRepository<Tag> _tagRepository;

		private IDbContext Context
		{
			get { return _context ?? (_context = new DataContext()); }
		}

		public IProductRepository ProductRepository
		{
			get { return _productRepository ?? (_productRepository = new ProductRepository(Context)); }
		}

		public IRepository<ProductLog> ProductLogRepository
		{
			get { return _productLogRepository ?? (_productLogRepository = new Repository<ProductLog>(Context)); }
		}

		public IRepository<ProductTag> ProductTagRepository
		{
			get { return _productTagRepository ?? (_productTagRepository = new Repository<ProductTag>(Context)); }
		}

		public IRepository<Tag> TagRepository
		{
			get { return _tagRepository ?? (_tagRepository = new Repository<Tag>(Context)); }
		}

		public void Save()
		{
			_context.SaveChanges();
		}
		public void Recycle()
		{
			if (_context != null)
			{
				_context.Dispose();
				_context = null;
			}
		}

		#region IDisposable

		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing && _context != null)
				{
					_context.Dispose();
				}
			}
			this.disposed = true;
		}

		void IDisposable.Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion
	}
}