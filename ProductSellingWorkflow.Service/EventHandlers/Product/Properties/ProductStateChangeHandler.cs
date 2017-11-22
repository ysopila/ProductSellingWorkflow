using ProductSellingWorkflow.Service.Events;
using ProductSellingWorkflow.Service.Events.Product.Properties;

namespace ProductSellingWorkflow.Service.EventHandlers.Product.Properties
{
	class ProductStateChangeHandler : ProductPropertyHandler<ProductStateChange>
	{
		public ProductStateChangeHandler(DataModel.Product entity, OperationContext context) : base(entity, context)
		{
		}

		public override EventResult Apply(ProductStateChange e, IEventOptions options)
		{
			var result = base.Apply(e, options);
			if (Entity.State != e.Value)
			{
				Entity.State = e.Value;
				if (options.Store)
					CreateLog("State", e.Value.ToString());
			}
			return result;
		}
	}
}