using ProductSellingWorkflow.Common.Core;
using ProductSellingWorkflow.DataModel;
using ProductSellingWorkflow.Repository.Abstractions;
using ProductSellingWorkflow.Repository.Models;
using ProductSellingWorkflow.Service.Abstractions;
using ProductSellingWorkflow.Service.Events;
using ProductSellingWorkflow.Service.Models;
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

		public ProductDTO Get(int id)
		{
			var product = _unitOfWork.ProductRepository.GetOne(id);
			return _mapper.Map<ProductModel, ProductDTO>(product);
		}

		public IEnumerable<ProductDTO> GetAll()
		{
			return _unitOfWork.ProductRepository.GetAll().Map(_mapper.GetMapper<ProductModel, ProductDTO>());
		}

		public void Create(CreateProductEvent @event)
		{
			var product = new Product();

			@event.Apply(product, true);

			_unitOfWork.ProductRepository.Insert(product);
			_unitOfWork.Save();
		}

		public void Update(UpdateProductEvent @event)
		{
			var product = _unitOfWork.ProductRepository.Find(x => x.Id == @event.Id).FirstOrDefault();

			@event.Apply(product, true);

			//if (@event.AddedTags != null)
			//{
			//	var allTags = _unitOfWork.TagRepository.Find(x => @event.AddedTags.Contains(x.Name));

			//	foreach (var item in @event.AddedTags)
			//	{
			//		var tag = allTags.FirstOrDefault(x => x.Name == item) ?? new Tag { Name = item };

			//		product.ProductTags.Add(new ProductTag { Tag = tag });
			//	}
			//}

			//if (@event.RemovedTags != null)
			//{
			//	foreach (var item in @event.RemovedTags)
			//	{
			//		var productTag = product.ProductTags.FirstOrDefault(x => x.Tag.Name == item);
			//		_unitOfWork.ProductTagRepository.Delete(productTag);
			//	}
			//}

			_unitOfWork.ProductRepository.Update(product);
			_unitOfWork.Save();
		}
	}
}