using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

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
	}
}
