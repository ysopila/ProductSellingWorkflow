namespace ProductSellingWorkflow.Service.Events
{
	public class ProductTagRemove : PropertyChangeEvent<string>
	{
		public ProductTagRemove(string value) : base(value)
		{
		}
	}
}