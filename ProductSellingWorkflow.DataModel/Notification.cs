using System;
using System.Collections.Generic;

namespace ProductSellingWorkflow.DataModel
{
	public class Notification
	{
		public Notification()
		{
			UserNotifications = new List<UserNotification>();
		}

		public int Id { get; set; }
		public string Text { get; set; }

		public int NotificationTypeId { get; set; }
		public NotificationType NotificationType { get; set; }

		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset? SentAt { get; set; }

		public ICollection<UserNotification> UserNotifications { get; set; }
	}
}
