using ProductSellingWorkflow.Data.Views;

namespace ProductSellingWorkflow.Service.Abstractions
{
	public interface IAuthenticationService
	{
		void SignIn(UserView user, bool createPersistentCookie);
		void SignOut();
		UserView CurrentUser { get; }
	}
}
