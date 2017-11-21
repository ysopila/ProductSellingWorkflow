using ProductSellingWorkflow.Common.Enums;
using System;

namespace ProductSellingWorkflow.Service.Events
{
	public abstract class ComplexProductEvent : ProductEvent
	{
		protected Guid OperationId { get; } = Guid.NewGuid();
		protected abstract ProductLogType Type { get; }

	}
}
