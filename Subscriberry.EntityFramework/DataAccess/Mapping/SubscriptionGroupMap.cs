using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Subscriberry.core.Model;

namespace Subscriberry.EntityFramework.DataAccess.Mapping
{
	internal class SubscriptionGroupMap : DbEntityConfiguration<SubscriptionGroup>
	{
		private readonly string _schema;

		public SubscriptionGroupMap(string schema)
		{
			_schema = schema;
		}

		public override void Configure(EntityTypeBuilder<SubscriptionGroup> entity)
		{
			entity.ToTable("Group", _schema);
			entity.HasKey(t => t.Id);
			entity.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();
			entity.Property(t => t.Name).HasColumnName("Name").HasMaxLength(50).IsUnicode(false);
		}
	}
}