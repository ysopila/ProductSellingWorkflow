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

		public void Create(CreateProductEvent @event)
		{
			var product = new Product();

			SetProperties(product, @event);

			_unitOfWork.ProductRepository.Insert(product);
			_unitOfWork.Save();
		}

		public void Update(UpdateProductEvent @event)
		{
			var product = _unitOfWork.ProductRepository.Find(x => x.Id == @event.Id).FirstOrDefault();

			SetProperties(product, @event);

			_unitOfWork.ProductRepository.Update(product);
			_unitOfWork.Save();
		}

		private void SetProperties(Product product, ChangeProductEvent @event)
		{
			var logs = new List<ProductLog>();
			var operationId = Guid.NewGuid();
			var type = product.GetType();

			foreach (var eventProp in @event.Values)
			{
				var operation = SetKeys.Contains(eventProp.Key) ? ProductLogOperation.Set : ProductLogOperation.Add;

				var log = new ProductLog
				{
					OperationId = operationId,
					CreatedAt = DateTimeOffset.UtcNow,
					Property = eventProp.Key,
					Value = eventProp.Value.ToString(),
					Operation = operation,
					Type = @event.Type
				};

				var prop = type.GetProperty(eventProp.Key);

				if (prop != null)
				{
					prop.SetValue(product, eventProp.Value, null);
				}

				product.ProductLogs.Add(log);
			}
		}
	}
}
