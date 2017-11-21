using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.DataModel;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Service.Events
{
	public class CreateProductEvent : ComplexProductEvent
	{
		private ProductNameChange _name;
		private ProductDescriptionChange _description;
		private ProductColorChange _color;
		private ProductSizeChange _size;
		private ProductTagsAdd _tagsAdd;
		private ProductTagsRemove _tagsRemove;

		protected override ProductLogType Type => ProductLogType.Create;

		internal override void Apply(Product product, bool createLog = true)
		{
			_name?.Apply(product, createLog);
			_description?.Apply(product, createLog);
			_color?.Apply(product, createLog);
			_size?.Apply(product, createLog);
			//_tagsAdd?.Apply(product, createLog);
			//_tagsRemove?.Apply(product, createLog);
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

		public IEnumerable<string> AddedTags
		{
			get { return _tagsAdd?.Value; }
			set { _tagsAdd = new ProductTagsAdd(value, Type, OperationId); }
		}

		public IEnumerable<string> RemovedTags
		{
			get { return _tagsRemove?.Value; }
			set { _tagsRemove = new ProductTagsRemove(value, Type, OperationId); }
		}
	}
}
