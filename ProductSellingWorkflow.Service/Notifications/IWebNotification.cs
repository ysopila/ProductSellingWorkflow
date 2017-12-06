namespace ProductSellingWorkflow.Service.Notifications
{
	public interface IWebNotification : INotification
	{
		string UserEmail { get; set; }
		string Message { get; set; }
	}
}
