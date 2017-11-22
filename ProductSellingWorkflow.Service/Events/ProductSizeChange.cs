namespace ProductSellingWorkflow.Service.Events
{
	public class ProductSizeChange : PropertyChangeEvent<string>
	{
		public ProductSizeChange(string value) : base(value)
		{
		}
	}
}