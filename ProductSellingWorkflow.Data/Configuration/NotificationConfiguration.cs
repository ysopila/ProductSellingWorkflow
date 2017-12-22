using ProductSellingWorkflow.DataModel;
using System.Data.Entity.ModelConfiguration;

namespace ProductSellingWorkflow.Data.Configuration
{
	class NotificationConfiguration : EntityTypeConfiguration<Notification>
	{
		public NotificationConfiguration()
		{
			HasKey(x => x.Id);

			HasRequired(x => x.NotificationType)
				.WithMany(x => x.Notifications)
				.HasForeignKey(x => x.NotificationTypeId);
		}
	}
}
