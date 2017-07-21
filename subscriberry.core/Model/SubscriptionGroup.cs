namespace Subscriberry.core.Model
{
    using System.Collections.Generic;

    public class SubscriptionGroup
    {
        public SubscriptionGroup(string name, params Subscription[] subscriptions)
        {
            this.Name = name;

            // ReSharper disable once VirtualMemberCallInConstructor
            this.Subscriptions = subscriptions;
        }

        protected SubscriptionGroup()
        {
        }

        public int Id { get; protected set; }
        public string Name { get; protected set; }

        public virtual IEnumerable<Subscription> Subscriptions { get; protected set; }
    }
}