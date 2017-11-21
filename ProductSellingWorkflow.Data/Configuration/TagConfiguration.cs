using ProductSellingWorkflow.DataModel;
using System.Data.Entity.ModelConfiguration;

namespace ProductSellingWorkflow.Data.Configuration
{
	class TagConfiguration : EntityTypeConfiguration<Tag>
	{
		public TagConfiguration()
		{
			HasKey(x => x.Id);

			HasMany(x => x.ProductTags)
				.WithRequired(x => x.Tag)
				.HasForeignKey(x => x.TagId);
		}
	}
}
