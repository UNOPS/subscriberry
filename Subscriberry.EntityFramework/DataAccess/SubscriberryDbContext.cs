using Microsoft.EntityFrameworkCore;
using Subscriberry.core.Model;
using Subscriberry.EntityFramework.DataAccess.Mapping;
using Subscriberry.EntityFramework.Extenions;

namespace Subscriberry.EntityFramework.DataAccess
{
	internal class SubscriberryDbContext : DbContext
	{
		public SubscriberryDbContext(DbContextOptions options) : base(options)
		{
		}


		public virtual DbSet<Subscription> Subscriptions { get; set; }
		public virtual DbSet<SubscriptionGroup> Groups { get; set; }
		public virtual DbSet<UserSubscription> UserSubscriptions { get; set; }
		public virtual DbSet<SubscriptionRole> SubscriptionRoles { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.HasDefaultSchema("sbsc");
			builder.AddConfiguration(new SubscriptionMap());
			builder.AddConfiguration(new SubscriptionGroupMap());
			builder.AddConfiguration(new UserSubscriptionMap());
			builder.AddConfiguration(new SubscriptionRoleMap());
		}
	}
}