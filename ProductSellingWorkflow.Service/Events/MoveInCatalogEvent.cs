using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.DataModel;

namespace ProductSellingWorkflow.Service.Events
{
	public class MoveInCatalogEvent : ComplexProductEvent
	{
		private ProductStateChange _state;
		protected override ProductLogType Type => ProductLogType.StateChange;

		public MoveInCatalogEvent()
		{
			_state = new ProductStateChange(ProductState.InCatalog, Type, OperationId);
		}

		internal override EventResult Apply(Product product, bool createLog = true)
		{
			var result = base.Apply(product, createLog);
			if (result.Success)
			{
				result += _state?.Apply(product, createLog);
			}
			return result;
		}
	}
}
