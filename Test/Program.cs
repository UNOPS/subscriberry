using System;
using Subscriberry.core;
using Subscriberry.EntityFramework;

namespace Test
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			string connectionString =
				@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=subscriberry;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
			var repository = new SubscriptionRepository(connectionString);
			var service = new SubscriptionService(repository);

			service.EnsureSubscriptionInGroup(1, "test");


			var x = service.GetAllSubscriptions();

			Console.ReadLine();

		}
	}
}