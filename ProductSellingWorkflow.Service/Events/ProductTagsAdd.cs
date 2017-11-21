using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.DataModel;
using System;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Service.Events
{
	public class ProductTagAdd : ProductPropertyChange<ProductTag>
	{
		public ProductTagAdd(ProductTag value, ProductLogType type, Guid operationId) : base(value, type, operationId) { }

		protected override ProductLogOperation Operation => ProductLogOperation.Add;

		internal override EventResult Apply(Product product, bool createLog = true)
		{
			var result = base.Apply(product, createLog);
			if (result.Success)
			{
				var productTag = new DataModel.ProductTag { ProductId = product.Id };
				if (Value.Id.HasValue)
					productTag.TagId = Value.Id.Value;
				else
					productTag.Tag = new Tag { Name = Value.Value };
				product.ProductTags.Add(productTag);

				if (createLog)
					CreateLog(product, "Tags", Value.Value);
			}
			return result;
		}
	}
}
