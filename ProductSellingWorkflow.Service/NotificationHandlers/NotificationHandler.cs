using ProductSellingWorkflow.Service.Notifications;

namespace ProductSellingWorkflow.Service.NotificationHandlers
{
	public abstract class NotificationHandler
	{
		public abstract void Handle(INotification notification);
		public abstract bool CanHandle(INotification notification);
	}
}
