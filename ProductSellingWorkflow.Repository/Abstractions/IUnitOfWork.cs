using ProductSellingWorkflow.DataModel;
using System;

namespace ProductSellingWorkflow.Repository.Abstractions
{
	public interface IUnitOfWork : IDisposable
	{
		void Save();
		void Rollback();
		void Recycle();

		IProductRepository ProductRepository { get; }
		IRepository<WatchList> WatchListRepository { get; }
		IRepository<ProductLog> ProductLogRepository { get; }
		IRepository<ProductTag> ProductTagRepository { get; }
		IRepository<Tag> TagRepository { get; }
		IUserRepository UserRepository { get; }
		IRepository<Role> RoleRepository { get; }
	}
}
