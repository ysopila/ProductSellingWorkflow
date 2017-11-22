using System.Collections.Generic;
using System.Linq;

namespace ProductSellingWorkflow.Service.Events
{
	public class CreateProductEvent : ComplexEvent
	{
		public string Name
		{
			get { return GetEvent<ProductNameChange>(nameof(Name)).Value; }
			set { SetEvent(nameof(Name), new ProductNameChange(value)); }
		}

		public string Description
		{
			get { return GetEvent<ProductDescriptionChange>(nameof(Description)).Value; }
			set { SetEvent(nameof(Description), new ProductDescriptionChange(value)); }
		}

		public string Color
		{
			get { return GetEvent<ProductColorChange>(nameof(Color)).Value; }
			set { SetEvent(nameof(Color), new ProductColorChange(value)); }
		}

		public string Size
		{
			get { return GetEvent<ProductSizeChange>(nameof(Size)).Value; }
			set { SetEvent(nameof(Size), new ProductSizeChange(value)); }
		}

		public IEnumerable<string> AddedTags
		{
			get { return GetEventCollection<ProductTagAdd>(nameof(AddedTags))?.Select(x => x.Value); }
			set => SetEventCollection(nameof(AddedTags), value?.Select(x => new ProductTagAdd(x)));
		}
	}
}