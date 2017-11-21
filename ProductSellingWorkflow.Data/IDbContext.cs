using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace ProductSellingWorkflow.Data
{
	public interface IDbContext : IDisposable
	{
		DbSet<TEntity> Set<TEntity>() where TEntity : class;
		DbSet Set(Type entityType);
		int SaveChanges();
		IEnumerable<DbEntityValidationResult> GetValidationErrors();
		DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
		DbEntityEntry Entry(object entity);
		DbChangeTracker ChangeTracker { get; }
	}
}
