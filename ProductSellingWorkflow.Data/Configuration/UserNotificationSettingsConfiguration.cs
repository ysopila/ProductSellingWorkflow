using ProductSellingWorkflow.DataModel;
using System.Data.Entity.ModelConfiguration;

namespace ProductSellingWorkflow.Data.Configuration
{
	class UserNotificationSettingsConfiguration : EntityTypeConfiguration<UserNotificationSettings>
	{
		public UserNotificationSettingsConfiguration()
		{
			HasKey(x => new { x.NotificationTypeId, x.UserId, x.RoleId });
		}
	}
}
