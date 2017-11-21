using ProductSellingWorkflow.Service.Events;
using ProductSellingWorkflow.Service.Models;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Service.Abstractions
{
	public interface IProductService
	{
		ProductDTO Get(int id);
		IEnumerable<ProductDTO> GetAll();
		EventResult Create(CreateProductEvent @event);
		EventResult Update(UpdateProductEvent @event);
	}
}
