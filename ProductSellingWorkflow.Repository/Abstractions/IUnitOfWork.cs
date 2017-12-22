using ProductSellingWorkflow.DataModel;
using System;

namespace ProductSellingWorkflow.Repository.Abstractions
{
	public interface IUnitOfWork : IDisposable
	{
		void Save();
		void Rollback();
		void Recycle();

		IProductRepository ProductRepository { get; }
		IWatchListRepository WatchListRepository { get; }
		IRepository<ProductLog> ProductLogRepository { get; }
		IRepository<ProductTag> ProductTagRepository { get; }
		IRepository<Tag> TagRepository { get; }
		IUserRepository UserRepository { get; }
		IRepository<Role> RoleRepository { get; }
		IRepository<NotificationType> NotificationTypeRepository { get; }
		IUserNotificationSettingsRepository UserNotificationSettingsRepository { get; }
		IRepository<UserNotification> UserNotificationRepository { get; }
	}
}
