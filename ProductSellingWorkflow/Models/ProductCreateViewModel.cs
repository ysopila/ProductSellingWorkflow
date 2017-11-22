using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ProductSellingWorkflow.Models
{
	public class ProductCreateViewModel
	{
		[Required]
		public string Name { get; set; }
		public string Description { get; set; }
		public string Size { get; set; }
		public string Color { get; set; }
		public decimal Price { get; set; }
		public string Tags { get; set; }

		public IEnumerable<string> TagsList => Tags?.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
	}
}