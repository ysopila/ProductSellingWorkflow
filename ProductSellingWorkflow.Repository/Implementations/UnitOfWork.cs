using ProductSellingWorkflow.Data;
using ProductSellingWorkflow.DataModel;
using ProductSellingWorkflow.Repository.Abstractions;
using System;
using System.Data.Entity;
using System.Linq;

namespace ProductSellingWorkflow.Repository.Implementations
{
	public class UnitOfWork : IUnitOfWork
	{
		private IDbContext _context;
		private IProductRepository _productRepository;
		private IRepository<ProductLog> _productLogRepository;
		private IRepository<ProductTag> _productTagRepository;
		private IRepository<Tag> _tagRepository;
		private IUserRepository _userRepository;
		private IRepository<Role> _roleRepository;

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

		public IUserRepository UserRepository
		{
			get { return _userRepository ?? (_userRepository = new UserRepository(Context)); }
		}

		public IRepository<Role> RoleRepository
		{
			get { return _roleRepository ?? (_roleRepository = new Repository<Role>(Context)); }
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
		public void Rollback()
		{
			if (_context == null)
				return;
			var changedEntries = _context.ChangeTracker.Entries()
				.Where(x => x.State != EntityState.Unchanged).ToList();

			foreach (var entry in changedEntries)
			{
				switch (entry.State)
				{
					case EntityState.Modified:
						entry.CurrentValues.SetValues(entry.OriginalValues);
						entry.State = EntityState.Unchanged;
						break;
					case EntityState.Added:
						entry.State = EntityState.Detached;
						break;
					case EntityState.Deleted:
						entry.State = EntityState.Unchanged;
						break;
				}
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