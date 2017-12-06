using ProductSellingWorkflow.Service.Notifications;

namespace ProductSellingWorkflow.Service.NotificationHandlers
{
	public abstract class NotificationHandlerBase<T> : NotificationHandler where T : Notification
	{
		public virtual void Handle(T notification)
		{
		}

		public override void Handle(INotification notification)
		{
			if (CanHandle(notification))
				Handle((T)notification);
		}

		public override bool CanHandle(INotification notification)
		{
			return notification as T != null;
		}
	}
}
