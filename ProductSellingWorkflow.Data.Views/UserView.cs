using System.Collections.Generic;

namespace ProductSellingWorkflow.Data.Views
{
	public class UserView
	{
		public UserView()
		{
			WatchList = new List<WatchListView>();
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public IEnumerable<string> Roles { get; set; }
		public IEnumerable<WatchListView> WatchList { get; set; }
	}
}
