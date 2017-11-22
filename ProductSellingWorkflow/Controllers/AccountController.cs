using ProductSellingWorkflow.Data.Views;
using ProductSellingWorkflow.Models;
using ProductSellingWorkflow.Service.Abstractions;
using System.Linq;
using System.Web.Mvc;

namespace ProductSellingWorkflow.Controllers
{
	[AllowAnonymous]
	public class AccountController : MvcBaseController
	{
		private readonly IUserService _userService;
		private readonly IAuthenticationService _authService;

		public AccountController(IUserService userService, IAuthenticationService authService)
		{
			_userService = userService;
			_authService = authService;
		}

		[HttpGet]
		public ActionResult Login()
		{
			if (User.Identity.IsAuthenticated)
				return RedirectToAction("Index", "Product");

			return View(new UserLoginViewModel());
		}

		[HttpPost]
		public ActionResult Login(UserLoginViewModel model)
		{
			if (User.Identity.IsAuthenticated)
				return RedirectToAction("Index", "Product");

			if (ModelState.IsValid)
			{
				UserView user = null;

				if (_userService.Validate(model.Email, model.Password, out user))
				{
					_authService.SignIn(user, true);

					return RedirectToAction("Index", "Product");
				}

				ModelState.AddModelError("", "The e-mail or password provided is incorrect.");
			}

			return View(model);
		}

		[HttpGet]
		public ActionResult Logout()
		{
			_authService.SignOut();

			return RedirectToAction("Login", "Account");
		}

		[HttpGet]
		public ActionResult Signup()
		{
			if (User.Identity.IsAuthenticated)
				return RedirectToAction("Index", "Product");

			return View(new UserCreateViewModel());
		}

		[HttpPost]
		public ActionResult Signup(UserCreateViewModel model)
		{
			if (User.Identity.IsAuthenticated)
				return RedirectToAction("Index", "Product");

			if (ModelState.IsValid)
			{
				UserView user = null;

				var roles = model.AllRoles.Where(x => x.Selected).Select(x => x.Name).ToList();

				if (_userService.Create(model.Name, model.Email, model.Password, roles, out user))
				{
					_authService.SignIn(user, true);

					return RedirectToAction("Index", "Product");
				}

				ModelState.AddModelError("", "A user name for that e-mail address already exists. Please enter a different e-mail address.");
			}

			return View(model);
		}
	}
}