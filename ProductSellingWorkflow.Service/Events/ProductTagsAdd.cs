using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.DataModel;
using System;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Service.Events
{
	public class ProductTagsAdd : ProductPropertyChange<IEnumerable<string>>
	{
		public ProductTagsAdd(IEnumerable<string> value, ProductLogType type, Guid operationId) : base(value, type, operationId) { }

		protected override ProductLogOperation Operation => ProductLogOperation.Add;

		internal override void Apply(Product product, bool createLog = true)
		{
			foreach (var item in Value)
				product.ProductTags.Add(new ProductTag { Tag = new Tag { Name = item } });

			if (createLog) product.ProductLogs.Add(CreateLog("Tags", string.Join(", ", Value)));
		}
	}
}
