using ProductSellingWorkflow.Common.Enums;

namespace ProductSellingWorkflow.Service.Events.Product.Properties
{
	public class ProductStateChange : PropertyChangeEvent<ProductState>
	{
		public ProductStateChange(ProductState value) : base(value)
		{
		}
	}
}
