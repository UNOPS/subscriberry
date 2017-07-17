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
		public DataContext(DbContextOptions options)
		{
			DbContext = new SubscriberryDbContext(options);
		}

		// ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
		internal SubscriberryDbContext DbContext { get; private set; }
	}
}