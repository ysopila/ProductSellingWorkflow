using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.Repository.Abstractions;
using ProductSellingWorkflow.Service.Abstractions;
using ProductSellingWorkflow.Service.EventHandlers.Product.Properties;
using ProductSellingWorkflow.Service.Events;
using ProductSellingWorkflow.Service.Events.Product;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Service.EventHandlers.Product
{
	public class CreateProductEventHandler : ComplexEventHandler<CreateProductEvent, DataModel.Product>
	{
		public CreateProductEventHandler(IUnitOfWork unitOfWork, IAuthenticationService authService) : base(unitOfWork, authService)
		{
		}

		protected override LogOperation Operation => LogOperation.Create;

		protected override DataModel.Product GetEntity(CreateProductEvent e)
		{
			return new DataModel.Product { CreatedById = AuthenticationService.CurrentUser.Id, State = ProductState.UnProcessed };
		}

		protected override IList<EventHandlerBase> GetHandlers(DataModel.Product entity) => new List<EventHandlerBase>()
			{
				new ProductSizeChangeHandler(entity, GetOperationContext()),
				new ProductPriceChangeHandler(entity, GetOperationContext()),
				new ProductNameChangeHandler(entity, GetOperationContext()),
				new ProductDescriptionChangeHandler(entity, GetOperationContext()),
				new ProductColorChangeHandler(entity, GetOperationContext()),
				new ProductTagAddChangeHandler(entity, GetOperationContext(), UnitOfWork),
			};

		protected override EventResult SaveChanges(DataModel.Product entity)
		{
			UnitOfWork.ProductRepository.Insert(entity);
			UnitOfWork.Save();
			return new EventResult();
		}
	}
}