using ProductSellingWorkflow.Service.Notifications;

namespace ProductSellingWorkflow.Service.NotificationHandlers
{
	public interface INotificationHandler<in T> where T : INotification
	{
		void Handle(T notification);
	}
}
