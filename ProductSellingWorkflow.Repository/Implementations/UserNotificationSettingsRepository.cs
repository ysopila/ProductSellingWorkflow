using ProductSellingWorkflow.Data;
using ProductSellingWorkflow.Data.Views;
using ProductSellingWorkflow.DataModel;
using ProductSellingWorkflow.Repository.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace ProductSellingWorkflow.Repository.Implementations
{
	internal class UserNotificationSettingsRepository : Repository<UserNotificationSettings>, IUserNotificationSettingsRepository
	{
		public UserNotificationSettingsRepository(IDbContext context) : base(context) { }

		public IEnumerable<NotificationSettingsView> GetNotificationSettings(int userId)
		{
			return Query
				.Where(x => x.UserId == userId)
				.Select(x => new NotificationSettingsView
				{
					NotificationKinds = x.NotificationKinds,
					NotificationTypeId = x.NotificationTypeId,
					RoleId = x.RoleId
				})
				.ToList();
		}
	}
}
