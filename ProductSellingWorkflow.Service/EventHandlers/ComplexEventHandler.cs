using ProductSellingWorkflow.Repository.Abstractions;
using ProductSellingWorkflow.Service.Events;
using ProductSellingWorkflow.Common.Enums;
using System;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Service.EventHandlers
{
	public abstract class ComplexEventHandler<T, V> : EventHandlerBase<T> where T : ComplexEvent where V: class
	{
		protected abstract LogOperation Operation { get; }
		protected virtual Guid OperationId { get; } = Guid.NewGuid();
		protected IUnitOfWork UnitOfWork { get; }

		protected abstract V GetEntity(T e);
		protected abstract EventResult SaveChanges(V entity);
		protected abstract IList<EventHandlerBase> GetHandlers(V entity);

		protected virtual EventResult ApplyChanges(V entity, T e, IEventOptions options)
		{
			var result = new EventResult();
			var handlers = GetHandlers(entity);
			foreach (var ev in e.Events)
				foreach (var h in handlers)
					h.Apply(ev.Value, options);
			return result;
		}
		protected virtual EventResult Rollback(V entity)
		{
			UnitOfWork.Rollback();
			return new EventResult();
		}


		public ComplexEventHandler(IUnitOfWork unitOfWork)
		{
			UnitOfWork = unitOfWork;
		}

		public sealed override EventResult Apply(T e, IEventOptions options)
		{
			var result = base.Apply(e, options);

			var entity = GetEntity(e);
			result += ApplyChanges(entity, e, options);

			if (result.Success)
				result += SaveChanges(entity);
			else
				result += Rollback(entity);
			return result;
		}
	}
}