using Autofac;
using ProductSellingWorkflow.Service.Abstractions;
using System;
using System.Threading;

namespace ProductSellingWorkflow.NotificationHandlers
{
	public class NotificationJob
	{
		private IContainer _container;
		private readonly ManualResetEvent _stop = new ManualResetEvent(false);
		private TimeSpan _sleepTime = new TimeSpan(0, 1, 0);

		public NotificationJob(IContainer container)
		{
			_container = container;
		}
		public void Start()
		{
			while (!_stop.WaitOne(_sleepTime))
			{
				using (var scope = _container.BeginLifetimeScope())
				{
					var service = (INotificationService)scope.Resolve(typeof(INotificationService));

					service.SendAll();
				}
			}
		}

		public void Stop()
		{
			_stop.Set();
		}

	}
}