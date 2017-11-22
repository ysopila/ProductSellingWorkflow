using ProductSellingWorkflow.Service.Events;
using ProductSellingWorkflow.Service.Events.Product.Properties;

namespace ProductSellingWorkflow.Service.EventHandlers.Product.Properties
{
	class ProductNameChangeHandler : ProductPropertyHandler<ProductNameChange>
	{
		public ProductNameChangeHandler(DataModel.Product entity, OperationContext context) : base(entity, context)
		{
		}

		public override EventResult Apply(ProductNameChange e, IEventOptions options)
		{
			var result = base.Apply(e, options);
			if (!string.Equals(Entity.Name, e.Value))
			{
				Entity.Name = e.Value;
				if (options.Store)
					CreateLog("Name", e.Value);
			}
			return result;
		}
	}
}