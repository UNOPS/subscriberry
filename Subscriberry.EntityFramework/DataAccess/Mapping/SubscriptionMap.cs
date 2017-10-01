using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Subscriberry.core.Model;

namespace Subscriberry.EntityFramework.DataAccess.Mapping
{
	internal class SubscriptionMap : DbEntityConfiguration<Subscription>
	{
		private readonly string schema;

		public SubscriptionMap(string schema)
		{
			this.schema = schema;
		}

		public override void Configure(EntityTypeBuilder<Subscription> entity)
		{
			entity.ToTable("Subscription", schema);
			entity.HasKey(t => t.Id);
			entity.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();
			entity.Property(t => t.Name).HasColumnName("Name").HasMaxLength(100);
			entity.HasOne(t => t.Group).WithMany(t => t.Subscriptions).HasForeignKey(t => t.GroupId);
		}
	}
}