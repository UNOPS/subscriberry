namespace Subscriberry.EntityFramework.DataAccess.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Subscriberry.core.Model;

    internal class SubscriptionRoleMap : DbEntityConfiguration<SubscriptionRole>
    {
        private readonly string schema;

        public SubscriptionRoleMap(string schema)
        {
            this.schema = schema;
        }

        public override void Configure(EntityTypeBuilder<SubscriptionRole> entity)
        {
            entity.ToTable("SubscriptionRole", this.schema);
            entity.HasKey(t => new { t.RoleId, t.SubscriptionId });
        }
    }
}