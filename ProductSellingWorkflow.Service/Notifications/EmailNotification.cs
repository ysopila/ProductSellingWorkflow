namespace ProductSellingWorkflow.Service.Notifications
{
	public class EmailNotification : Notification, IEmailNotification
	{
		public string From { get; set; }
		public string To { get; set; }
		public string Subject { get; set; }
		public string Message { get; set; }
	}
}
