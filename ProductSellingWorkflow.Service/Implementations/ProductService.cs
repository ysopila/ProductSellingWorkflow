using ProductSellingWorkflow.Common.Core;
using ProductSellingWorkflow.Repository.Abstractions;
using ProductSellingWorkflow.Data.Views;
using ProductSellingWorkflow.Service.Abstractions;
using ProductSellingWorkflow.Service.Events;
using System.Collections.Generic;
using ProductSellingWorkflow.Service.EventHandlers.Product;
using ProductSellingWorkflow.Service.EventHandlers;
using ProductSellingWorkflow.Service.Events.Product;

namespace ProductSellingWorkflow.Service.Implementations
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ISimpleMapper _mapper;
		private readonly IProductEventFactory _factory;

		public ProductService(IUnitOfWork unitOfWork, ISimpleMapper mapper, IProductEventFactory factory)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_factory = factory;
		}

		public ProductView Get(int id)
		{
			var product = _unitOfWork.ProductRepository.GetOne(id);
			return product;
		}

		public IEnumerable<ProductView> GetAll()
		{
			return _unitOfWork.ProductRepository.GetAll();
		}

		public EventResult Create(CreateProductEvent @event)
		{
			return _factory.GetHandler<CreateProductEvent>().Apply(@event, new EventOptions { Store = true });
		}

		public EventResult Update(UpdateProductEvent @event)
		{
			return _factory.GetHandler<UpdateProductEvent>().Apply(@event, new EventOptions { Store = true });
		}
	}
}