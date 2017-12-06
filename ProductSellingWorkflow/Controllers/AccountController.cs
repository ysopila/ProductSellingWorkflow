using ProductSellingWorkflow.Common.Enums;
using ProductSellingWorkflow.Data.Views;
using ProductSellingWorkflow.Models;
using ProductSellingWorkflow.Service.Abstractions;
using System.Linq;
using System.Web.Mvc;

namespace ProductSellingWorkflow.Controllers
{
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
		[AllowAnonymous]
		public ActionResult Login()
		{
			if (User.Identity.IsAuthenticated)
				return RedirectToAction("Index", "Product");

			return View(new UserLoginViewModel());
		}

		[HttpPost]
		[AllowAnonymous]
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
		[AllowAnonymous]
		public ActionResult Signup()
		{
			if (User.Identity.IsAuthenticated)
				return RedirectToAction("Index", "Product");

			return View(new UserCreateViewModel());
		}

		[HttpPost]
		[AllowAnonymous]
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

		[HttpGet]
		public ActionResult Settings()
		{
			var userId = _authService.CurrentUser.Id;
			var settings = _userService.GetNotificationSettings(userId);
			var types = _userService.GetNotificationTypes();

			var model = new NotificationSettingsViewModel
			{
				NotificationTypes = types.Select(x => new NotificationTypeViewModel
				{
					NotificationType = x,
					CurrentKind = settings.Where(s => s.NotificationTypeId == x.Id)
						.Select(s => s.NotificationKinds)?.FirstOrDefault() ?? NotificationKind.None
				})
				.ToList()
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Settings(NotificationSettingsViewModel model)
		{
			if (ModelState.IsValid)
			{
				var userId = _authService.CurrentUser.Id;
				var selectedNotifications = model.NotificationTypes
					.ToDictionary(x => x.NotificationType.Id, x => x.SelectedKinds.Aggregate(NotificationKind.None, (c, n) => c |= n));

				_userService.UpdateNotificationSettings(userId, selectedNotifications);

				return RedirectToAction("Settings");
			}

			return View(model);
		}

	}
}