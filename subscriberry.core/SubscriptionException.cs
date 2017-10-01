using System;

namespace Subscriberry.core
{
	public class SubscriptionException : Exception
	{
		public SubscriptionException(string message) : base(message)
		{
		}

		public SubscriptionException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}