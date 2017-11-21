using ProductSellingWorkflow.Data;
using ProductSellingWorkflow.DataModel;
using ProductSellingWorkflow.Repository.Abstractions;
using ProductSellingWorkflow.Repository.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ProductSellingWorkflow.Repository.Implementations
{
	internal class ProductRepository : Repository<Product>, IProductRepository
	{
		public ProductRepository(IDbContext context) : base(context) { }

		protected override IQueryable<Product> Query
		{
			get { return base.Query.Include(x => x.ProductLogs); }
		}

		public ProductModel GetOne(int id)
		{
			return ProductSet.Where(x => x.Id == id).FirstOrDefault();
		}

		public IEnumerable<ProductModel> GetAll()
		{
			return ProductSet.ToList();
		}

		private IQueryable<ProductModel> ProductSet
		{
			get
			{
				return Query.Select(x => new ProductModel
				{
					Id = x.Id,
					Name = x.Name,
					Color = x.Color,
					Description = x.Description,
					Size = x.Size,
					State = x.State,
					CreatedAt = x.ProductLogs.OrderBy(s => s.CreatedAt).Select(s => s.CreatedAt).FirstOrDefault(),
					ModifiedAt = x.ProductLogs.OrderByDescending(s => s.CreatedAt).Select(s => s.CreatedAt).FirstOrDefault(),
					Tags = x.ProductTags.Select(s => s.Tag.Name)
				});
			}
		}
	}
}
