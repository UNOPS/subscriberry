using System.Collections.Generic;

namespace Subscriberry.core.Model
{
	public class SubscriptionGroup
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public virtual IEnumerable<Subscription> Subscriptions { get; set; }
	}
}