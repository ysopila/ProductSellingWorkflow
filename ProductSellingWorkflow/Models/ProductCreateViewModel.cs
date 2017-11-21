using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Models
{
	public class ProductCreateViewModel
	{
		[Required]
		public string Name { get; set; }
		public string Description { get; set; }
		public string Size { get; set; }
		public string Color { get; set; }
		public string Tags { get; set; }

		public IEnumerable<string> TagsList => Tags?.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());
	}
}