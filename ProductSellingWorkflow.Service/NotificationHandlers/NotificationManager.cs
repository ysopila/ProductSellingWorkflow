using ProductSellingWorkflow.Repository.Abstractions;
using ProductSellingWorkflow.Service.Abstractions;
using ProductSellingWorkflow.Service.Notifications;

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
	}
}
