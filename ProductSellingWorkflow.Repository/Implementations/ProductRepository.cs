using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.Data;
using ProductSellingWorkflow.Data.Views;
using ProductSellingWorkflow.DataModel;
using ProductSellingWorkflow.Repository.Abstractions;
using System;
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
			get { return base.Query.Include(x => x.ProductTags); }
		}
		public ProductView GetOne(int id) => ProductSet.Where(x => x.Id == id).FirstOrDefault();

		public IEnumerable<ProductView> GetAllForOwner(int ownerId) => ProductSet.Where(x => x.CreatedBy.Id == ownerId).ToList();

		public IEnumerable<ProductView> GetAllForAdmin() => ProductSet.Where(x => x.State == ProductState.UnProcessed).ToList();

		public IEnumerable<ProductBaseView> GetCatalog() => ProductSet.Where(x => x.State == ProductState.InCatalog).ToList();

		private IQueryable<GroupedProductLog> GroupedProductLogQuery =>
			from x in Context.Set<ProductLog>()
			group x by x.ProductId into o
			select new GroupedProductLog
			{
				ProductId = o.Key,
				CreatedAt = o.Min(x => x.CreatedAt),
				ModifiedAt = o.Max(x => x.CreatedAt),
			};


		private IQueryable<ProductView> ProductSet =>
			from x in Context.Set<Product>()
			join o in GroupedProductLogQuery on x.Id equals o.ProductId
			select new ProductView
			{
				Id = x.Id,
				Name = x.Name,
				Color = x.Color,
				Description = x.Description,
				Size = x.Size,
				Price = x.Price,
				State = x.State,
				CreatedBy = new UserView
				{
					Id = x.CreatedBy.Id,
					Email = x.CreatedBy.Email,
					Name = x.CreatedBy.Name
				},
				CreatedAt = o.CreatedAt,
				ModifiedAt = o.ModifiedAt,
				Tags = x.ProductTags.Select(s => s.Tag.Name)
			};

		private class GroupedProductLog
		{
			public int ProductId { get; set; }
			public DateTimeOffset CreatedAt { get; set; }
			public DateTimeOffset ModifiedAt { get; set; }
		}
	}
}
