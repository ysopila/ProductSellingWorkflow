using Microsoft.AspNet.SignalR;
using ProductSellingWorkflow.Hubs;
using ProductSellingWorkflow.Service.NotificationHandlers;
using ProductSellingWorkflow.Service.Notifications;

namespace ProductSellingWorkflow.NotificationHandlers
{
	public class WebNotificationHandler : NotificationHandlerBase<WebNotification>
	{
		public override bool CanHandle(INotification notification)
		{
			return notification is WebNotification;
		}

		public override void Handle(WebNotification notification)
		{
			GlobalHost.ConnectionManager
				.GetHubContext<NotificationHub>()
				.Clients
				.Group(notification.UserEmail)
				.notify(notification.Message);
		}
	}
}