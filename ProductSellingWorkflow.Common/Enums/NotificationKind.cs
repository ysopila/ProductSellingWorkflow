using System;

namespace ProductSellingWorkflow.Common.Enums
{
	[Flags]
	public enum NotificationKind
	{
		None = 0,
		Web = 1,
		Email = 2,
		PushNotification = 4
	}
}
