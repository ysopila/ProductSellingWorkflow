using ProductSellingWorkflow.DataModel;
using System.Data.Entity.ModelConfiguration;

namespace ProductSellingWorkflow.Data.Configuration
{
	class RoleConfiguration : EntityTypeConfiguration<Role>
	{
		public RoleConfiguration()
		{
			HasKey(x => x.Id);

			HasMany(x => x.Users)
				.WithRequired(x => x.Role)
				.HasForeignKey(x => x.RoleId);

			HasMany(x => x.NotificationSettings)
				.WithRequired(x => x.Role)
				.HasForeignKey(x => x.RoleId);
		}
	}
}
