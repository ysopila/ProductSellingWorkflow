using ProductSellingWorkflow.DataModel;
using System.Data.Entity.ModelConfiguration;

namespace ProductSellingWorkflow.Data.Configuration
{
	class UserNotificationConfiguration : EntityTypeConfiguration<UserNotification>
	{
		public UserNotificationConfiguration()
		{
			HasKey(x => new { x.UserId, x.NotificationId, x.Kind });

			HasRequired(x => x.User)
				.WithMany(x => x.UserNotifications)
				.HasForeignKey(x => x.UserId);


			HasRequired(x => x.Notification)
				.WithMany(x => x.UserNotifications)
				.HasForeignKey(x => x.NotificationId);
		}
	}
}
