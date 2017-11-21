using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.DataModel;
using System;

namespace ProductSellingWorkflow.Service.Events
{
	public class ProductSizeChange : ProductPropertyChange<string>
	{
		public ProductSizeChange(string value, ProductLogType type, Guid operationId) : base(value, type, operationId) { }

		internal override void Apply(Product product, bool createLog = true)
		{
			product.Size = Value;

			if (createLog) product.ProductLogs.Add(CreateLog("Size", Value));
		}
	}
}
