using ProductSellingWorkflow.DataModel;
using System.Data.Entity.ModelConfiguration;

namespace ProductSellingWorkflow.Data.Configuration
{
	class UserInRoleConfiguration : EntityTypeConfiguration<UserInRole>
	{
		public UserInRoleConfiguration()
		{
			HasKey(x => new { x.UserId, x.RoleId });
		}
	}
}
