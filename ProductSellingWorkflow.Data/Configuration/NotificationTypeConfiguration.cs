using ProductSellingWorkflow.DataModel;
using System.Data.Entity.ModelConfiguration;

namespace ProductSellingWorkflow.Data.Configuration
{
	class NotificationTypeConfiguration : EntityTypeConfiguration<NotificationType>
	{
		public NotificationTypeConfiguration()
		{
			HasKey(x => x.Id);

			HasMany(x => x.NotificationSettings)
				.WithRequired(x => x.NotificationType)
				.HasForeignKey(x => x.NotificationTypeId);
		}
	}
}
