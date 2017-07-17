using System;
using System.Linq;
using Subscriberry.core;
using Xunit;

namespace Subscriberry.EntityFramework.Tests
{
	[Collection(nameof(DatabaseCollectionFixture))]
	public class DbTest
	{
		public DbTest(DatabaseFixture dbFixture)
		{
			this.dbFixture = dbFixture;
		}

		private readonly DatabaseFixture dbFixture;

		[Fact]
		public void CanAttachToContext()
		{
			var repository = new SubscriptionRepository(this.dbFixture.CreateDataContext());
			var service = new SubscriptionService(repository);
			service.EnsureSubscriptionData(1, "Test", "Group1");

			var subsc = service.GetAllSubscriptions();

			Assert.True(subsc.Any());
		}

		[Fact]
		public void AssignSubscriptionToRole()
		{
			var repository = new SubscriptionRepository(this.dbFixture.CreateDataContext());
			var service = new SubscriptionService(repository);
			service.AddSubscriptionRoles(1, new[] { 1, 2 });
			var subscription = service.GetRoleSubscriptions(1);
			Assert.True(subscription.Any());
		}

		[Fact]
		public void CanUserSubscribe()
		{
			var repository = new SubscriptionRepository(this.dbFixture.CreateDataContext());
			var service = new SubscriptionService(repository);
			service.EnsureSubscribe("1", new[] { 1 }, new[] { 1 });
			var subscription = service.GetUserSubscriptions("1");

			Assert.True(subscription.Any());
		}


	}
}
