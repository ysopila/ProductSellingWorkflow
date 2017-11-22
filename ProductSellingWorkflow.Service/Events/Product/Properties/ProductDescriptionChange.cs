namespace ProductSellingWorkflow.Service.Events.Product.Properties
{
	public class ProductDescriptionChange : PropertyChangeEvent<string>
	{
		public ProductDescriptionChange(string value) : base(value)
		{
		}
	}
}
