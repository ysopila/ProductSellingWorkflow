using ProductSellingWorkflow.Service.Events;

namespace ProductSellingWorkflow.Service.EventHandlers.Product.Properties
{
	class ProductSizeChangeHandler : ProductPropertyHandler<ProductSizeChange>
	{
		public ProductSizeChangeHandler(DataModel.Product entity, OperationContext context) : base(entity, context)
		{
		}

		public override EventResult Apply(ProductSizeChange e, IEventOptions options)
		{
			var result = base.Apply(e, options);
			Entity.Size = e.Value;
			if (options.Store)
				CreateLog("Size", e.Value);
			return result;
		}
	}
}