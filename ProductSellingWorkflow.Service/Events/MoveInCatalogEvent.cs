using ProductSellingWorkflow.Common.Enums;

namespace ProductSellingWorkflow.Service.Events
{
	public class MoveInCatalogEvent : ComplexEvent
	{
		public MoveInCatalogEvent()
		{
			Events.Add("State", new ProductStateChange(ProductState.InCatalog));
		}
	}
}