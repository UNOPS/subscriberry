namespace Subscriberry.core.Model
{
    public class Subscription
    {
        public Subscription(int id, string name, int? groupId)
        {
            this.Id = id;
            this.Name = name;
            this.GroupId = groupId;
        }

        public Subscription(int id, int? groupId)
        {
            this.Id = id;
            this.GroupId = groupId;
        }

        protected Subscription()
        {
        }

        public virtual SubscriptionGroup Group { get; protected set; }
        public int? GroupId { get; set; }
        public int Id { get; protected set; }
        public string Name { get; protected set; }
    }
}