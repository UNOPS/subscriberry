using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Subscriberry.EntityFramework.DataAccess
{
	internal abstract class DbEntityConfiguration<TEntity> where TEntity : class
	{
		public abstract void Configure(EntityTypeBuilder<TEntity> entity);
	}
}