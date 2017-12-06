using ProductSellingWorkflow.Common.Enums;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Common.Core
{
	public class Notifications
	{
		public static IDictionary<string, NotificationKind> DefaultAdminNotifications = new Dictionary<string, NotificationKind> {
			{ PRODUCT_CHANGED, NotificationKind.Email | NotificationKind.PushNotification },
			{ PRODUCT_SOLD, NotificationKind.Email },
			{ PRODUCT_MOVED_TO_CATALOG, NotificationKind.None }
		};

		public static IDictionary<string, NotificationKind> DefaultSellerNotifications = DefaultAdminNotifications;

		public static IDictionary<string, NotificationKind> DefaultBuyerNotifications = new Dictionary<string, NotificationKind> {
			{ WATCH_LIST_PRODUCT_SOLD, NotificationKind.Email | NotificationKind.PushNotification },
			{ WATCH_LIST_PRODUCT_CHANGED, NotificationKind.Email },
			{ NEW_PRODUCT_APPEARED, NotificationKind.None }
		};

		public const string PRODUCT_CHANGED = "Product changed";
		public const string PRODUCT_SOLD = "Product sold";
		public const string PRODUCT_MOVED_TO_CATALOG = "Product moved to catalog";
		public const string WATCH_LIST_PRODUCT_SOLD = "Watch list product sold";
		public const string WATCH_LIST_PRODUCT_CHANGED = "Watch list product changed";
		public const string NEW_PRODUCT_APPEARED = "New product appeared";
	}
}
