using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ProductSellingWorkflow.Startup), "Configuration")]
namespace ProductSellingWorkflow
{
	public static class Startup
	{
		public static void Configuration(IAppBuilder appBuilder)
		{
			// For more information on how to configure your application using OWIN startup, visit http://go.microsoft.com/fwlink/?LinkID=316888

			appBuilder.MapSignalR();
		}
	}

}