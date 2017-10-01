using Microsoft.EntityFrameworkCore;
using Subscriberry.EntityFramework.DataAccess;

namespace Subscriberry.EntityFramework
{
	/// <summary>
	/// Represents a single unit of work.
	/// </summary>
	public class DataContext
	{
		/// <summary>
		/// Instantiates a new instance of the DataContext class.
		/// </summary>
		public DataContext(DbContextOptions options, string schema = "dbo")
		{
			DbContext = new SubscriberryDbContext(options, schema);
		}

		// ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
		internal SubscriberryDbContext DbContext { get; private set; }

		/// <summary>
		/// Runs <see cref="RelationalDatabaseFacadeExtensions.Migrate"/> underlying <see cref="Microsoft.EntityFrameworkCore.DbContext"/>,
		/// to make sure database exists and all migrations are run.
		/// </summary>
		public void MigrateDatabase()
		{
			DbContext.Database.Migrate();
		}
	}
}