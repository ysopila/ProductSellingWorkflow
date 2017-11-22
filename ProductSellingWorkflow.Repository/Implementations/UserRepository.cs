using ProductSellingWorkflow.Data;
using ProductSellingWorkflow.Data.Views;
using ProductSellingWorkflow.DataModel;
using ProductSellingWorkflow.Repository.Abstractions;
using System.Linq;

namespace ProductSellingWorkflow.Repository.Implementations
{
	internal class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(IDbContext context) : base(context) { }

		public UserView GetOne(string email) => Query
			.Where(x => x.Email == email)
			.Select(x => new UserView
			{
				Id = x.Id,
				Name = x.Name,
				Email = x.Email,
				Password = x.Password
			})
			.FirstOrDefault();


	}
}
