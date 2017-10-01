using System.Collections.Generic;

namespace Subscriberry.core.Model
{
	public class SubscriptionGroup
	{
		public SubscriptionGroup(string name, params Subscription[] subscriptions)
		{
			Name = name;

			// ReSharper disable once VirtualMemberCallInConstructor
			Subscriptions = subscriptions;
		}

		protected SubscriptionGroup()
		{
		}

		public int Id { get; protected set; }
		public string Name { get; protected set; }

		public virtual IEnumerable<Subscription> Subscriptions { get; protected set; }
	}
}