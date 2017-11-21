using ProductSellingWorkflow.DataModel;
using System.Data.Entity.ModelConfiguration;

namespace ProductSellingWorkflow.Data.Configuration
{
	class ProductLogConfiguration : EntityTypeConfiguration<ProductLog>
	{
		public ProductLogConfiguration()
		{
			HasKey(x => x.Id);

			HasRequired(x => x.Product)
				.WithMany(x => x.ProductLogs)
				.HasForeignKey(x => x.ProductId);

			HasIndex(x => x.ProductId);
		}
	}
}
