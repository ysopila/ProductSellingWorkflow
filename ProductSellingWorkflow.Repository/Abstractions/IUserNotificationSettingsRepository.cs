using ProductSellingWorkflow.Data.Views;
using ProductSellingWorkflow.DataModel;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Repository.Abstractions
{
	public interface IUserNotificationSettingsRepository : IRepository<UserNotificationSettings>
	{
		IEnumerable<NotificationSettingsView> GetNotificationSettings(int userId);
	}
}
