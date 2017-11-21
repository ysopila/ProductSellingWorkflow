using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.DataModel;
using System;

namespace ProductSellingWorkflow.Service.Events
{
	public class ProductColorChange : ProductPropertyChange<string>
	{
		public ProductColorChange(string value, ProductLogType type, Guid operationId) : base(value, type, operationId) { }

		internal override void Apply(Product product, bool createLog = true)
		{
			product.Color = Value;

			if (createLog) product.ProductLogs.Add(CreateLog("Color", Value));
		}
	}
}
