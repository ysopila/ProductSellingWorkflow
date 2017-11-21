using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.DataModel;
using System;
using System.Linq;

namespace ProductSellingWorkflow.Service.Events
{
	public class ProductTagRemove : ProductPropertyChange<ProductTag>
	{
		public ProductTagRemove(ProductTag value, ProductLogType type, Guid operationId) : base(value, type, operationId) { }

		protected override ProductLogOperation Operation => ProductLogOperation.Remove;

		internal override EventResult Apply(Product product, bool createLog = true)
		{
			var result = base.Apply(product, createLog);
			if (result.Success)
			{
				product.ProductTags.Remove(product.ProductTags.FirstOrDefault(x => x.TagId == Value.Id));

				if (createLog) CreateLog(product, "Tags", Value.Value);
			}
			return result;
		}
	}
}