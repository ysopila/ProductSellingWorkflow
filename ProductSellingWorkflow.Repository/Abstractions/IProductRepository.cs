using ProductSellingWorkflow.DataModel;
using ProductSellingWorkflow.Data.Views;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Repository.Abstractions
{
	public interface IProductRepository : IRepository<Product>
	{
		ProductView GetOne(int id);
		IEnumerable<ProductView> GetAll();
	}
}
