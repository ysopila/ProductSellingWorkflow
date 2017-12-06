namespace ProductSellingWorkflow.Data.Migrations
{
	using ProductSellingWorkflow.Common.Core;
	using ProductSellingWorkflow.DataModel;
	using System.Data.Entity.Migrations;

	public sealed class Configuration : DbMigrationsConfiguration<DataContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(DataContext context)
		{
			context.Set<NotificationType>().AddOrUpdate(x => x.Name, new NotificationType[]
			{
				new NotificationType { Name = Notifications.PRODUCT_CHANGED },
				new NotificationType { Name = Notifications.PRODUCT_SOLD },
				new NotificationType { Name = Notifications.PRODUCT_MOVED_TO_CATALOG },
				new NotificationType { Name = Notifications.WATCH_LIST_PRODUCT_CHANGED },
				new NotificationType { Name = Notifications.WATCH_LIST_PRODUCT_SOLD },
				new NotificationType { Name = Notifications.NEW_PRODUCT_APPEARED }
			});
		}
	}
}
