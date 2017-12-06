using ProductSellingWorkflow.Common.Core;
using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.Data.Views;
using ProductSellingWorkflow.DataModel;
using ProductSellingWorkflow.Repository.Abstractions;
using ProductSellingWorkflow.Service.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace ProductSellingWorkflow.Service.Implementations
{
	public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ISimpleMapper _mapper;

		public UserService(IUnitOfWork unitOfWork, ISimpleMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public bool Create(string name, string email, string password, List<string> roles, out UserView user)
		{
			user = _unitOfWork.UserRepository.GetOne(email);

			if (user == null)
			{
				var entity = new User
				{
					Name = name,
					Email = email,
					Password = password
				};

				AddRoles(entity, roles);

				AddDefaultNotifications(entity);

				_unitOfWork.UserRepository.Insert(entity);
				_unitOfWork.Save();

				user = _unitOfWork.UserRepository.GetOne(email);
				return true;
			}

			return false;
		}

		private void AddRoles(User entity, List<string> roles)
		{
			var rolesInDatabase = _unitOfWork.RoleRepository.Find(x => roles.Contains(x.Name), noTracking: false);

			foreach (var role in roles)
			{
				var roleInDatabase = rolesInDatabase.FirstOrDefault(x => x.Name == role);
				roleInDatabase = roleInDatabase ?? new Role { Name = role };
				entity.Roles.Add(new UserInRole { Role = roleInDatabase });
			}
		}

		private void AddDefaultNotifications(User entity)
		{
			if (entity.Roles.Any())
			{
				var notificationTypes = _unitOfWork.NotificationTypeRepository.Find(noTracking: false);

				foreach (var role in entity.Roles)
				{
					switch (role.Role.Name)
					{
						case Roles.Admin: AddNotifications(entity, role.Role, notificationTypes, Common.Core.Notifications.DefaultAdminNotifications); break;
						case Roles.Seller: AddNotifications(entity, role.Role, notificationTypes, Common.Core.Notifications.DefaultSellerNotifications); break;
						case Roles.Buyer: AddNotifications(entity, role.Role, notificationTypes, Common.Core.Notifications.DefaultBuyerNotifications); break;
						default: break;
					}
				}
			}
		}

		private void AddNotifications(User entity, Role role, IEnumerable<NotificationType> notificationTypes, IDictionary<string, NotificationKind> notifications)
		{
			foreach (var notification in notifications)
			{
				var notificationType = notificationTypes.FirstOrDefault(x => x.Name == notification.Key);
				entity.NotificationSettings.Add(new UserNotificationSettings { Role = role, NotificationKinds = notification.Value, NotificationType = notificationType });
			}
		}

		private void UpdateNotifications(User entity, Role role, IEnumerable<NotificationType> notificationTypes, IDictionary<int, NotificationKind> notifications)
		{
			foreach (var notification in notifications)
			{
				var notificationType = notificationTypes.FirstOrDefault(x => x.Id == notification.Key);
				var settings = entity.NotificationSettings.FirstOrDefault(x => x.NotificationTypeId == notificationType.Id && x.RoleId == role.Id);

				if (settings == null)
					entity.NotificationSettings.Add(new UserNotificationSettings { Role = role, NotificationKinds = notification.Value, NotificationType = notificationType });
				else
					settings.NotificationKinds = notification.Value;
			}
		}

		public IEnumerable<NotificationSettingsView> GetNotificationSettings(int userId)
		{
			return _unitOfWork.UserNotificationSettingsRepository.GetNotificationSettings(userId);
		}

		public IEnumerable<NotificationTypeView> GetNotificationTypes()
		{
			return _unitOfWork.NotificationTypeRepository.Find().Select(x => new NotificationTypeView { Id = x.Id, Name = x.Name }).ToList();
		}

		public void UpdateNotificationSettings(int userId, IDictionary<int, NotificationKind> selectedNotifications)
		{
			var user = _unitOfWork.UserRepository.Find(x => x.Id == userId, includeProperties: "Roles.Role, NotificationSettings", noTracking: false).First();
			var notificationTypes = _unitOfWork.NotificationTypeRepository.Find(noTracking: false);

			foreach (var role in user.Roles)
				UpdateNotifications(user, role.Role, notificationTypes, selectedNotifications);

			_unitOfWork.UserRepository.Update(user);
			_unitOfWork.Save();
		}

		public UserView GetByEmail(string email)
		{
			return _unitOfWork.UserRepository.GetOne(email);
		}

		public bool Validate(string email, string password, out UserView user)
		{
			user = _unitOfWork.UserRepository.GetOne(email);

			if (user == null || !string.Equals(user.Password, password))
				return false;

			return true;
		}

		public IEnumerable<int> GetWatchList(int userId)
		{
			return _unitOfWork.WatchListRepository.GetWatchList(userId);
		}

	}
}
