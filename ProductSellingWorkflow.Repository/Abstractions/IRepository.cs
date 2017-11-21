using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ProductSellingWorkflow.Repository.Abstractions
{
	public interface IRepository<T>
	{
		IEnumerable<T> Find(
			Expression<Func<T, bool>> filter = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			string includeProperties = "",
			int? skip = null,
			int? take = null,
			bool noTracking = true);
		void Insert(T obj);
		void Update(T obj);
		void Delete(T obj);
		bool Any(Expression<Func<T, bool>> filter);
		int Count(Expression<Func<T, bool>> filter);
	}
}
