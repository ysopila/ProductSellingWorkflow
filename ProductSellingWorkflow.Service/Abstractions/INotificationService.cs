namespace ProductSellingWorkflow.Service.Abstractions
{
	public interface INotificationService
	{
		void Create(string type);
		void SendAll();
	}
}
