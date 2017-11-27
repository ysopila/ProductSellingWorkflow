using ProductSellingWorkflow.Common.Core;
using ProductSellingWorkflow.Data.Views;
using ProductSellingWorkflow.DataModel;
using ProductSellingWorkflow.Repository.Abstractions;
using ProductSellingWorkflow.Service.Abstractions;
using ProductSellingWorkflow.Service.EventHandlers;
using ProductSellingWorkflow.Service.EventHandlers.Product;
using ProductSellingWorkflow.Service.Events;
using ProductSellingWorkflow.Service.Events.Product;
using System.Collections.Generic;
using System.Linq;

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
			return _unitOfWork.ProductRepository.GetOne(id);
		}

		public IEnumerable<ProductBaseView> GetAll()
		{
			return _unitOfWork.ProductRepository.GetCatalog();
		}

		public IEnumerable<ProductView> GetAllForAdmin()
		{
			return _unitOfWork.ProductRepository.GetAllForAdmin();
		}

		public IEnumerable<ProductView> GetAllForOwner(int ownerId)
		{
			return _unitOfWork.ProductRepository.GetAllForOwner(ownerId);
		}

		public EventResult Create(CreateProductEvent @event)
		{
			return _factory.GetHandler<CreateProductEvent>().Apply(@event, new EventOptions { Store = true });
		}

		public EventResult Update(UpdateProductEvent @event)
		{
			return _factory.GetHandler<UpdateProductEvent>().Apply(@event, new EventOptions { Store = true });
		}

		public EventResult Update(MoveInCatalogEvent @event)
		{
			return _factory.GetHandler<MoveInCatalogEvent>().Apply(@event, new EventOptions { Store = true });
		}

		public void AddToWatchList(int id, int userId)
		{
			var watchList = _unitOfWork.WatchListRepository.Find(x => x.ProductId == id && x.UserId == userId, noTracking: false).FirstOrDefault();

			if (watchList == null)
			{
				watchList = new WatchList { ProductId = id, UserId = userId };

				_unitOfWork.WatchListRepository.Insert(watchList);
				_unitOfWork.Save();
			}
		}

		public void RemoveFromWatchList(int id, int userId)
		{
			var watchList = _unitOfWork.WatchListRepository.Find(x => x.ProductId == id && x.UserId == userId, noTracking: false).FirstOrDefault();

			if (watchList != null)
			{
				_unitOfWork.WatchListRepository.Delete(watchList);
				_unitOfWork.Save();
			}
		}
	}
}