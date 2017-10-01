using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Subscriberry.EntityFramework.Tests
{
	public class DatabaseFixture
	{
		private readonly DbContextOptions _options;

		public DatabaseFixture()
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			_options = new DbContextOptionsBuilder().UseSqlServer(config.GetConnectionString("subscriberry")).Options;
		}

		public DataContext CreateDataContext()
		{
			return new DataContext(_options);
		}
	}
}