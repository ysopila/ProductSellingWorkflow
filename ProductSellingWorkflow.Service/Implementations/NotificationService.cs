using ProductSellingWorkflow.Common.Core;
using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.DataModel;
using ProductSellingWorkflow.Repository.Abstractions;
using ProductSellingWorkflow.Service.Abstractions;
using ProductSellingWorkflow.Service.NotificationHandlers;
using ProductSellingWorkflow.Service.Notifications;
using System;
using System.Linq;

namespace ProductSellingWorkflow.Service.Implementations
{
	public class NotificationService : INotificationService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ISimpleMapper _mapper;
		private readonly INotificationManager _notificationManager;

		public NotificationService(IUnitOfWork unitOfWork, ISimpleMapper mapper, INotificationManager notificationManager)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_notificationManager = notificationManager;
		}

		public void Create(string type)
		{
			var notificationSettings = _unitOfWork.UserNotificationSettingsRepository
				.Find(x => x.NotificationType.Name == type, noTracking: true);

			foreach (var settings in notificationSettings)
			{
				if (settings.NotificationKinds == NotificationKind.None) continue;

				var notification = new Notification
				{
					CreatedAt = DateTimeOffset.UtcNow,
					NotificationTypeId = settings.NotificationTypeId,
					Text = ""
				};

				if (settings.NotificationKinds.HasFlag(NotificationKind.Email))
					_unitOfWork.UserNotificationRepository.Insert(Create(settings, notification, NotificationKind.Email));

				if (settings.NotificationKinds.HasFlag(NotificationKind.Web))
					_unitOfWork.UserNotificationRepository.Insert(Create(settings, notification, NotificationKind.Web));
			}

			_unitOfWork.Save();
		}

		public void SendAll()
		{
			var date = DateTimeOffset.UtcNow;

			var notifications = _unitOfWork.UserNotificationRepository.Find(x => x.Notification.SentAt == null, includeProperties: "Notification, User", noTracking: false);

			foreach (var userNotification in notifications)
			{
				if (userNotification.Kind == NotificationKind.None) continue;

				switch (userNotification.Kind)
				{
					case NotificationKind.Web:
						SendWebNotification(userNotification);
						break;
					case NotificationKind.Email:
						SendEmailNotification(userNotification);
						break;
					case NotificationKind.PushNotification:
						break;
					case NotificationKind.None:
					default:
						break;
				}

				userNotification.Notification.SentAt = DateTimeOffset.UtcNow;

				_unitOfWork.UserNotificationRepository.Update(userNotification);
			}

			if (notifications.Any())
				_unitOfWork.Save();
		}

		private void SendEmailNotification(UserNotification userNotification)
		{

		}

		private void SendWebNotification(UserNotification userNotification)
		{
			_notificationManager.Send(new WebNotification
			{
				UserEmail = userNotification.User.Email,
				Message = userNotification.Notification.Text
			});
		}

		private UserNotification Create(UserNotificationSettings settings, Notification notification, NotificationKind kind)
		{
			return new UserNotification
			{
				CreatedAt = DateTimeOffset.UtcNow,
				ExpiredAt = DateTimeOffset.UtcNow.AddDays(7),
				UserId = settings.UserId,
				Kind = kind,
				Notification = notification
			};
		}
	}
}