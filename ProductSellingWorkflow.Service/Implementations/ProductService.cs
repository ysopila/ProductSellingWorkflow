using ProductSellingWorkflow.Common.Core;
using ProductSellingWorkflow.DataModel;
using ProductSellingWorkflow.Repository.Abstractions;
using ProductSellingWorkflow.Data.Views;
using ProductSellingWorkflow.Service.Abstractions;
using ProductSellingWorkflow.Service.Events;
using System.Collections.Generic;
using System.Linq;

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
			var product = new Product();

			var result = @event.Apply(product, true);
			if (result.Success)
			{
				_unitOfWork.ProductRepository.Insert(product);
				_unitOfWork.Save();
			}
			else
			{
				_unitOfWork.Rollback();
			}
			return result;
		}

		public EventResult Update(UpdateProductEvent @event)
		{
			var product = _unitOfWork.ProductRepository.Find(x => x.Id == @event.Id, noTracking: false).FirstOrDefault();

			var result = @event.Apply(product, true);
			if (result.Success)
			{
				_unitOfWork.ProductRepository.Update(product);
				_unitOfWork.Save();
			}
			else
			{
				_unitOfWork.Rollback();
			}
			return result;
		}
	}
}