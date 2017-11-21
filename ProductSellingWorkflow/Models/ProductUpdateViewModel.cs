using ProductSellingWorkflow.Service.Events;
using ProductSellingWorkflow.Service.Models;

namespace ProductSellingWorkflow.Models
{
	public class ProductUpdateViewModel : ProductCreateViewModel
	{
		public int Id { get; set; }

		public string HiddenName { get; set; }
		public string HiddenDescription { get; set; }
		public string HiddenSize { get; set; }
		public string HiddenColor { get; set; }
		public string HiddenTags { get; set; }

		public static ProductUpdateViewModel CreateModel(ProductDTO product)
		{
			return new ProductUpdateViewModel
			{
				Id = product.Id,

				Name = product.Name,
				Color = product.Color,
				Description = product.Description,
				Size = product.Size,
				Tags = string.Join(", ", product.Tags),

				HiddenName = product.Name,
				HiddenColor = product.Color,
				HiddenDescription = product.Description,
				HiddenSize = product.Size,
				HiddenTags = string.Join(", ", product.Tags)
			};
		}

		public UpdateProductEvent ToUpdateProductEvent()
		{
			var @event = new UpdateProductEvent(Id);

			if (!Name.Equals(HiddenName)) @event.Name = Name;

			if (!Description.Equals(HiddenDescription)) @event.Description = Description;

			if (!Color.Equals(HiddenColor)) @event.Color = Color;

			if (!Size.Equals(HiddenSize)) @event.Size = Size;

			//if (!Tags.Equals(HiddenTags))
			//{
			//	var first = HiddenTags.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());
			//	var second = Tags.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());

			//	var removed = first.Except(second);
			//	var added = second.Except(first);

			//	if (added.Any()) @event.AddedTags = added;
			//	if (removed.Any()) @event.RemovedTags = removed;
			//}

			return @event;
		}
	}
}