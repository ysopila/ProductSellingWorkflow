namespace ProductSellingWorkflow.Service.Events
{
	public class ProductDescriptionChange : PropertyChangeEvent<string>
	{
		public ProductDescriptionChange(string value) : base(value)
		{
		}
	}
}
