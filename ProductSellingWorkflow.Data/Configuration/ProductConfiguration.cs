using ProductSellingWorkflow.DataModel;
using System.Data.Entity.ModelConfiguration;

namespace ProductSellingWorkflow.Data.Configuration
{
	class ProductConfiguration : EntityTypeConfiguration<Product>
	{
		public ProductConfiguration()
		{
			HasKey(x => x.Id);

			HasMany(x => x.ProductLogs)
				.WithRequired(x => x.Product)
				.HasForeignKey(x => x.ProductId);

			HasMany(x => x.ProductTags)
				.WithRequired(x => x.Product)
				.HasForeignKey(x => x.ProductId);

			HasMany(x => x.WatchList)
				.WithRequired(x => x.Product)
				.HasForeignKey(x => x.ProductId);
		}
	}
}
