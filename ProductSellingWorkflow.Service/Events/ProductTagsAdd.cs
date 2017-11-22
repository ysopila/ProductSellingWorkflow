namespace ProductSellingWorkflow.Service.Events
{
	public class ProductTagAdd : PropertyChangeEvent<string>
	{
		public ProductTagAdd(string value) : base(value)
		{
		}
	}
}
