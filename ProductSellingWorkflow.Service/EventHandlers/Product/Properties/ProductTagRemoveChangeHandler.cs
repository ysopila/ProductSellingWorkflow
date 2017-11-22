using ProductSellingWorkflow.Repository.Abstractions;
using ProductSellingWorkflow.Service.Events;
using System.Linq;
using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.Service.Events.Product.Properties;

namespace ProductSellingWorkflow.Service.EventHandlers.Product.Properties
{
	class ProductTagRemoveChangeHandler : ProductPropertyHandler<ProductTagRemove>
	{
		private IUnitOfWork UnitOfWork { get; }

		protected override ProductLogType Type => ProductLogType.Remove;

		public ProductTagRemoveChangeHandler(DataModel.Product entity, OperationContext context, IUnitOfWork unitOfWork) : base(entity, context)
		{
			UnitOfWork = unitOfWork;
		}

		public override EventResult Apply(ProductTagRemove e, IEventOptions options)
		{
			var result = base.Apply(e, options);

			var tag = UnitOfWork.TagRepository.Find(x => x.Name == e.Value).FirstOrDefault();
			if (tag == null)
				return result;

			Entity.ProductTags.Remove(Entity.ProductTags.FirstOrDefault(x => x.TagId == tag.Id));

			if (options.Store)
				CreateLog("Tags", e.Value.ToString());
			return result;
		}
	}
}