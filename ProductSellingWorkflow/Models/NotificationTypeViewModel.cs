using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.Data.Views;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Models
{
	public class NotificationTypeViewModel
	{
		public NotificationTypeViewModel()
		{
			SelectedKinds = new List<NotificationKind>();
		}

		public NotificationTypeView NotificationType { get; set; }
		public List<NotificationKind> SelectedKinds { get; set; }
		public NotificationKind CurrentKind { get; set; }
	}
}