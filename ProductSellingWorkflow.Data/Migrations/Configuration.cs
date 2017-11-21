namespace ProductSellingWorkflow.Data.Migrations
{
	using System.Data.Entity.Migrations;

	public sealed class Configuration : DbMigrationsConfiguration<DataContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(DataContext context)
		{
		 
		}
	}
}
