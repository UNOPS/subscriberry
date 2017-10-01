using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Subscriberry.core;
using Subscriberry.core.Model;
using Subscriberry.EntityFramework.DataAccess;
using Subscriberry.EntityFramework.Extensions;

namespace Subscriberry.EntityFramework
{
	public class SubscriptionRepository : ISubscriptionRepository
	{
		private readonly SubscriberryDbContext _dbContext;

		public SubscriptionRepository(string connectionString, string schema)
		{
			var optionBuilder = new DbContextOptionsBuilder<SubscriberryDbContext>();
			optionBuilder.UseSqlServer(connectionString);
			_dbContext = new SubscriberryDbContext(optionBuilder.Options, schema);
		}

		public SubscriptionRepository(DataContext dataContext)
		{
			_dbContext = dataContext.DbContext;
		}

		/// <summary>
		/// Add new subscription
		/// </summary>
		/// <param name="subscription"></param>
		/// <returns></returns>
		public int AddSubscription(Subscription subscription)
		{
			_dbContext.Subscriptions.Add(subscription);
			return _dbContext.SaveChanges();
		}

		/// <summary>
		/// Add new subscription group
		/// </summary>
		/// <param name="group"></param>
		/// <returns></returns>
		public int AddSubscriptionGroup(SubscriptionGroup group)
		{
			_dbContext.SubscriptionGroups.Add(group);
			return _dbContext.SaveChanges();
		}

		/// <summary>
		/// Link subscription to specific role
		/// </summary>
		/// <param name="subscriptionRole"></param>
		/// <returns></returns>
		public int AddSubscriptionToRole(SubscriptionRole subscriptionRole)
		{
			_dbContext.SubscriptionRoles.Add(subscriptionRole);
			return _dbContext.SaveChanges();
		}

		/// <summary>
		/// Manual subscriptions for roles.
		/// </summary>
		/// <param name="subscriptionId">User Identity</param>
		/// <param name="roles">User roles to compare with the intended subscriptions set roles</param>
		public int AddSubscriptionToRoles(int subscriptionId, int[] roles)
		{
			_dbContext.SubscriptionRoles.AddRange(roles.Select(role => new SubscriptionRole(subscriptionId, role)));
			return _dbContext.SaveChanges();
		}

		/// <summary>
		/// Get all subscriptions.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Subscription> GetAllSubscriptions()
		{
			return _dbContext.Subscriptions.ToList();
		}

		/// <summary>
		/// Get all subscriptions for set of roles.
		/// </summary>
		/// <param name="roles">Set of roles</param>
		/// <returns></returns>
		public IEnumerable<Subscription> GetRolesSubscriptions(IEnumerable<int> roles)
		{
			return _dbContext.SubscriptionRoles
				.Where(s => roles.Contains(s.RoleId))
				.Include(t => t.Subscription.Group)
				.DistinctBy(t => t.SubscriptionId)
				.Select(t => t.Subscription).ToList();
		}

		/// <summary>
		/// Get all subscriptions for specific role.
		/// </summary>
		/// <param name="roleId"></param>
		/// <returns></returns>
		public IEnumerable<Subscription> GetRoleSubscriptions(int roleId)
		{
			return _dbContext.SubscriptionRoles.Where(s => s.RoleId == roleId).Select(s => s.Subscription);
		}

		/// <summary>
		/// Get all subscribers for a specific action.
		/// </summary>
		/// <param name="subscription"></param>
		/// <returns></returns>
		public string[] GetSubscribersFor(int subscription)
		{
			return _dbContext.UserSubscriptions.Where(s => s.SubscriptionId == subscription).Select(s => s.UserId).ToArray();
		}

		/// <summary>
		/// Get all subscriptions the user can subscribe to and if user already subscribed regards his roles grouped by subscription group,
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="roles"></param>
		/// <returns></returns>
		public IEnumerable<IGrouping<SubscriptionGroup, UserSubscribed>> GetSubscriptionsForUser(string userId,
			IEnumerable<int> roles)
		{
			var result = from s in _dbContext.Subscriptions
				join sr in _dbContext.SubscriptionRoles on s.Id equals sr.SubscriptionId
				join us in _dbContext.UserSubscriptions on new {sub = s.Id, uId = userId} equals new
				{
					sub = us.SubscriptionId,
					uId = us.UserId
				}
				into uSubscriptions
				from data in uSubscriptions.DefaultIfEmpty()
				where roles.Contains(sr.RoleId)
				select new UserSubscribed(uSubscriptions.Any(), s);

			var temp = result.ToList();
			return temp.DistinctBy(t => t.Subscription.Id).GroupBy(t => t.Subscription.Group);
		}

		/// <summary>
		/// Get all user subscriptions
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public IEnumerable<int> GetUserSubscriptions(string userId)
		{
			return _dbContext.UserSubscriptions.Where(s => s.UserId == userId).Select(s => s.SubscriptionId).ToArray();
		}

		/// <summary>
		/// Remove all user subscriptions linked to specific roles
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="roles"></param>
		/// <returns></returns>
		public int RemoveRolesSubscriptions(string userId, int[] roles)
		{
			var subscriptions = from roleSubscriptions in _dbContext.SubscriptionRoles
				join userSubscription in _dbContext.UserSubscriptions
				on roleSubscriptions.SubscriptionId equals userSubscription.SubscriptionId
				where roles.Contains(roleSubscriptions.RoleId) && userSubscription.UserId == userId
				select userSubscription;

			_dbContext.UserSubscriptions.RemoveRange(subscriptions);
			_dbContext.SaveChanges();

			return subscriptions.Count();
		}

		/// <summary>
		/// Subscribe to specific action
		/// </summary>
		/// <param name="userId">User identity</param>
		/// <param name="subscriptionId">The action to subscribe</param>
		public void Subscribe(string userId, int subscriptionId)
		{
			_dbContext.UserSubscriptions.Add(new UserSubscription(subscriptionId, userId));
			_dbContext.SaveChanges();
		}

		/// <summary>
		/// Subscribe to set of actions
		/// </summary>
		/// <param name="userId">User identity</param>
		/// <param name="subscriptions">Array of subscription actions </param>
		public void SubscribeRange(string userId, IEnumerable<int> subscriptions)
		{
			_dbContext.UserSubscriptions.AddRange(subscriptions.Select(s => new UserSubscription(s, userId)));
			_dbContext.SaveChanges();
		}

		/// <summary>
		/// Unsubscribe specific action
		/// </summary>
		/// <param name="userId">User identity </param>
		/// <param name="subscriptionId">The action to unsubscribe.</param>
		public void UnSubscribe(string userId, int subscriptionId)
		{
			var subscription =
				_dbContext.UserSubscriptions.FirstOrDefault(t => t.SubscriptionId == subscriptionId && t.UserId == userId);
			if (subscription == null)
				return;

			_dbContext.UserSubscriptions.Remove(subscription);
			_dbContext.SaveChanges();
		}

		/// <summary>
		/// Unsubscribe set of actions
		/// </summary>
		/// <param name="userId">User identity </param>
		/// <param name="subscriptions">Array of subscriptions to unsubscribe</param>
		public void UnSubscribeRange(string userId, IEnumerable<int> subscriptions)
		{
			var deSubscriptions =
				_dbContext.UserSubscriptions.Where(s => s.UserId == userId && subscriptions.Contains(s.SubscriptionId));
			_dbContext.UserSubscriptions.RemoveRange(deSubscriptions);
			_dbContext.SaveChanges();
		}

		/// <summary>
		/// Get the group if it's exists in database otherwise create a new one and return the new object.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public SubscriptionGroup EnsureGroup(string name)
		{
			var data = _dbContext.SubscriptionGroups.FirstOrDefault(g => g.Name.Equals(name));
			if (data != null)
				return data;

			var newGroup = _dbContext.SubscriptionGroups.Add(new SubscriptionGroup(name));
			_dbContext.SaveChanges();
			return newGroup.Entity;
		}

		/// <summary>
		/// Subscribe with ensuring that subscriptions are relevant to user's permissions
		/// </summary>
		/// <param name="userId">User Identity</param>
		/// <param name="subscriptions">Array of actions to subscribe</param>
		/// <param name="usersRoles">User roles to compare with the intended subscriptions set roles</param>
		public void EnsureSubscribe(string userId, IEnumerable<int> subscriptions, IEnumerable<int> usersRoles)
		{
			var rolesSubscriptions = GetRolesSubscriptions(usersRoles).Select(s => s.Id).ToArray();

			// make sure that subscriptions are among user's permissions 
			subscriptions = subscriptions.Intersect(rolesSubscriptions).ToArray();

			var userSubscriptions = GetUserSubscriptions(userId);

			// make sure not to violate the unique key rule.
			subscriptions = subscriptions.Except(userSubscriptions).ToArray();

			SubscribeRange(userId, subscriptions);
		}

		/// <summary>
		/// Ensure that subscription in group 
		/// </summary>
		/// <param name="subscriptionId"></param>
		/// <param name="group"></param>
		public void EnsureSubscriptionInGroup(int subscriptionId, string group)
		{
			var subscriptionData = _dbContext.Subscriptions.Include(t => t.Group).FirstOrDefault(s => s.Id == subscriptionId);
			if (subscriptionData == null)
				throw new SubscriptionException("Subscription is not registered");

			var subscriptionGroup = subscriptionData.Group;
			if (group != subscriptionGroup.Name)
				subscriptionData.GroupId = EnsureGroup(@group).Id;

			_dbContext.SaveChanges();
		}

		/// <summary>
		/// Ensure that subscription is in roles 
		/// </summary>
		/// <param name="subscriptionId"></param>
		/// <param name="roles"></param>
		public void EnsureSubscriptionInRoles(int subscriptionId, int[] roles)
		{
			var subscriptionData = _dbContext.Subscriptions.Find(subscriptionId);
			if (subscriptionData == null)
				throw new SubscriptionException("Subscription is not registered");

			var subscriptionRoles = _dbContext.SubscriptionRoles.Where(s => s.SubscriptionId == subscriptionId).ToList();
			_dbContext.SubscriptionRoles.RemoveRange(subscriptionRoles);
			_dbContext.SubscriptionRoles.AddRange(roles.Select(role => new SubscriptionRole(subscriptionId, role)));
			_dbContext.SaveChanges();
		}

		/// <summary>
		/// Ensure that subscription event is registered 
		/// </summary>
		/// <param name="subscriptionId"></param>
		/// <param name="subscriptionEvent"></param>
		/// <param name="group"></param>
		public void EnsureSubscriptionData(int subscriptionId, string subscriptionEvent, string group)
		{
			var subscription = _dbContext.Subscriptions.Find(subscriptionId);
			if (subscription != null)
				return;

			var data = EnsureGroup(group);
			_dbContext.Subscriptions.Add(new Subscription(subscriptionEvent, data.Id));
			_dbContext.SaveChanges();
		}

		/// <summary>
		/// Ensure unsubscribe list of subscriptions among user's roles.
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="subscriptions"></param>
		/// <param name="usersRoles"></param>
		public void EnsureUnSubscribe(string userId, IEnumerable<int> subscriptions, IEnumerable<int> usersRoles)
		{
			var rolesSubscriptions = GetRolesSubscriptions(usersRoles).Select(s => s.Id).ToArray();
			// make sure that subscriptions are among user's permissions 
			subscriptions = subscriptions.Intersect(rolesSubscriptions).ToArray();

			var userSubscriptions = GetUserSubscriptions(userId);

			// make sure that subscriptions are already subscribed to.
			subscriptions = subscriptions.Intersect(userSubscriptions).ToArray();

			UnSubscribeRange(userId, subscriptions);
		}
	}
}