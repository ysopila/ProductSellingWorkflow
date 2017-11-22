using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.Repository.Abstractions;
using ProductSellingWorkflow.Service.Events;
using ProductSellingWorkflow.Service.Events.Product.Properties;
using System.Linq;

namespace ProductSellingWorkflow.Service.EventHandlers.Product.Properties
{
	class ProductTagAddChangeHandler : ProductPropertyHandler<ProductTagAdd>
	{
		private IUnitOfWork UnitOfWork { get; }

		protected override ProductLogType Type => ProductLogType.Add;

		public ProductTagAddChangeHandler(DataModel.Product entity, OperationContext context, IUnitOfWork unitOfWork) : base(entity, context)
		{
			UnitOfWork = unitOfWork;
		}

		public override EventResult Apply(ProductTagAdd e, IEventOptions options)
		{
			var result = base.Apply(e, options);

			var tag = UnitOfWork.TagRepository.Find(x => x.Name == e.Value).FirstOrDefault();
			if (tag == null)
				Entity.ProductTags.Add(new DataModel.ProductTag { ProductId = Entity.Id, Tag = new DataModel.Tag { Name = e.Value } });
			else
				Entity.ProductTags.Add(new DataModel.ProductTag { ProductId = Entity.Id, TagId = tag.Id });

			if (options.Store)
				CreateLog("Tags", e.Value.ToString());
			return result;
		}
	}
}