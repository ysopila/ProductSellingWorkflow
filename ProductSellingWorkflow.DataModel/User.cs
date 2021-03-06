﻿using System.Collections.Generic;

namespace ProductSellingWorkflow.DataModel
{
	public class User
	{
		public User()
		{
			Roles = new List<UserInRole>();
			Products = new List<Product>();
			WatchList = new List<WatchList>();
			ProductLogs = new List<ProductLog>();
			NotificationSettings = new List<UserNotificationSettings>();
			UserNotifications = new List<UserNotification>();
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		public ICollection<UserInRole> Roles { get; set; }
		public ICollection<Product> Products { get; set; }
		public ICollection<ProductLog> ProductLogs { get; set; }
		public ICollection<WatchList> WatchList { get; set; }
		public ICollection<UserNotificationSettings> NotificationSettings { get; set; }
		public ICollection<UserNotification> UserNotifications { get; set; }
	}
}
