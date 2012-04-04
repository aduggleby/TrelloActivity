namespace Chello.Core
{
	public class NotificationService : TrelloApiBase
	{
		public NotificationService(string authKey, string authToken)
			: base(authKey, authToken)
		{
		}

		// Requires authentication tokens to work, which haven't been added yet.
	}
}
