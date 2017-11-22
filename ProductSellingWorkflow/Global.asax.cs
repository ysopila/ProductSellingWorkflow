using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
		protected void Application_Start()
		{

			resolver = new AutofacResolver();

			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
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
