using System.Collections.Generic;

namespace ProductSellingWorkflow.Models
{
	public class NotificationSettingsViewModel
	{
		public NotificationSettingsViewModel()
		{
			NotificationTypes = new List<NotificationTypeViewModel>();
		}

		public List<NotificationTypeViewModel> NotificationTypes { get; set; }
	}
}