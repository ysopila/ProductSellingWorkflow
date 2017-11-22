using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.Data.Views;
using ProductSellingWorkflow.Service.Events;
using System;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Service.EventHandlers
{
	public interface IEventOptions
	{
		bool Store { get; }
	}

	public class EventOptions : IEventOptions
	{
		public bool Store { get; set; }
	}

	public interface IEventHandler
	{
		bool CanHandle(EventBase e);
		EventResult Apply(EventBase e, IEventOptions options);
	}

	public abstract class EventHandlerBase : IEventHandler
	{
		public abstract IList<EventValidationError> Validate(EventBase e);

		public abstract EventResult Apply(EventBase e, IEventOptions options);

		public abstract bool CanHandle(EventBase e);
	}

	public abstract class EventHandlerBase<V> : EventHandlerBase where V : EventBase
	{
		public sealed override IList<EventValidationError> Validate(EventBase e)
		{
			var result = new List<EventValidationError>();
			if (CanHandle(e))
				result.AddRange(Validate((V)e));
			return result;
		}

		public sealed override EventResult Apply(EventBase e, IEventOptions options)
		{
			if (!CanHandle(e))
				return new EventResult { };
			return Apply((V)e, options);
		}

		public virtual IList<EventValidationError> Validate(V e)
		{
			return new List<EventValidationError>();
		}

		public virtual EventResult Apply(V e, IEventOptions options)
		{
			if (!CanHandle(e))
				return new EventResult { };
			return new EventResult { Errors = Validate(e) };
		}

		public override bool CanHandle(EventBase e)
		{
			return e as V != null;
		}
	}

	public abstract class PropertyEventHandler<T, V> : EventHandlerBase<V>, IEventHandler where T : class where V : EventBase
	{
		protected T Entity { get; }
		protected OperationContext Context { get; }

		public PropertyEventHandler(T entity, OperationContext context)
		{
			Entity = entity;
			Context = context;
		}
	}

	public class OperationContext
	{
		public Guid OperationId { get; set; }
		public LogOperation Operation { get; set; }
		public UserView CurrentUser { get; set; }
	}
}