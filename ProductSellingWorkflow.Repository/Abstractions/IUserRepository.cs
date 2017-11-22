using ProductSellingWorkflow.Data.Views;
using ProductSellingWorkflow.DataModel;

namespace ProductSellingWorkflow.Repository.Abstractions
{
	public interface IUserRepository : IRepository<User>
	{
		UserView GetOne(string email);
	}
}
