using ProductSellingWorkflow.Data.Views;

namespace ProductSellingWorkflow.Service.Abstractions
{
	public interface IUserService
	{
		UserView GetByEmail(string email);
		bool Validate(string email, string password, out UserView user);
		bool Create(string name, string email, string password, out UserView user);
	}
}
