using ProductSellingWorkflow.Repository.Abstractions;
using ProductSellingWorkflow.Service.Events;
using ProductSellingWorkflow.Service.Events.Product;
using System;

namespace ProductSellingWorkflow.Service.EventHandlers.Product
{
	public interface IProductEventFactory
	{
		EventHandlerBase GetHandler<T>() where T : EventBase;
	}
	public class ProductEventFactory : IProductEventFactory
	{
		private IUnitOfWork UnitOfWork { get; }
		public ProductEventFactory(IUnitOfWork unitOfWork)
		{
			UnitOfWork = unitOfWork;
		}

		public EventHandlerBase GetHandler<T>() where T : EventBase
		{
			if (typeof(T) == typeof(UpdateProductEvent))
				return new UpdateProductEventHandler(UnitOfWork);
			if (typeof(T) == typeof(CreateProductEvent))
				return new CreateProductEventHandler(UnitOfWork);
			throw new NotSupportedException("Type is not supported");
		}
	}
}