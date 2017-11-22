namespace ProductSellingWorkflow.Service.Events.Product.Properties
{
	public class ProductTagAdd : PropertyChangeEvent<string>
	{
		public ProductTagAdd(string value) : base(value)
		{
		}
	}
}
