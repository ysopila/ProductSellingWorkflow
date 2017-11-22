using ProductSellingWorkflow.Common.Enums;

namespace ProductSellingWorkflow.Service.Events
{
	public class BuyEvent : ComplexEvent
	{
		public BuyEvent()
		{
			Events.Add("State", new ProductStateChange(ProductState.Sold));
		}
	}
}