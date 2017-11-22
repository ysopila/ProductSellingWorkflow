using ProductSellingWorkflow.Common.Enums;

namespace ProductSellingWorkflow.Service.Events
{
	public class ProductStateChange : PropertyChangeEvent<ProductState>
	{
		public ProductStateChange(ProductState value) : base(value)
		{
		}
	}
}
