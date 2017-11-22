using ProductSellingWorkflow.Common.Enums;
using System.Collections.Generic;

namespace ProductSellingWorkflow.DataModel
{
	public class Product
	{
		public Product ()
		{
			ProductLogs = new List<ProductLog>();
			ProductTags = new List<ProductTag>();
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Size { get; set; }
		public string Color { get; set; }
		public decimal Price { get; set; }
		public ProductState State { get; set; }

		public ICollection<ProductLog> ProductLogs { get; set; }
		public ICollection<ProductTag> ProductTags { get; set; }
	}
}
