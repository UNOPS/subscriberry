namespace Subscriberry.EntityFramework.DataAccess.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Subscriberry.core.Model;

    internal class UserSubscriptionMap : DbEntityConfiguration<UserSubscription>
    {
        private readonly string schema;

        public UserSubscriptionMap(string schema)
        {
            this.schema = schema;
        }

        public override void Configure(EntityTypeBuilder<UserSubscription> entity)
        {
            entity.ToTable("UserSubscription", this.schema);
            entity.HasKey(t => new { t.UserId, t.SubscriptionId });
        }
    }
}