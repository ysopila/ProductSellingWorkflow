using ProductSellingWorkflow.Repository.Abstractions;
using ProductSellingWorkflow.Service.EventHandlers.Product.Properties;
using ProductSellingWorkflow.Service.Events;
using System.Collections.Generic;
using ProductSellingWorkflow.Common.Enums;

namespace ProductSellingWorkflow.Service.EventHandlers.Product
{
	public class CreateProductEventHandler : ComplexEventHandler<CreateProductEvent, DataModel.Product>
	{
		public CreateProductEventHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		protected override LogOperation Operation => LogOperation.Create;

		protected override DataModel.Product GetEntity(CreateProductEvent e)
		{
			return new DataModel.Product { };
		}

		protected override IList<EventHandlerBase> GetHandlers(DataModel.Product entity)
		{
			return new List<EventHandlerBase>()
			{
				new ProductSizeChangeHandler(entity, new OperationContext { Operation = Operation, OperationId = OperationId }),
				new ProductNameChangeHandler(entity, new OperationContext { Operation = Operation, OperationId = OperationId }),
				new ProductDescriptionChangeHandler(entity, new OperationContext { Operation = Operation, OperationId = OperationId }),
				new ProductColorChangeHandler(entity, new OperationContext { Operation = Operation, OperationId = OperationId }),
				new ProductTagAddChangeHandler(entity, new OperationContext { Operation = Operation, OperationId = OperationId }, UnitOfWork),
			};
		}

		protected override EventResult SaveChanges(DataModel.Product entity)
		{
			UnitOfWork.ProductRepository.Insert(entity);
			UnitOfWork.Save();
			return new EventResult();
		}
	}
}