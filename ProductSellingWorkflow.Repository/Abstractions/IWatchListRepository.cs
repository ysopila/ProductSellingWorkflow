using ProductSellingWorkflow.DataModel;
using System.Collections.Generic;

namespace ProductSellingWorkflow.Repository.Abstractions
{
	public interface IWatchListRepository : IRepository<WatchList>
	{
		IEnumerable<int> GetWatchList(int userId);
	}
}
