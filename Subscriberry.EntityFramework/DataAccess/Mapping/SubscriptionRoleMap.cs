using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Subscriberry.core.Model;

namespace Subscriberry.EntityFramework.DataAccess.Mapping
{
	internal class SubscriptionRoleMap : DbEntityConfiguration<SubscriptionRole>
	{
		public override void Configure(EntityTypeBuilder<SubscriptionRole> entity)
		{
			entity.ToTable("SubscriptionRole");
			entity.HasKey(t => new {t.RoleId, t.SubscriptionId});
		}
	}
}