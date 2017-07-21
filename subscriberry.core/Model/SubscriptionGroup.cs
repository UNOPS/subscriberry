namespace Subscriberry.core.Model
{
    using System.Collections.Generic;

    public class SubscriptionGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<Subscription> Subscriptions { get; set; }
    }
}