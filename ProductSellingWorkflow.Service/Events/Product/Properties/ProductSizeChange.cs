namespace ProductSellingWorkflow.Service.Events.Product.Properties
{
	public class ProductSizeChange : PropertyChangeEvent<string>
	{
		public ProductSizeChange(string value) : base(value)
		{
		}
	}
}