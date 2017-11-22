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

		public ProductService(IUnitOfWork unitOfWork, ISimpleMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
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
			return new CreateProductEventHandler(_unitOfWork).Apply(@event, new EventOptions { Store = true });
		}

		public EventResult Update(UpdateProductEvent @event)
		{
			return new UpdateProductEventHandler(_unitOfWork).Apply(@event, new EventOptions { Store = true });
		}
	}
}