using System;
using System.Collections.Generic;
using System.Linq;
using Subscriberry.core.Model;

namespace Subscriberry.core
{
	public class SubscriptionService : IDisposable
	{
		private readonly ISubscriptionRepository _repository;

		public static SubscriptionService Default;

		public SubscriptionService(ISubscriptionRepository repository)
		{
			_repository = repository;
		}


		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}


		/// <summary>
		/// Manual subscriptions for roles.
		/// </summary>
		/// <param name="subscription">User Identity</param>
		/// <param name="roles">User roles to compare with the intended subscriptions set roles</param>
		public void AddSubscriptionRoles(int subscription, int[] roles)
		{
			_repository.AddSubscriptionToRoles(subscription, roles);
		}

		/// <summary>
		/// Allow System's Administrator to change user's subscriptions.
		/// </summary>
		/// <param name="userId">User Identity</param>
		/// <param name="subscriptions">Array of actions to subscribe</param>
		public void ChangeUserSubscriptions(string userId, IEnumerable<int> subscriptions)
		{
			var userSubscriptions = _repository.GetUserSubscriptions(userId);
			_repository.UnSubscribeRange(userId, userSubscriptions);
			_repository.SubscribeRange(userId, subscriptions);
		}


		/// <summary>
		/// Get the group if it's exists in database otherwise create a new one and return the new object.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public SubscriptionGroup EnsureGroup(string name)
		{
			return _repository.EnsureGroup(name);
		}


		/// <summary>
		/// Subscribe with ensuring that subscriptions are relevant to user's permissions
		/// </summary>
		/// <param name="userId">User Identity</param>
		/// <param name="subscriptions">Array of actions to subscribe</param>
		/// <param name="usersRoles">User roles to compare with the intended subscriptions set roles</param>
		public void EnsureSubscribe(string userId, IEnumerable<int> subscriptions, IEnumerable<int> usersRoles)
		{
			_repository.EnsureSubscribe(userId, subscriptions, usersRoles);
		}


		/// <summary>
		/// Ensure that subscription in group 
		/// </summary>
		/// <param name="subscriptionId"></param>
		/// <param name="group"></param>
		public void EnsureSubscriptionInGroup(int subscriptionId, string group)
		{
			_repository.EnsureSubscriptionInGroup(subscriptionId, group);
		}


		/// <summary>
		/// Ensure that subscription event is registered 
		/// </summary>
		/// <param name="subscriptionId"></param>
		/// <param name="subscriptionEvent"></param>
		/// <param name="group"></param>
		public void EnsureSubscriptionData(int subscriptionId, string subscriptionEvent, string group)
		{
			_repository.EnsureSubscriptionData(subscriptionId, subscriptionEvent, group);
		}

		/// <summary>
		/// Ensure unsubscribe list of subscriptions among user's roles.
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="subscriptions"></param>
		/// <param name="usersRoles"></param>
		public void EnsureUnSubscribe(string userId, IEnumerable<int> subscriptions, IEnumerable<int> usersRoles)
		{
			_repository.EnsureUnSubscribe(userId, subscriptions, usersRoles);
		}

		/// <summary>
		/// Get all subscriptions.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Subscription> GetAllSubscriptions()
		{
			return _repository.GetAllSubscriptions();
		}


		/// <summary>
		/// Get all subscriptions for set of roles.
		/// </summary>
		/// <param name="roles">Set of roles</param>
		/// <returns></returns>
		public IEnumerable<Subscription> GetRolesSubscriptions(IEnumerable<int> roles)
		{
			return _repository.GetRolesSubscriptions(roles);
		}


		/// <summary>
		/// Get all subscriptions for specific role.
		/// </summary>
		/// <param name="roleId"></param>
		/// <returns></returns>
		public IEnumerable<Subscription> GetRoleSubscriptions(int roleId)
		{
			return _repository.GetRoleSubscriptions(roleId);
		}


		/// <summary>
		/// Get all subscribers for a specific action.
		/// </summary>
		/// <param name="subscription"></param>
		/// <returns></returns>
		public string[] GetSubscribersFor(int subscription)
		{
			return _repository.GetSubscribersFor(subscription);
		}


		/// <summary>
		/// Get all subscriptions the user can subscribe to and if user already subscribed regards his roles grouped by subscription group,
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="roles"></param>
		/// <returns></returns>
		public IEnumerable<IGrouping<SubscriptionGroup, UserSubscribed>> GetUserSubscriptions(string userId,
			IEnumerable<int> roles)
		{
			return _repository.GetSubscriptionsForUser(userId, roles);
		}


		/// <summary>
		/// Get all user subscriptions
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public IEnumerable<int> GetUserSubscriptions(string userId)
		{
			return _repository.GetUserSubscriptions(userId);
		}


		/// <summary>
		/// Remove all user subscriptions linked to specific roles
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="roles"></param>
		/// <returns></returns>
		public int RemoveRolesSubscriptions(string userId, int[] roles)
		{
			return _repository.RemoveRolesSubscriptions(userId, roles);
		}


		/// <summary>
		/// Subscribe to specific action
		/// </summary>
		/// <param name="userId">User identity</param>
		/// <param name="subscriptionId">The action to subscribe</param>
		public void Subscribe(string userId, int subscriptionId)
		{
			_repository.Subscribe(userId, subscriptionId);
		}


		/// <summary>
		/// Subscribe to set of actions
		/// </summary>
		/// <param name="userId">User identity</param>
		/// <param name="subscriptions">Array of subscription actions </param>
		public void SubscribeRange(string userId, IEnumerable<int> subscriptions)
		{
			_repository.SubscribeRange(userId, subscriptions);
		}


		/// <summary>
		/// Unsubscribe specific action
		/// </summary>
		/// <param name="userId">User identity </param>
		/// <param name="subscriptionId">The action to unsubscribe.</param>
		public void UnSubscribe(string userId, int subscriptionId)
		{
			_repository.UnSubscribe(userId, subscriptionId);
		}


		/// <summary>
		/// Unsubscribe set of actions
		/// </summary>
		/// <param name="userId">User identity </param>
		/// <param name="subscriptions">Array of subscriptions to unsubscribe</param>
		public void UnSubscribeRange(string userId, IEnumerable<int> subscriptions)
		{
			_repository.UnSubscribeRange(userId, subscriptions);
		}

		/// <summary>
		/// Ensure that subscription is in roles 
		/// </summary>
		/// <param name="subscriptionId"></param>
		/// <param name="roles"></param>
		public void EnsureSubscriptionInRoles(int subscriptionId, int[] roles)
		{
			_repository.EnsureSubscriptionInRoles(subscriptionId,roles);
		}
	}
}