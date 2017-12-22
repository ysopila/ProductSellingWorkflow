using Newtonsoft.Json;
using ProductSellingWorkflow.NotificationHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace ProductSellingWorkflow
{
	public class MvcApplication : System.Web.HttpApplication
	{
		private static AutofacResolver resolver;
		private NotificationJob _notificationJob;

		protected void Application_Start()
		{

			resolver = new AutofacResolver();

			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			_notificationJob = new NotificationJob(resolver.Container);

			Task.Factory.StartNew(() => _notificationJob.Start());
		}

		protected void Application_End()
		{
			_notificationJob?.Stop();
		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{
			if (HttpContext.Current.User != null)
			{
				if (HttpContext.Current.User.Identity.IsAuthenticated)
				{
					if (HttpContext.Current.User.Identity is FormsIdentity)
					{
						var identity = (FormsIdentity)HttpContext.Current.User.Identity;
						var roles = JsonConvert.DeserializeObject<IEnumerable<string>>(identity.Ticket.UserData);
						HttpContext.Current.User = new GenericPrincipal(identity, roles.ToArray());
					}
				}
			}
		}
	}
}
