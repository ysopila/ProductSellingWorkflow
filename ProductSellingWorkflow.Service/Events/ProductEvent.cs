using ProductSellingWorkflow.DataModel;

namespace ProductSellingWorkflow.Service.Events
{
	public abstract class ProductEvent
	{
		internal abstract void Apply(Product product, bool createLog = true);
	}
}
