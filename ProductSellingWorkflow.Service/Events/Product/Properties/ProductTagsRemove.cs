namespace ProductSellingWorkflow.Service.Events.Product.Properties
{
	public class ProductTagRemove : PropertyChangeEvent<string>
	{
		public ProductTagRemove(string value) : base(value)
		{
		}
	}
}