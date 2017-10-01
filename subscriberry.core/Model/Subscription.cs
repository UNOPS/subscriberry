namespace Subscriberry.core.Model
{
	public class Subscription
	{
		public Subscription(string name, int? groupId)
		{
			Name = name;
			GroupId = groupId;
		}

		public Subscription(int groupId)
		{
			GroupId = groupId;
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