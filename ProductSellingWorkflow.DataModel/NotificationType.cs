using System.Collections.Generic;

namespace ProductSellingWorkflow.DataModel
{
	public class NotificationType
	{
		public NotificationType()
		{
			NotificationSettings = new List<UserNotificationSettings>();
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string Caption { get; set; }

		public ICollection<UserNotificationSettings> NotificationSettings { get; set; }
	}
}
