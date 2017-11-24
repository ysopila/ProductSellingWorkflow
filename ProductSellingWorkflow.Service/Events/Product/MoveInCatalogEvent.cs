using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.Service.Events.Product.Properties;

namespace ProductSellingWorkflow.Service.Events.Product
{
	public class MoveInCatalogEvent : ComplexEvent
	{
		public int Id { get; }

		public MoveInCatalogEvent(int id)
		{
			Id = id;

			Events.Add("State", new ProductStateChange(ProductState.InCatalog));
		}
	}
}