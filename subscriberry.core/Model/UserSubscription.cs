namespace Subscriberry.core.Model
{
    public class UserSubscription
    {
        protected UserSubscription()
        {
        }

        public UserSubscription(int subscriptionId, string userId)
        {
            this.SubscriptionId = subscriptionId;
            this.UserId = userId;
        }

        public virtual Subscription Subscription { get; protected set; }

        public int SubscriptionId { get; protected set; }
        public string UserId { get; protected set; }
    }
}