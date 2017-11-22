using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.Service.Events.Product.Properties;

namespace ProductSellingWorkflow.Service.Events.Product
{
	public class BuyEvent : ComplexEvent
	{
		public BuyEvent()
		{
			Events.Add("State", new ProductStateChange(ProductState.Sold));
		}
	}
}