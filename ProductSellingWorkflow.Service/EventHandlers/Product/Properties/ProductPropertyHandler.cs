using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.DataModel;
using ProductSellingWorkflow.Service.Events;
using System;

namespace ProductSellingWorkflow.Service.EventHandlers.Product.Properties
{
	public class ProductPropertyHandler<T> : PropertyEventHandler<DataModel.Product, T> where T : PropertyChangeEvent
	{
		public ProductPropertyHandler(DataModel.Product entity, OperationContext context) : base(entity, context)
		{
		}

		protected virtual ProductLogType Type => ProductLogType.Set;

		protected virtual ProductLog CreateLog(string property, string value)
		{
			var log = new ProductLog
			{
				CreatedAt = DateTimeOffset.UtcNow,
				Operation = Context.Operation,
				OperationId = Context.OperationId,
				Property = property,
				Type = Type,
				Value = value,
				ProductId = Entity.Id,
				CreatedById = Context.CurrentUser.Id
			};
			Entity.ProductLogs.Add(log);
			return log;
		}
	}
}