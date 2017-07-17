namespace Subscriberry.core.Model
{
	public class UserSubscription
	{
		public UserSubscription()
		{
		}

		public UserSubscription(int subscriptionId, string userId)
		{
			SubscriptionId = subscriptionId;
			UserId = userId;
		}

		public int SubscriptionId { get; set; }
		public string UserId { get; set; }

		public virtual Subscription Subscription { get; set; }
	}
}