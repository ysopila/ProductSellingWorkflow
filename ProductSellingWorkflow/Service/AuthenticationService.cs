using Newtonsoft.Json;
using ProductSellingWorkflow.Data.Views;
using ProductSellingWorkflow.Service.Abstractions;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace ProductSellingWorkflow.Service
{
	public class AuthenticationService : IAuthenticationService
	{
		private IUserService _userService;

		public AuthenticationService(IUserService userService)
		{
			_userService = userService;
		}

		public void SignIn(UserView user, bool createPersistentCookie)
		{
			var authTicket = new FormsAuthenticationTicket(1,
				user.Email,
				DateTime.UtcNow,
				DateTime.UtcNow.AddMinutes(30),
				createPersistentCookie,
				JsonConvert.SerializeObject(user));

			HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket)));
			HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(user.Email, "Forms"), null);
		}

		public void SignOut()
		{
			FormsAuthentication.SignOut();
		}

		public UserView CurrentUser
		{
			get
			{
				if (HttpContext.Current != null && HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
				{
					var cached = HttpContext.Current.Items["CURRENT_USER"] as UserView;
					if (cached == null)
					{
						cached = _userService.GetByEmail(HttpContext.Current.User.Identity.Name);
						HttpContext.Current.Items["CURRENT_USER"] = cached;
					}
					return cached;
				}
				return null;
			}
		}
	}
}