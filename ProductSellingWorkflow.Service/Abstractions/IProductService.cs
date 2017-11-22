using ProductSellingWorkflow.Data.Views;
using ProductSellingWorkflow.Service.Events;
using ProductSellingWorkflow.Service.Events.Product;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Service.Abstractions
{
	public interface IProductService
	{
		ProductView Get(int id);
		IEnumerable<ProductView> GetAll();
		EventResult Create(CreateProductEvent @event);
		EventResult Update(UpdateProductEvent @event);
	}
}
