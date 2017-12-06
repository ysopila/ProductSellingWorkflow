using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.Data.Views;
using ProductSellingWorkflow.DataModel;
using ProductSellingWorkflow.Repository.Abstractions;
using ProductSellingWorkflow.Service.Abstractions;
using ProductSellingWorkflow.Service.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace ProductSellingWorkflow.Service.NotificationHandlers
{
	public class NotificationManager : INotificationManager
	{
		private readonly INotificationFactory _factory;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IAuthenticationService _authService;

		public NotificationManager(IUnitOfWork unitOfWork, IAuthenticationService authService, INotificationFactory factory)
		{
			_unitOfWork = unitOfWork;
			_authService = authService;
			_factory = factory;
		}

		public void Send(IEmailNotification notification)
		{
			_factory.GetHandler<IEmailNotification>().Handle(notification);
		}

		public void Send(IWebNotification notification)
		{
			_factory.GetHandler<IWebNotification>().Handle(notification);
		}

		public void Send(IPushNotification notification)
		{
			_factory.GetHandler<IPushNotification>().Handle(notification);
		}

		public void SendNotifications(string type)
		{
			var currentUser = _authService.CurrentUser;
			var notificationSettings = _unitOfWork.UserNotificationSettingsRepository.Find(includeProperties: "NotificationType, User", noTracking: true);

			SendNotifications(currentUser, notificationSettings.Where(x => x.NotificationType.Name == type).ToList());
		}

		private void SendNotifications(UserView currentUser, IEnumerable<UserNotificationSettings> notificationSettings)
		{
			foreach (var settings in notificationSettings)
			{
				if (settings.NotificationKinds.HasFlag(NotificationKind.Email))
				{
					Send(new EmailNotification
					{
						From = currentUser.Email,
						To = settings.User.Email,
						Message = settings.NotificationType.Name,
						Subject = settings.NotificationType.Name
					});
				}

				if (settings.NotificationKinds.HasFlag(NotificationKind.Web))
				{
					Send(new WebNotification
					{
						UserEmail = settings.User.Email,
						Message = settings.NotificationType.Name
					});
				}
			}
		}
	}
}
