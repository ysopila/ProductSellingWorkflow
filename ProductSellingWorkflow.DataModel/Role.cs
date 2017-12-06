using System.Collections.Generic;

namespace ProductSellingWorkflow.DataModel
{
	public class Role
	{
		public Role()
		{
			Users = new List<UserInRole>();
			NotificationSettings = new List<UserNotificationSettings>();
		}

		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<UserInRole> Users { get; set; }
		public ICollection<UserNotificationSettings> NotificationSettings { get; set; }
	}
}
