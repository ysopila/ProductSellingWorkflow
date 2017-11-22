namespace ProductSellingWorkflow.Service.Events
{
	public class ProductColorChange : PropertyChangeEvent<string>
	{
		public ProductColorChange(string value) : base(value)
		{
		}
	}
}
