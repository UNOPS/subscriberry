using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriberry.EntityFramework.Tests
{
	using System.IO;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;

	public class DatabaseFixture
	{
		private readonly DbContextOptions options;

		public DatabaseFixture()
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			this.options = new DbContextOptionsBuilder().UseSqlServer(config.GetConnectionString("filer")).Options;
		}

		public DataContext CreateDataContext()
		{
			return new DataContext(this.options);
		}
	}
}
