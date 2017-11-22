using ProductSellingWorkflow.Service.Events.Product.Properties;
using System.Collections.Generic;
using System.Linq;

namespace ProductSellingWorkflow.Service.Events.Product
{
	public class UpdateProductEvent : CreateProductEvent
	{
		public UpdateProductEvent(int id)
		{
			Id = id;
		}

		public int Id { get; }

		public IEnumerable<string> RemovedTags
		{
			get => GetEventCollection<ProductTagRemove>(nameof(RemovedTags))?.Select(x => x.Value);
			set => SetEventCollection(nameof(RemovedTags), value?.Select(x => new ProductTagRemove(x)));
		}
	}
}