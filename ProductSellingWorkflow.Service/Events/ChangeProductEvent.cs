using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.DataModel;
using System;

namespace ProductSellingWorkflow.Service.Events
{
	public abstract class ProductEvent
	{
		internal abstract void Apply(Product product, bool createLog = true);
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

		internal override void Apply(Product product, bool createLog = true)
		{
			product.Color = Value;

			if (createLog)
				product.ProductLogs.Add(CreateLog("Color", Value.ToString()));
		}
	}

	public class ProductStateChange : ProductPropertyChange<ProductState>
	{
		public ProductStateChange(ProductState value, ProductLogType type, Guid operationId) : base(value, type, operationId) { }

		internal override void Apply(Product product, bool createLog = true)
		{
			product.State = Value;

			if (createLog)
				product.ProductLogs.Add(CreateLog("State", Value.ToString()));
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

		internal override void Apply(Product product, bool createLog = true)
		{
			_color?.Apply(product, createLog);
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

		internal override void Apply(Product product, bool createLog = true)
		{
			_state?.Apply(product, createLog);
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

		internal override void Apply(Product product, bool createLog = true)
		{
			_state?.Apply(product, createLog);
		}
	}
}