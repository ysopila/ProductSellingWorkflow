using ProductSellingWorkflow.DataModel;
using System.Data.Entity.ModelConfiguration;

namespace ProductSellingWorkflow.Data.Configuration
{
	class WatchListConfiguration : EntityTypeConfiguration<WatchList>
	{
		public WatchListConfiguration()
		{
			HasKey(x => new { x.UserId, x.ProductId });
		}
	}
}
