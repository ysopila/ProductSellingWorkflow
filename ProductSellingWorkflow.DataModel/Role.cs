using System.Collections.Generic;

namespace ProductSellingWorkflow.DataModel
{
	public class Role
	{
		public Role()
		{
			Users = new List<UserInRole>();
		}

		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<UserInRole> Users { get; set; }
	}
}
