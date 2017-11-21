using System.Collections.Generic;

namespace ProductSellingWorkflow.DataModel
{
	public class Tag
	{
		public Tag()
		{
			ProductTags = new List<ProductTag>();
		}

		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<ProductTag> ProductTags { get; set; }
	}
}
