using ProductSellingWorkflow.Service.Events;
using ProductSellingWorkflow.Service.Models;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Service.Abstractions
{
	public interface IProductService
	{
		ProductDTO Get(int id);
		IEnumerable<ProductDTO> GetAll();
		void Create(CreateProductEvent @event);
		void Update(UpdateProductEvent @event);
	}
}
