namespace Subscriberry.EntityFramework.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using Subscriberry.core.Model;
    using Subscriberry.EntityFramework.DataAccess.Mapping;
    using Subscriberry.EntityFramework.Extenions;

    internal class SubscriberryDbContext : DbContext
    {
        private const string DefaultConnectionString =
            "Server=(localdb)\\mssqllocaldb;Database=subscriberry;Trusted_Connection=True;MultipleActiveResultSets=true";

        private readonly string schema;

        public SubscriberryDbContext()
            : this(new DbContextOptionsBuilder().UseSqlServer(DefaultConnectionString).Options, "dbo")
        {
        }

        public SubscriberryDbContext(DbContextOptions options, string schema) : base(options)
        {
            this.schema = schema;
        }

        public virtual DbSet<SubscriptionGroup> SubscriptionGroups { get; set; }
        public virtual DbSet<SubscriptionRole> SubscriptionRoles { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<UserSubscription> UserSubscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema(this.schema);

            builder.AddConfiguration(new SubscriptionMap(this.schema));
            builder.AddConfiguration(new SubscriptionGroupMap(this.schema));
            builder.AddConfiguration(new UserSubscriptionMap(this.schema));
            builder.AddConfiguration(new SubscriptionRoleMap(this.schema));
        }
    }
}