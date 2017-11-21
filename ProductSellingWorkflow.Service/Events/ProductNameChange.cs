using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.DataModel;
using System;

namespace ProductSellingWorkflow.Service.Events
{
	public class ProductNameChange : ProductPropertyChange<string>
	{
		public ProductNameChange(string value, ProductLogType type, Guid operationId) : base(value, type, operationId) { }

		internal override void Apply(Product product, bool createLog = true)
		{
			product.Name = Value;

			if (createLog) product.ProductLogs.Add(CreateLog("Name", Value));
		}
	}
}
