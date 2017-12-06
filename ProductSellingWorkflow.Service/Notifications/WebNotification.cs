namespace ProductSellingWorkflow.Service.Notifications
{
	public class WebNotification : Notification, IWebNotification
	{
		public string UserEmail { get; set; }
		public string Message { get; set; }
	}
}
