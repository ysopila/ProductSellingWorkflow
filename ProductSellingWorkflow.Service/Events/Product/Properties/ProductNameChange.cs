namespace ProductSellingWorkflow.Service.Events.Product.Properties
{
	public class ProductNameChange : PropertyChangeEvent<string>
	{
		public ProductNameChange(string value) : base(value)
		{
		}
	}
}