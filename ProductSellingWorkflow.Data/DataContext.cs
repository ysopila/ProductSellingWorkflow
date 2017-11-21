using ProductSellingWorkflow.Data.Configuration;
using System.Data.Entity;

namespace ProductSellingWorkflow.Data
{
	public class DataContext : DbContext, IDbContext
	{
		public DataContext()
			: base("ProductSellingWorkflow")
		{
			Configuration.LazyLoadingEnabled = false;
		}

		static DataContext()
		{
			Database.SetInitializer(new DataContextInitializer());
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Configurations.Add(new ProductConfiguration());
			modelBuilder.Configurations.Add(new ProductLogConfiguration());
			modelBuilder.Configurations.Add(new ProductTagConfiguration());
			modelBuilder.Configurations.Add(new TagConfiguration());
		}
	}

	public class DataContextInitializer : MigrateDatabaseToLatestVersion<DataContext, Migrations.Configuration>
	{
	}
}
