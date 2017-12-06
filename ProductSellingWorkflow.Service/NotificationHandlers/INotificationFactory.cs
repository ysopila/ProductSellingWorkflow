using ProductSellingWorkflow.Service.Notifications;

namespace ProductSellingWorkflow.Service.NotificationHandlers
{
	public interface INotificationFactory
	{
		NotificationHandler GetHandler<T>() where T : INotification;
	}
}
