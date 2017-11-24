using ProductSellingWorkflow.Repository.Abstractions;
using ProductSellingWorkflow.Service.Abstractions;
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
		private IAuthenticationService AuthenticationService { get; }

		public ProductEventFactory(IUnitOfWork unitOfWork, IAuthenticationService authService)
		{
			UnitOfWork = unitOfWork;
			AuthenticationService = authService;
		}

		public EventHandlerBase GetHandler<T>() where T : EventBase
		{
			if (typeof(T) == typeof(UpdateProductEvent))
				return new UpdateProductEventHandler(UnitOfWork, AuthenticationService);
			if (typeof(T) == typeof(CreateProductEvent))
				return new CreateProductEventHandler(UnitOfWork, AuthenticationService);
			if (typeof(T) == typeof(MoveInCatalogEvent))
				return new MoveInCatalogEventHandler(UnitOfWork, AuthenticationService);
			throw new NotSupportedException("Type is not supported");
		}
	}
}