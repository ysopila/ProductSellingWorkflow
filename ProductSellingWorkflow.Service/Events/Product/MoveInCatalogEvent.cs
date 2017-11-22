using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.Service.Events.Product.Properties;

namespace ProductSellingWorkflow.Service.Events.Product
{
	public class MoveInCatalogEvent : ComplexEvent
	{
		public MoveInCatalogEvent()
		{
			Events.Add("State", new ProductStateChange(ProductState.InCatalog));
		}
	}
}