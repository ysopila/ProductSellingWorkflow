using ProductSellingWorkflow.Service.Events;
using System.ComponentModel.DataAnnotations;

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

		public CreateProductEvent ToCreateProductEvent()
		{
			var @event = new CreateProductEvent
			{
				Name = Name,
				Description = Description,
				Size = Size,
				Color = Color
			};

			//var added = Tags.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());

			//if (added.Any()) @event.AddedTags = added;

			return @event;
		}
	}
}