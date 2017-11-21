using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.DataModel;
using System;

namespace ProductSellingWorkflow.Service.Events
{
	public abstract class ProductPropertyChange<T> : ProductEvent
	{
		public ProductPropertyChange(T value, ProductLogType type, Guid operationId)
		{
			Value = value;
			Type = type;
			OperationId = operationId;
		}

		protected virtual ProductLog CreateLog(Product product, string property, string value)
		{
			var log = new ProductLog
			{
				CreatedAt = DateTimeOffset.UtcNow,
				Operation = Operation,
				OperationId = OperationId,
				Property = property,
				Type = Type,
				Value = value,
				ProductId = product.Id
			};
			product.ProductLogs.Add(log);
			return log;
		}

		protected virtual ProductLogOperation Operation => ProductLogOperation.Set;

		internal Guid OperationId { get; }
		internal T Value { get; }
		internal ProductLogType Type { get; }
	}
}
