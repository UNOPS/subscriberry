namespace Subscriberry.core.Model
{
    public class UserSubscribed
    {
        protected UserSubscribed()
        {
        }

        public UserSubscribed(bool subscribed, Subscription subscription)
        {
            this.Subscribed = subscribed;
            this.Subscription = subscription;
        }

        public bool Subscribed { get; protected set; }
        public Subscription Subscription { get; protected set; }
    }
}