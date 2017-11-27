using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductSellingWorkflow.Data.Views
{
	public class ProductBaseView
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Size { get; set; }
		public string Color { get; set; }
		public decimal Price { get; set; }
		public IEnumerable<string> Tags { get; set; }
		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset ModifiedAt { get; set; }
		public bool IsInWatchlist { get; set; }

		public string[] TagsArray
		{
			get { return Tags?.ToArray(); }
			set { Tags = value; }
		}
	}
}