using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Subscriberry.core.Model;

namespace Subscriberry.EntityFramework.DataAccess.Mapping
{
	internal class UserSubscriptionMap : DbEntityConfiguration<UserSubscription>
	{
		public override void Configure(EntityTypeBuilder<UserSubscription> entity)
		{
			entity.ToTable("UserSubscription");
			entity.HasKey(t => new {t.UserId, t.SubscriptionId});
		}
	}
}