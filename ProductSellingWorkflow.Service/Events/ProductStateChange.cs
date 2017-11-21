using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.DataModel;
using System;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Service.Events
{
	public class ProductStateChange : ProductPropertyChange<ProductState>
	{
		public ProductStateChange(ProductState value, ProductLogType type, Guid operationId) : base(value, type, operationId) { }

		internal override IList<EventValidationError> Validate(Product product)
		{
			var result = base.Validate(product);

			if (product.State != ProductState.InCatalog
				&& Value == ProductState.Sold)
				result.Add(new EventValidationError { Property = "State", Message = "Cannot sell product that is not in catalog" });

			if (product.State == ProductState.Sold
				&& Value == ProductState.InCatalog)
				result.Add(new EventValidationError { Property = "State", Message = "Cannot move sold product in catalog" });

			return result;
		}

		internal override EventResult Apply(Product product, bool createLog = true)
		{
			var result = base.Apply(product, createLog);
			if (result.Success)
			{
				product.State = Value;

				if (createLog) CreateLog(product, "State", Value.ToString());
			}
			return result;
		}
	}
}
