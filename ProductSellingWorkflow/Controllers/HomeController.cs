using System.Web.Mvc;

namespace ProductSellingWorkflow.Controllers
{
	public class HomeController : MvcBaseController
	{
		public ActionResult Index()
		{
			return RedirectToAction("Index", "Product");
		}
	}
}