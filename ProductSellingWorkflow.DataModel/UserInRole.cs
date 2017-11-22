namespace ProductSellingWorkflow.DataModel
{
	public class UserInRole
	{
		public int UserId { get; set; }
		public User User { get; set; }

		public int RoleId { get; set; }
		public Role Role { get; set; }
	}
}
