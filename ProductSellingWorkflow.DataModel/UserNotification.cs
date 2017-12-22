using ProductSellingWorkflow.Common.Enums;
using System;

namespace ProductSellingWorkflow.DataModel
{
	public class UserNotification
	{
		public UserNotification()
		{
		}

		public int UserId { get; set; }
		public User User { get; set; }

		public int NotificationId { get; set; }
		public Notification Notification { get; set; }

		public NotificationKind Kind { get; set; }

		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset? ReadAt { get; set; }
		public DateTimeOffset ExpiredAt { get; set; }
	}
}
