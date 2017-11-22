using ProductSellingWorkflow.Common.Enums;
using System;

namespace ProductSellingWorkflow.DataModel
{
	public class ProductLog
	{
		public int Id { get; set; }

		public Guid OperationId { get; set; }
		public int ProductId { get; set; }
		public Product Product { get; set; }

		public ProductLogType Type { get; set; }
		public LogOperation Operation { get; set; }
		public string Property { get; set; }
		public string Value { get; set; }
		public DateTimeOffset CreatedAt { get; set; }

		public int CreatedById { get; set; }
		public User UserCreator { get; set; }
	}
}
