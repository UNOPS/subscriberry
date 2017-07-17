namespace Subscriberry.core.Model
{
	public class SubscriptionRole
	{
		public SubscriptionRole(int subscriptionId, int roleId)
		{
			SubscriptionId = subscriptionId;
			RoleId = roleId;
		}

		public SubscriptionRole()
		{
			
		}
		public int RoleId { get; set; }
		public virtual Subscription Subscription { get; set; }
		public int SubscriptionId { get; set; }
	}
}