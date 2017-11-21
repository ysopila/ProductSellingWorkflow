﻿using ProductSellingWorkflow.Common.Enums;
using System;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Repository.Models
{
	public class ProductModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Size { get; set; }
		public string Color { get; set; }
		public IEnumerable<string> Tags { get; set; }
		public ProductState State { get; set; }
		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset ModifiedAt { get; set; }
	}
}
