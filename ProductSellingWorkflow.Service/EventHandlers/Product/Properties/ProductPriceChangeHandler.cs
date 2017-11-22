using ProductSellingWorkflow.Service.Events;
using ProductSellingWorkflow.Service.Events.Product.Properties;

namespace ProductSellingWorkflow.Service.EventHandlers.Product.Properties
{
	class ProductPriceChangeHandler : ProductPropertyHandler<ProductPriceChange>
	{
		public ProductPriceChangeHandler(DataModel.Product entity, OperationContext context) : base(entity, context)
		{
		}

		public override EventResult Apply(ProductPriceChange e, IEventOptions options)
		{
			var result = base.Apply(e, options);

			if (!decimal.Equals(Entity.Price, e.Value))
			{
				Entity.Price = e.Value;
				if (options.Store)
					CreateLog("Price", e.Value.ToString());
			}

			return result;
		}
	}
}