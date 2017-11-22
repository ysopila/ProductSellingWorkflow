using ProductSellingWorkflow.Service.Events.Product.Properties;
using System.Collections.Generic;
using System.Linq;

namespace ProductSellingWorkflow.Service.Events.Product
{
	public class CreateProductEvent : ComplexEvent
	{
		public string Name
		{
			get => GetEvent<ProductNameChange>(nameof(Name)).Value;
			set => SetEvent(nameof(Name), new ProductNameChange(value));
		}

		public string Description
		{
			get => GetEvent<ProductDescriptionChange>(nameof(Description)).Value;
			set => SetEvent(nameof(Description), new ProductDescriptionChange(value));
		}

		public string Color
		{
			get => GetEvent<ProductColorChange>(nameof(Color)).Value;
			set => SetEvent(nameof(Color), new ProductColorChange(value));
		}

		public string Size
		{
			get => GetEvent<ProductSizeChange>(nameof(Size)).Value;
			set => SetEvent(nameof(Size), new ProductSizeChange(value));
		}

		public IEnumerable<string> AddedTags
		{
			get => GetEventCollection<ProductTagAdd>(nameof(AddedTags))?.Select(x => x.Value);
			set => SetEventCollection(nameof(AddedTags), value?.Select(x => new ProductTagAdd(x)));
		}
	}
}