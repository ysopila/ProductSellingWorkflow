﻿using ProductSellingWorkflow.Service.Events;
using ProductSellingWorkflow.Service.Events.Product.Properties;

namespace ProductSellingWorkflow.Service.EventHandlers.Product.Properties
{
	class ProductDescriptionChangeHandler : ProductPropertyHandler<ProductDescriptionChange>
	{
		public ProductDescriptionChangeHandler(DataModel.Product entity, OperationContext context) : base(entity, context)
		{
		}

		public override EventResult Apply(ProductDescriptionChange e, IEventOptions options)
		{
			var result = base.Apply(e, options);
			if (!string.Equals(Entity.Description, e.Value))
			{
				Entity.Description = e.Value;
				if (options.Store)
					CreateLog("Description", e.Value);
			}
			return result;
		}
	}
}