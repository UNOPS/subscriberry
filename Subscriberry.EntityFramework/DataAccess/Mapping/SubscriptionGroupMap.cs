namespace Subscriberry.EntityFramework.DataAccess.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Subscriberry.core.Model;

    internal class SubscriptionGroupMap : DbEntityConfiguration<SubscriptionGroup>
    {
        private readonly string schema;

        public SubscriptionGroupMap(string schema)
        {
            this.schema = schema;
        }

        public override void Configure(EntityTypeBuilder<SubscriptionGroup> entity)
        {
            entity.ToTable("Group", this.schema);
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();
            entity.Property(t => t.Name).HasColumnName("Name").HasMaxLength(50).IsUnicode(false);
        }
    }
}