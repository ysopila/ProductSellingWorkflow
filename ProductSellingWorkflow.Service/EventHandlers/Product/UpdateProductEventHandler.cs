using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.Repository.Abstractions;
using ProductSellingWorkflow.Service.EventHandlers.Product.Properties;
using ProductSellingWorkflow.Service.Events;
using ProductSellingWorkflow.Service.Events.Product;
using System.Collections.Generic;
using System.Linq;

namespace ProductSellingWorkflow.Service.EventHandlers.Product
{
	public class UpdateProductEventHandler : CreateProductEventHandler
	{
		public UpdateProductEventHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		protected override LogOperation Operation => LogOperation.Modify;

		public override bool CanHandle(EventBase e)
		{
			return e is UpdateProductEvent;
		}

		protected override DataModel.Product GetEntity(CreateProductEvent e)
		{
			var u = (UpdateProductEvent)e;
			return UnitOfWork.ProductRepository.Find(x => x.Id == u.Id, noTracking: false).FirstOrDefault();
		}

		protected override IList<EventHandlerBase> GetHandlers(DataModel.Product entity)
		{
			var h = base.GetHandlers(entity);
			h.Add(new ProductTagRemoveChangeHandler(entity, GetOperationContext(), UnitOfWork));
			return h;
		}

		protected override EventResult SaveChanges(DataModel.Product entity)
		{
			UnitOfWork.ProductRepository.Update(entity);
			UnitOfWork.Save();
			return new EventResult();
		}
	}
}