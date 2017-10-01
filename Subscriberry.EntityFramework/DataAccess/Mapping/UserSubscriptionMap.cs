using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Subscriberry.core.Model;

namespace Subscriberry.EntityFramework.DataAccess.Mapping
{
	internal class UserSubscriptionMap : DbEntityConfiguration<UserSubscription>
	{
		private readonly string _schema;

		public UserSubscriptionMap(string schema)
		{
			_schema = schema;
		}

		public override void Configure(EntityTypeBuilder<UserSubscription> entity)
		{
			entity.ToTable("UserSubscription", _schema);
			entity.HasKey(t => new {t.UserId, t.SubscriptionId});
		}
	}
}