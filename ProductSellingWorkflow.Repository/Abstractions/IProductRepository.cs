using ProductSellingWorkflow.DataModel;
using ProductSellingWorkflow.Repository.Models;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Repository.Abstractions
{
	public interface IProductRepository : IRepository<Product>
	{
		ProductModel GetOne(int id);
		IEnumerable<ProductModel> GetAll();
	}
}
