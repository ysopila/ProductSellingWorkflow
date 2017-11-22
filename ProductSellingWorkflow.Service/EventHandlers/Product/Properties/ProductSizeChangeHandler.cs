using ProductSellingWorkflow.Service.Events;
using ProductSellingWorkflow.Service.Events.Product.Properties;

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
			if (!string.Equals(Entity.Size, e.Value))
			{
				Entity.Size = e.Value;
				if (options.Store)
					CreateLog("Size", e.Value);
			}
			return result;
		}
	}
}