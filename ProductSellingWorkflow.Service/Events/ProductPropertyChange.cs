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

		protected virtual ProductLog CreateLog(string property, string value)
		{
			return new ProductLog
			{
				CreatedAt = DateTimeOffset.UtcNow,
				Operation = Operation,
				OperationId = OperationId,
				Property = property,
				Type = Type,
				Value = value
			};
		}

		protected virtual ProductLogOperation Operation => ProductLogOperation.Set;

		internal Guid OperationId { get; }
		internal T Value { get; }
		internal ProductLogType Type { get; }
	}
}
