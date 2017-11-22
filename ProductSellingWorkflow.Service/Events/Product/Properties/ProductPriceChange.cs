using ProductSellingWorkflow.Common.Enums;

namespace ProductSellingWorkflow.Service.Events.Product.Properties
{
	public class ProductPriceChange : PropertyChangeEvent<decimal>
	{
		public ProductPriceChange(decimal value) : base(value)
		{
		}
	}
}
