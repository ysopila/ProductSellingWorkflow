using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.DataModel;
using System;

namespace ProductSellingWorkflow.Service.Events
{
	public class ProductDescriptionChange : ProductPropertyChange<string>
	{
		public ProductDescriptionChange(string value, ProductLogType type, Guid operationId) : base(value, type, operationId) { }

		internal override void Apply(Product product, bool createLog = true)
		{
			product.Description = Value;

			if (createLog) product.ProductLogs.Add(CreateLog("Description", Value));
		}
	}
}
