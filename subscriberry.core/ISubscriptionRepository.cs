namespace Subscriberry.core
{
    using System.Collections.Generic;
    using System.Linq;
    using Subscriberry.core.Model;

    public interface ISubscriptionRepository
    {
        /// <summary>
        /// Add new subscription
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        int AddSubscription(Subscription subscription);

        /// <summary>
        /// Add new subscription group
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        int AddSubscriptionGroup(SubscriptionGroup subscription);

        /// <summary>
        /// Assign subscription to role.
        /// </summary>
        /// <param name="subscriptionRole"></param>
        /// <returns></returns>
        int AddSubscriptionToRole(SubscriptionRole subscriptionRole);

        /// <summary>
        /// Assign subscription to set of roles
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        int AddSubscriptionToRoles(int subscriptionId, int[]roles);

        /// <summary>
        /// Get the group if it's exists in database otherwise create a new one and return the new object.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        SubscriptionGroup EnsureGroup(string name);

        /// <summary>
        /// Subscribe with ensuring that subscriptions are relevant to user's permissions
        /// </summary>
        /// <param name="userId">User Identity</param>
        /// <param name="subscriptions">Array of actions to subscribe</param>
        /// <param name="usersRoles">User roles to compare with the intended subscriptions set roles</param>
        void EnsureSubscribe(string userId, IEnumerable<int> subscriptions, IEnumerable<int> usersRoles);

        /// <summary>
        /// Ensure that subscription event is registered 
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="subscriptionEvent"></param>
        /// <param name="group"></param>
        void EnsureSubscriptionData(int subscriptionId, string subscriptionEvent, string group);

        /// <summary>
        /// Ensure that subscription in group 
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="group"></param>
        void EnsureSubscriptionInGroup(int subscriptionId, string group);

        /// <summary>
        /// Ensure that subscription is in roles 
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="roles"></param>
        void EnsureSubscriptionInRoles(int subscriptionId, int[] roles);

        /// <summary>
        /// Ensure unsubscribe list of subscriptions among user's roles.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="subscriptions"></param>
        /// <param name="usersRoles"></param>
        void EnsureUnSubscribe(string userId, IEnumerable<int> subscriptions, IEnumerable<int> usersRoles);

        /// <summary>
        /// Get all subscriptions .
        /// </summary>
        /// <returns></returns>
        IEnumerable<Subscription> GetAllSubscriptions();

        /// <summary>
        /// Get all subscriptions for set of roles.
        /// </summary>
        /// <param name="roles">Set of roles</param>
        /// <returns></returns>
        IEnumerable<Subscription> GetRolesSubscriptions(IEnumerable<int> roles);

        /// <summary>
        /// Get all subscriptions for specific role.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        IEnumerable<Subscription> GetRoleSubscriptions(int roleId);

        /// <summary>
        /// Get all subscribers for a specific action.
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        string[] GetSubscribersFor(int subscription);

        /// <summary>
        /// Get all subscriptions the user can subscribe to and if user already subscribed regards his roles grouped by subscription group.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        IEnumerable<IGrouping<SubscriptionGroup, UserSubscribed>> GetSubscriptionsForUser(string userId,
            IEnumerable<int> roles);

        /// <summary>
        /// Get all subscriptions the user subscribed to.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<int> GetUserSubscriptions(string userId);

        /// <summary>
        /// Remove all user subscriptions linked to specific roles
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        int RemoveRolesSubscriptions(string userId, int[] roles);

        /// <summary>
        /// Subscribe to specific action
        /// </summary>
        /// <param name="userId">User identity</param>
        /// <param name="subscriptionId">The action to subscribe</param>
        void Subscribe(string userId, int subscriptionId);

        /// <summary>
        /// Subscribe to set of actions
        /// </summary>
        /// <param name="userId">User identity</param>
        /// <param name="subscriptions">Array of subscription actions </param>
        void SubscribeRange(string userId, IEnumerable<int> subscriptions);

        /// <summary>
        /// Unsubscribe specific action
        /// </summary>
        /// <param name="userId">User identity </param>
        /// <param name="subscriptionId">The action to unsubscribe.</param>
        void UnSubscribe(string userId, int subscriptionId);

        /// <summary>
        /// Unsubscribe set of actions
        /// </summary>
        /// <param name="userId">User identity </param>
        /// <param name="subscriptions">Array of subscriptions to unsubscribe</param>
        void UnSubscribeRange(string userId, IEnumerable<int> subscriptions);
    }
}