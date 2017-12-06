namespace ProductSellingWorkflow.Service.Notifications
{
	public interface IEmailNotification : INotification
	{
		string From { get; set; }
		string To { get; set; }
		string Subject { get; set; }
		string Message { get; set; }
	}
}
