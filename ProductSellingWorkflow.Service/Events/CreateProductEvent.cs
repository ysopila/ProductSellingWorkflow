using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace ProductSellingWorkflow.Service.Events
{
	public class CreateProductEvent : ComplexProductEvent
	{
		private ProductNameChange _name;
		private ProductDescriptionChange _description;
		private ProductColorChange _color;
		private ProductSizeChange _size;
		private IEnumerable<ProductTagAdd> _tagsAdd;

		protected override ProductLogType Type => ProductLogType.Create;

		internal override EventResult Apply(Product product, bool createLog = true)
		{
			var result = base.Apply(product, createLog);
			if (result.Success)
			{
				result += _name?.Apply(product, createLog);
				result += _description?.Apply(product, createLog);
				result += _color?.Apply(product, createLog);
				result += _size?.Apply(product, createLog);

				if (_tagsAdd != null)
					foreach (var t in _tagsAdd)
						result += t.Apply(product, createLog);
			}
			return result;
		}

		public string Name
		{
			get { return _name?.Value; }
			set { _name = new ProductNameChange(value, Type, OperationId); }
		}

		public string Description
		{
			get { return _description?.Value; }
			set { _description = new ProductDescriptionChange(value, Type, OperationId); }
		}

		public string Color
		{
			get { return _color?.Value; }
			set { _color = new ProductColorChange(value, Type, OperationId); }
		}

		public string Size
		{
			get { return _size?.Value; }
			set { _size = new ProductSizeChange(value, Type, OperationId); }
		}

		public IEnumerable<ProductTag> AddedTags
		{
			get { return _tagsAdd?.Select(x => x.Value); }
			set { _tagsAdd = value?.Select(x => new ProductTagAdd(x, Type, OperationId)); }
		}
	}
}