using ProductSellingWorkflow.Data;
using ProductSellingWorkflow.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ProductSellingWorkflow.Repository.Implementations
{
	internal class Repository<T> : IRepository<T> where T : class
	{
		private IDbContext _context;

		protected IDbContext Context
		{
			get { return _context; }
		}
		protected virtual IQueryable<T> Query
		{
			get { return _context.Set<T>(); }
		}

		public Repository(IDbContext context)
		{
			_context = context;
		}

		public virtual void Insert(T entity)
		{
			_context.Set<T>().Add(entity);
		}

		public virtual void Delete(T entityToDelete)
		{
			if (_context.Entry(entityToDelete).State == EntityState.Detached)
			{
				_context.Set<T>().Attach(entityToDelete);
			}
			_context.Set<T>().Remove(entityToDelete);
		}

		public virtual void Update(T entityToUpdate)
		{
			_context.Set<T>().Attach(entityToUpdate);
			_context.Entry(entityToUpdate).State = EntityState.Modified;
		}

		public virtual bool Any(Expression<Func<T, bool>> filter)
		{
			return _context.Set<T>().Any(filter);
		}

		public virtual int Count(Expression<Func<T, bool>> filter)
		{
			return _context.Set<T>().Count(filter);
		}

		public virtual IEnumerable<T> Find(
			Expression<Func<T, bool>> filter = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			string includeProperties = "",
			int? skip = null,
			int? take = null)
		{
			IQueryable<T> query = Query;

			if (filter != null)
				query = query.Where(filter);

			foreach (var includeProperty in includeProperties.Split
				(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				query = query.Include(includeProperty);

			if (orderBy != null)
				query = orderBy(query);
			if (skip.HasValue)
				query = query.Skip(skip.Value);
			if (take.HasValue)
				query = query.Take(take.Value);
			return query.ToList();
		}

		public virtual IEnumerable<T> FindNoTracking(
		   Expression<Func<T, bool>> filter = null,
		   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
		   string includeProperties = "",
		   int? skip = null,
		   int? take = null)
		{
			IQueryable<T> query = Query;

			if (filter != null)
				query = query.AsNoTracking().Where(filter);

			foreach (var includeProperty in includeProperties.Split
				(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				query = query.Include(includeProperty);

			if (orderBy != null)
				query = orderBy(query);
			if (skip.HasValue)
				query = query.Skip(skip.Value);
			if (take.HasValue)
				query = query.Take(take.Value);
			return query.ToList();
		}
	}
}
