using System.Linq;
using Subscriberry.core;
using Subscriberry.core.Model;
using Xunit;

namespace Subscriberry.EntityFramework.Tests
{
	[Collection(nameof(DatabaseCollectionFixture))]
	public class DbTest
	{
		public DbTest(DatabaseFixture dbFixture)
		{
			_subscriptionRepository = new SubscriptionRepository(dbFixture.CreateDataContext());
		}

		private readonly ISubscriptionRepository _subscriptionRepository;

		[Fact]
		public void AssignSubscriptionToRole()
		{
			var service = new SubscriptionService(_subscriptionRepository);
			service.AddSubscriptionRoles(1, new[] {1, 2});
			var subscription = service.GetRoleSubscriptions(1);

			Assert.True(subscription.Any());
		}

		[Fact]
		public void CanAttachToContext()
		{
			var service = new SubscriptionService(_subscriptionRepository);
			service.EnsureSubscriptionData(1, "Test", "Group1");
			var subsc = service.GetAllSubscriptions();

			Assert.True(subsc.Any());
		}

		[Fact]
		public void CanUserSubscribe()
		{
			var service = new SubscriptionService(_subscriptionRepository);
			service.EnsureSubscribe("1", new[] {1}, new[] {1});
			var subscription = service.GetUserSubscriptions("1");

			Assert.True(subscription.Any());
		}

		[Fact]
		public void CreateSubscription()
		{
			var subscription = new Subscription("New user", null);
			_subscriptionRepository.AddSubscription(subscription);

			Assert.NotEqual(0, subscription.Id);
		}
	}
}