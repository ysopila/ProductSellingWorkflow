using ProductSellingWorkflow.Data.Views;
using ProductSellingWorkflow.Service.Events;
using ProductSellingWorkflow.Service.Events.Product;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Service.Abstractions
{
	public interface IProductService
	{
		ProductView Get(int id);
		IEnumerable<ProductBaseView> GetAll();
		IEnumerable<ProductView> GetAllForAdmin();
		IEnumerable<ProductView> GetAllForOwner(int ownerId);

		EventResult Create(CreateProductEvent @event);
		EventResult Update(UpdateProductEvent @event);
		EventResult Update(MoveInCatalogEvent @event);

		void AddToWatchList(int id, int userId);
		void RemoveFromWatchList(int id, int userId);
	}
}
