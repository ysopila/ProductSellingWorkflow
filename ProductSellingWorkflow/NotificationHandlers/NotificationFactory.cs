using ProductSellingWorkflow.Service.NotificationHandlers;
using ProductSellingWorkflow.Service.Notifications;
using System;

namespace ProductSellingWorkflow.NotificationHandlers
{
	public class NotificationFactory : INotificationFactory
	{
		public NotificationHandler GetHandler<T>() where T : INotification
		{
			var notificationType = typeof(T);

			if (notificationType == typeof(IEmailNotification))
				return new EmailNotificationHandler();
			if (notificationType == typeof(IWebNotification))
				return new WebNotificationHandler();
			if (notificationType == typeof(IPushNotification))
				return new PushNotificationHandler();

			throw new NotImplementedException();
		}
	}
}