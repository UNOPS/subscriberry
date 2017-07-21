namespace Subscriberry.core.Model
{
    public class SubscriptionRole
    {
        public SubscriptionRole(int subscriptionId, int roleId)
        {
            this.SubscriptionId = subscriptionId;
            this.RoleId = roleId;
        }

        protected SubscriptionRole()
        {
        }

        public int RoleId { get; protected set; }
        public virtual Subscription Subscription { get; protected set; }
        public int SubscriptionId { get; protected set; }
    }
}