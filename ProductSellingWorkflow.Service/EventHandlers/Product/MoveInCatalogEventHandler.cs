using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.Repository.Abstractions;
using ProductSellingWorkflow.Service.Abstractions;
using ProductSellingWorkflow.Service.EventHandlers.Product.Properties;
using ProductSellingWorkflow.Service.Events;
using ProductSellingWorkflow.Service.Events.Product;
using System.Collections.Generic;
using System.Linq;

namespace ProductSellingWorkflow.Service.EventHandlers.Product
{
	public class MoveInCatalogEventHandler : ComplexEventHandler<MoveInCatalogEvent, DataModel.Product>
	{
		public MoveInCatalogEventHandler(IUnitOfWork unitOfWork, IAuthenticationService authService) : base(unitOfWork, authService)
		{
		}

		protected override LogOperation Operation => LogOperation.Modify;

		public override bool CanHandle(EventBase e)
		{
			return e is MoveInCatalogEvent;
		}

		protected override DataModel.Product GetEntity(MoveInCatalogEvent e)
		{
			return UnitOfWork.ProductRepository.Find(x => x.Id == e.Id, noTracking: false).FirstOrDefault();
		}

		protected override IList<EventHandlerBase> GetHandlers(DataModel.Product entity) => new List<EventHandlerBase>()
		{
			new ProductStateChangeHandler(entity, GetOperationContext())
		};

		protected override EventResult SaveChanges(DataModel.Product entity)
		{
			UnitOfWork.ProductRepository.Update(entity);
			UnitOfWork.Save();
			return new EventResult();
		}
	}
}