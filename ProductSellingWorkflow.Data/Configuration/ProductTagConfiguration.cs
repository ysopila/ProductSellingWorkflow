using ProductSellingWorkflow.DataModel;
using System.Data.Entity.ModelConfiguration;

namespace ProductSellingWorkflow.Data.Configuration
{
	class ProductTagConfiguration : EntityTypeConfiguration<ProductTag>
	{
		public ProductTagConfiguration()
		{
			HasKey(x => new { x.ProductId, x.TagId });
		}
	}
}
