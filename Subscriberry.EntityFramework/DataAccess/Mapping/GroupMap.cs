using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Subscriberry.core.Model;

namespace Subscriberry.EntityFramework.DataAccess.Mapping
{
	internal class SubscriptionGroupMap : DbEntityConfiguration<SubscriptionGroup>
	{
		public override void Configure(EntityTypeBuilder<SubscriptionGroup> entity)
		{
			entity.ToTable("Group");
			entity.HasKey(t => t.Id);
			entity.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();
			entity.Property(t => t.Name).HasColumnName("Name").HasMaxLength(50).IsUnicode(false);
		}
	}
}