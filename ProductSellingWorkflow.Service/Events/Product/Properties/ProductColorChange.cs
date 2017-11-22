namespace ProductSellingWorkflow.Service.Events.Product.Properties
{
	public class ProductColorChange : PropertyChangeEvent<string>
	{
		public ProductColorChange(string value) : base(value)
		{
		}
	}
}
