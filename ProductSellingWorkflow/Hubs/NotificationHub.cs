using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace ProductSellingWorkflow.Hubs
{
	[HubName("NotificationHub")]
	public class NotificationHub : Hub
	{
		public void Subscribe(int userId)
		{
			Groups.Add(Context.ConnectionId, userId.ToString());
		}
	}
}