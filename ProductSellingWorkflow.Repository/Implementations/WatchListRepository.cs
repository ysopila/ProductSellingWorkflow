using ProductSellingWorkflow.Data;
using ProductSellingWorkflow.DataModel;
using ProductSellingWorkflow.Repository.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace ProductSellingWorkflow.Repository.Implementations
{
	internal class WatchListRepository : Repository<WatchList>, IWatchListRepository
	{
		public WatchListRepository(IDbContext context) : base(context) { }

		public IEnumerable<int> GetWatchList(int userId) => Query.Where(x => x.UserId == userId).Select(x => x.ProductId).ToList();
	}
}
