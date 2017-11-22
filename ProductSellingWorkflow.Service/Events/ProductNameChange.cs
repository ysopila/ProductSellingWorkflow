namespace ProductSellingWorkflow.Service.Events
{
	public class ProductNameChange : PropertyChangeEvent<string>
	{
		public ProductNameChange(string value) : base(value)
		{
		}
	}
}