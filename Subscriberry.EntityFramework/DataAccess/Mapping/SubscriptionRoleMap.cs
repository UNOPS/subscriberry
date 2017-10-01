using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Subscriberry.core.Model;

namespace Subscriberry.EntityFramework.DataAccess.Mapping
{
	internal class SubscriptionRoleMap : DbEntityConfiguration<SubscriptionRole>
	{
		private readonly string _schema;

		public SubscriptionRoleMap(string schema)
		{
			_schema = schema;
		}

		public override void Configure(EntityTypeBuilder<SubscriptionRole> entity)
		{
			entity.ToTable("SubscriptionRole", _schema);
			entity.HasKey(t => new {t.RoleId, t.SubscriptionId});
		}
	}
}