namespace ProductSellingWorkflow.Service.Notifications
{
	public class WebNotification : NotificationBase, IWebNotification
	{
		public string UserEmail { get; set; }
		public string Message { get; set; }
	}
}
