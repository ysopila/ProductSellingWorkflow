using ProductSellingWorkflow.Data.Views;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Service.Abstractions
{
	public interface IUserService
	{
		UserView GetByEmail(string email);
		IEnumerable<WatchListView> GetWatchList(int userId);
		bool Validate(string email, string password, out UserView user);
		bool Create(string name, string email, string password, List<string> roles, out UserView user);
	}
}
