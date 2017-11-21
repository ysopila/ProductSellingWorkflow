using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.DataModel;

namespace ProductSellingWorkflow.Service.Events
{
	public class BuyEvent : ComplexProductEvent
	{
		private ProductStateChange _state;
		protected override ProductLogType Type => ProductLogType.StateChange;

		public BuyEvent()
		{
			_state = new ProductStateChange(ProductState.Sold, Type, OperationId);
		}

		internal override void Apply(Product product, bool createLog = true)
		{
			_state?.Apply(product, createLog);
		}
	}
}
