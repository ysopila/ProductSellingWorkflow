using ProductSellingWorkflow.Common.Enums;

namespace ProductSellingWorkflow.Data.Views
{
	public class NotificationSettingsView
	{
		public int RoleId { get; set; }
		public int NotificationTypeId { get; set; }
		public NotificationKind NotificationKinds { get; set; }
	}
}
