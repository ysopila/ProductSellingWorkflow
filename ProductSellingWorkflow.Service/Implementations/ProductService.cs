using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.DataModel;
using ProductSellingWorkflow.Repository.Abstractions;
using ProductSellingWorkflow.Service.Abstractions;
using ProductSellingWorkflow.Service.Events;
using ProductSellingWorkflow.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductSellingWorkflow.Service.Implementations
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;

		private static List<string> SetKeys = new List<string> { "Name", "Description", "Size", "Color" };

		public ProductService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public ProductDTO Get(int id)
		{
			var product = _unitOfWork.ProductRepository.GetOne(id);

			return new ProductDTO
			{
				Id = product.Id,
				Color = product.Color,
				CreatedAt = product.CreatedAt,
				Description = product.Description,
				ModifiedAt = product.ModifiedAt,
				Tags = product.Tags,
				Name = product.Name,
				Size = product.Size,
				State = product.State
			};
		}

		public IEnumerable<ProductDTO> GetAll()
		{
			return _unitOfWork.ProductRepository
				.GetAll()
				.Select(x => new ProductDTO
				{
					Id = x.Id,
					Color = x.Color,
					CreatedAt = x.CreatedAt,
					Description = x.Description,
					ModifiedAt = x.ModifiedAt,
					Tags = x.Tags,
					Name = x.Name,
					Size = x.Size,
					State = x.State
				});
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
			return result;
		}

		public EventResult Update(UpdateProductEvent @event)
		{
			var product = _unitOfWork.ProductRepository.Find(x => x.Id == @event.Id).FirstOrDefault();

			var result = @event.Apply(product, true);
			if (result.Success)
			{
				_unitOfWork.ProductRepository.Update(product);
				_unitOfWork.Save();
			}
			return result;
		}
	}
}