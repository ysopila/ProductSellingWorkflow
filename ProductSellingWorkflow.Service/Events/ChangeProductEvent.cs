using ProductSellingWorkflow.Common.Enums;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Service.Events
{
	public class ChangeProductEvent
	{
		public ChangeProductEvent(ProductLogType type)
		{
			Type = type;
		}

		public ProductLogType Type { get; set; }
		public Dictionary<string, object> Values { get; set; }
	}

	public class CreateProductEvent : ChangeProductEvent
	{
		public CreateProductEvent() : base(ProductLogType.Create)
		{
		}
	}
	public class UpdateProductEvent : ChangeProductEvent
	{
		public UpdateProductEvent() : base(ProductLogType.Modify)
		{
		}

		public int Id { get; set; }
	}
	public class ChangeStateProductEvent : ChangeProductEvent
	{
		public ChangeStateProductEvent() : base(ProductLogType.StateChange)
		{
		}
	}
}
