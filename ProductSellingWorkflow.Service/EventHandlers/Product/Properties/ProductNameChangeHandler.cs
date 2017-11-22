using ProductSellingWorkflow.Service.Events;

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
			Entity.Name = e.Value;
			if (options.Store)
				CreateLog("Name", e.Value);
			return result;
		}
	}
}