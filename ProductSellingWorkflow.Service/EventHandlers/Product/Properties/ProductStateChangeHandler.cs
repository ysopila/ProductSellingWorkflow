using ProductSellingWorkflow.Service.Events;

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
			Entity.State = e.Value;
			if (options.Store)
				CreateLog("State", e.Value.ToString());
			return result;
		}
	}
}