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
			get { return base.Query.Include(x => x.ProductLogs).Include(x => x.ProductTags.Select(s => s.Tag)); }
		}
		public ProductModel GetOne(int id) => ProductSet.Where(x => x.Id == id).FirstOrDefault();

		public IEnumerable<ProductModel> GetAll() => ProductSet.ToList();

		private IQueryable<ProductModel> ProductSet
		{
			get
			{
				var queryLog =
					from l in Context.Set<ProductLog>()
					group l by l.ProductId into grouped
					select new
					{
						ProductId = grouped.Key,
						CreatedAt = grouped.Min(x => x.CreatedAt),
						ModifiedAt = grouped.Max(x => x.CreatedAt),
					};

				return
					from x in Context.Set<Product>()
					join l in queryLog on x.Id equals l.ProductId
					select new ProductModel
					{
						Id = x.Id,
						Name = x.Name,
						Color = x.Color,
						Description = x.Description,
						Size = x.Size,
						State = x.State,
						CreatedAt = l.CreatedAt,
						ModifiedAt = l.ModifiedAt,
						Tags = x.ProductTags.Select(s => s.Tag.Name)
					};
			}
		}
	}
}
