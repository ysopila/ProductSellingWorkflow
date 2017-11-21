using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.DataModel;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Service.Events
{
	public class EventValidationError
	{
		public string Property { get; set; }
		public string Message { get; set; }
	}

	public class EventResult
	{
		public IList<EventValidationError> Errors { get; internal set; } = new List<EventValidationError>();

		public bool Success => !Errors.Any();

		public static EventResult operator +(EventResult e1, EventResult e2)
		{
			return new EventResult {
				Errors = e1?.Errors == null 
					? e2?.Errors 
					: e2?.Errors == null
						? e1?.Errors
						: e1.Errors.Union(e2.Errors).ToList()
			};
		}
	}

	public abstract class ProductEvent
	{
		internal virtual EventResult Apply(Product product, bool createLog = true)
		{
			var result = new EventResult();
			result.Errors = Validate(product);
			return result;
		}

		internal virtual IList<EventValidationError> Validate(Product product)
		{
			return new List<EventValidationError>();
		}
	}

	public abstract class ProductPropertyChange<T> : ProductEvent
	{
		public ProductPropertyChange(T value, ProductLogType type, Guid operationId)
		{
			Value = value;
			Type = type;
			OperationId = operationId;
		}

		protected virtual ProductLog CreateLog(string property, string value)
		{
			return new ProductLog
			{
				CreatedAt = DateTimeOffset.UtcNow,
				Operation = Operation,
				OperationId = OperationId,
				Property = property,
				Type = Type,
				Value = value
			};
		}

		protected virtual ProductLogOperation Operation => ProductLogOperation.Set;

		internal Guid OperationId { get; }
		internal T Value { get; }
		internal ProductLogType Type { get; }
	}

	public class ProductColorChange : ProductPropertyChange<string>
	{
		public ProductColorChange(string value, ProductLogType type, Guid operationId) : base(value, type, operationId) { }

		internal override EventResult Apply(Product product, bool createLog = true)
		{
			var result = base.Apply(product, createLog);
			if (result.Success)
			{
				product.Color = Value;

				if (createLog)
					product.ProductLogs.Add(CreateLog("Color", Value.ToString()));
			}
			return result;
		}
	}

	public class ProductStateChange : ProductPropertyChange<ProductState>
	{
		public ProductStateChange(ProductState value, ProductLogType type, Guid operationId) : base(value, type, operationId) { }

		internal override IList<EventValidationError> Validate(Product product)
		{
			var result = base.Validate(product);

			if (product.State != ProductState.InCatalog
				&& Value == ProductState.Sold)
				result.Add(new EventValidationError { Property = "State", Message = "Cannot sell product that is not in catalog" });

			if (product.State == ProductState.Sold
				&& Value == ProductState.InCatalog)
				result.Add(new EventValidationError { Property = "State", Message = "Cannot move sold product in catalog" });

			return result;
		}

		internal override EventResult Apply(Product product, bool createLog = true)
		{
			var result = base.Apply(product, createLog);
			if (result.Success)
			{
				product.State = Value;

				if (createLog)
					product.ProductLogs.Add(CreateLog("State", Value.ToString()));
			}
			return result;
		}
	}

	public abstract class ComplexProductEvent: ProductEvent
	{
		protected Guid OperationId { get; } = new Guid();
		protected abstract ProductLogType Type { get; }

	}

	public class CreateProductEvent : ComplexProductEvent
	{
		private ProductColorChange _color;

		protected override ProductLogType Type => ProductLogType.Create;

		internal override EventResult Apply(Product product, bool createLog = true)
		{
			var result = base.Apply(product, createLog);
			if (result.Success)
			{
				result += _color?.Apply(product, createLog);
			}
			return result;
		}

		public CreateProductEvent()
		{
		}

		public string Color
		{
			get { return _color?.Value; }
			set { _color = new ProductColorChange(value, Type, OperationId); }
		}
	}

	public class UpdateProductEvent : CreateProductEvent
	{
		protected override ProductLogType Type => ProductLogType.Modify;

		public UpdateProductEvent(int id)
		{
			Id = id;
		}

		public int Id { get; }
	}

	public class MoveInCatalogEvent : ComplexProductEvent
	{
		private ProductStateChange _state;
		protected override ProductLogType Type => ProductLogType.StateChange;

		public MoveInCatalogEvent()
		{
			_state = new ProductStateChange(ProductState.InCatalog, Type, OperationId);
		}

		internal override EventResult Apply(Product product, bool createLog = true)
		{
			var result = base.Apply(product, createLog);
			if (result.Success)
			{
				result += _state?.Apply(product, createLog);
			}
			return result;
		}
	}

	public class BuyEvent : ComplexProductEvent
	{
		private ProductStateChange _state;
		protected override ProductLogType Type => ProductLogType.StateChange;

		public BuyEvent()
		{
			_state = new ProductStateChange(ProductState.Sold, Type, OperationId);
		}

		internal override EventResult Apply(Product product, bool createLog = true)
		{
			var result = base.Apply(product, createLog);
			if (result.Success)
			{
				result += _state?.Apply(product, createLog);
			}
			return result;
		}
	}
}