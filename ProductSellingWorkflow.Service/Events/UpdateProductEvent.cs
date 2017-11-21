using ProductSellingWorkflow.Common.Enums;

namespace ProductSellingWorkflow.Service.Events
{
	public class UpdateProductEvent : CreateProductEvent
	{
		protected override ProductLogType Type => ProductLogType.Modify;

		public UpdateProductEvent(int id)
		{
			Id = id;
		}

		public int Id { get; }
	}
}
