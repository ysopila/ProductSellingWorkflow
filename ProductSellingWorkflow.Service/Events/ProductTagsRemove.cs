using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.DataModel;
using System;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Service.Events
{
	public class ProductTagsRemove : ProductPropertyChange<IEnumerable<string>>
	{
		public ProductTagsRemove(IEnumerable<string> value, ProductLogType type, Guid operationId) : base(value, type, operationId) { }

		protected override ProductLogOperation Operation => ProductLogOperation.Remove;

		internal override void Apply(Product product, bool createLog = true)
		{
			if (createLog) product.ProductLogs.Add(CreateLog("Tags", string.Join(", ", Value)));
		}
	}
}
