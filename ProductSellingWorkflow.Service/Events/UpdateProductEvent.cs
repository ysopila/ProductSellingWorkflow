using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace ProductSellingWorkflow.Service.Events
{
	public class UpdateProductEvent : CreateProductEvent
	{
		private IEnumerable<ProductTagRemove> _tagsRemove;

		protected override ProductLogType Type => ProductLogType.Modify;

		public UpdateProductEvent(int id)
		{
			Id = id;
		}

		internal override EventResult Apply(Product product, bool createLog = true)
		{
			var result = base.Apply(product, createLog);
			if (result.Success)
			{
				if (_tagsRemove != null)
					foreach (var t in _tagsRemove)
						result += t.Apply(product, createLog);
			}
			return result;
		}

		public int Id { get; }

		public IEnumerable<ProductTag> RemovedTags
		{
			get { return _tagsRemove?.Select(x => x.Value); }
			set { _tagsRemove = value?.Select(x => new ProductTagRemove(x, Type, OperationId)); }
		}
	}
}
