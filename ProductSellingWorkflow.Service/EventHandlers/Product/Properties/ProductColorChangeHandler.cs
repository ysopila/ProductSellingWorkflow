using ProductSellingWorkflow.Service.Events;
using ProductSellingWorkflow.Service.Events.Product.Properties;

namespace ProductSellingWorkflow.Service.EventHandlers.Product.Properties
{
	class ProductColorChangeHandler : ProductPropertyHandler<ProductColorChange>
	{
		public ProductColorChangeHandler(DataModel.Product entity, OperationContext context) : base(entity, context)
		{
		}

		public override EventResult Apply(ProductColorChange e, IEventOptions options)
		{
			var result = base.Apply(e, options);
			if (!string.Equals(Entity.Color, e.Value))
			{
				Entity.Color = e.Value;
				if (options.Store)
					CreateLog("Color", e.Value);
			}
			return result;
		}
	}
}