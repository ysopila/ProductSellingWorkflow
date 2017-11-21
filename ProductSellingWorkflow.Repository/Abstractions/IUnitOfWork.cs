using ProductSellingWorkflow.DataModel;
using System;

namespace ProductSellingWorkflow.Repository.Abstractions
{
	public interface IUnitOfWork : IDisposable
	{
		void Save();
		void Recycle();

		IProductRepository ProductRepository { get; }
		IRepository<ProductLog> ProductLogRepository { get; }
		IRepository<ProductTag> ProductTagRepository { get; }
		IRepository<Tag> TagRepository { get; }
	}
}
