using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.DataModel;
using System;

namespace ProductSellingWorkflow.Service.Events
{
	public class ProductNameChange : ProductPropertyChange<string>
	{
		public ProductNameChange(string value, ProductLogType type, Guid operationId) : base(value, type, operationId) { }

		internal override EventResult Apply(Product product, bool createLog = true)
		{
			var result = base.Apply(product, createLog);
			if (result.Success)
			{
				product.Name = Value;

				if (createLog) CreateLog(product, "Name", Value);
			}
			return result;
		}
	}
}
