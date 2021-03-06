﻿using ProductSellingWorkflow.DataModel;
using System.Data.Entity.ModelConfiguration;

namespace ProductSellingWorkflow.Data.Configuration
{
	class UserConfiguration : EntityTypeConfiguration<User>
	{
		public UserConfiguration()
		{
			HasKey(x => x.Id);

			HasMany(x => x.Roles)
				.WithRequired(x => x.User)
				.HasForeignKey(x => x.UserId);

			HasMany(x => x.Products)
				.WithRequired(x => x.CreatedBy)
				.HasForeignKey(x => x.CreatedById)
				.WillCascadeOnDelete(false);

			HasMany(x => x.ProductLogs)
				.WithRequired(x => x.CreatedBy)
				.HasForeignKey(x => x.CreatedById);

			HasMany(x => x.WatchList)
				.WithRequired(x => x.User)
				.HasForeignKey(x => x.UserId);

			HasMany(x => x.NotificationSettings)
				.WithRequired(x => x.User)
				.HasForeignKey(x => x.UserId);
		}
	}
}