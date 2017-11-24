using ProductSellingWorkflow.Data.Views;
using ProductSellingWorkflow.DataModel;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Repository.Abstractions
{
	public interface IProductRepository : IRepository<Product>
	{
		ProductView GetOne(int id);
		IEnumerable<ProductView> GetAllForAdmin();
		IEnumerable<ProductView> GetAllForOwner(int ownerId);
		IEnumerable<ProductBaseView> GetCatalog();
	}
}
