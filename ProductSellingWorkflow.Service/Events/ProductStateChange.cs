using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.DataModel;
using System;

namespace ProductSellingWorkflow.Service.Events
{
	public class ProductStateChange : ProductPropertyChange<ProductState>
	{
		public ProductStateChange(ProductState value, ProductLogType type, Guid operationId) : base(value, type, operationId) { }

		internal override void Apply(Product product, bool createLog = true)
		{
			product.State = Value;

			if (createLog) product.ProductLogs.Add(CreateLog("State", Value.ToString()));
		}
	}

}
