using ProductSellingWorkflow.Service.Notifications;

namespace ProductSellingWorkflow.Service.NotificationHandlers
{
	public interface INotificationManager<in T> where T : INotification
	{
		void Send(T notification);
	}

	public interface INotificationManager :
		INotificationManager<IWebNotification>,
		INotificationManager<IEmailNotification>,
		INotificationManager<IPushNotification>
	{
	}
}
