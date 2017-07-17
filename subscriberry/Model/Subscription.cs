namespace Subscriberry.core.Model
{
	public class Subscription
	{
		public Subscription(int id, string name, int? groupId)
		{
			Id = id;
			Name = name;
			GroupId = groupId;
		}

		public Subscription(int id, int? groupId)
		{
			Id = id;
			GroupId = groupId;
		}

		public Subscription()
		{
		}

		public virtual SubscriptionGroup Group { get; set; }
		public int? GroupId { get; set; }
		public int Id { get; set; }
		public string Name { get; set; }
	}
}