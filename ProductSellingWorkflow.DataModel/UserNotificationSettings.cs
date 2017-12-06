using ProductSellingWorkflow.Common.Enums;

namespace ProductSellingWorkflow.DataModel
{
	public class UserNotificationSettings
	{
		public int NotificationTypeId { get; set; }
		public NotificationType NotificationType { get; set; }

		public int UserId { get; set; }
		public User User { get; set; }

		public int RoleId { get; set; }
		public Role Role { get; set; }

		public NotificationKind NotificationKinds { get; set; }
	}
}
