using System;
using System.Collections.Generic;

namespace Chello.Core
{
	public class MemberService : TrelloApiBase
	{
		public MemberService(string authKey, string authToken)
			: base(authKey, authToken)
		{
		}

		public Member Single(string username)
		{
			return GetRequest<Member>("/members/{0}", username);
		}

		public IEnumerable<Member> ForCard(string cardId)
		{
			return GetRequest<List<Member>>("/cards/{0}/members", cardId);
		}

		public IEnumerable<Member> ForBoard(string boardId)
		{
			return GetRequest<List<Member>>("/boards/{0}/members", boardId);
		}

		public string Bio(string username)
		{
			return GetRequest("/members/{0}/bio", username);
		}

		public string Fullname(string username)
		{
			return GetRequest("/members/{0}/fullname", username);
		}

		public string Gravatar(string username)
		{
			return GetRequest("/members/{0}/gravatar", username);
		}

		public string Username(string userId)
		{
			return GetRequest("/members/{0}/url", userId);
		}

		public string Url(string username)
		{
			return GetRequest("/members/{0}/url", username);
		}

		public IEnumerable<Organization> Organizations(string username)
		{
			return GetRequest<List<Organization>>("/members/{0}/organizations", username);
		}

		public IEnumerable<Member> MembersInOrganizations(string username)
		{
			return GetRequest<List<Member>>("/members/{0}/organizations/members", username);
		}

		public IEnumerable<Notification> Notifications(string username)
		{
			if (string.IsNullOrEmpty(AuthToken))
				throw new ChelloException("The AuthToken setting is null/empty but is required for notifications.");

			return GetRequest<List<Notification>>("/members/{0}/notifications", username);
		}

		public IEnumerable<Notification> ReadNotifications(string username)
		{
			if (string.IsNullOrEmpty(AuthToken))
				throw new ChelloException("The AuthToken setting is null/empty but is required for read notifications.");

			return GetRequest<List<Notification>>("/members/{0}/notifications", username);
		}

		public IEnumerable<Notification> UnreadNotifications(string username)
		{
			if (string.IsNullOrEmpty(AuthToken))
				throw new ChelloException("The AuthToken setting is null/empty but is required for unread notifications.");

			return GetRequest<List<Notification>>("/members/{0}/notifications", username);
		}

		public IEnumerable<Card> Cards(string username)
		{
			throw new NotImplementedException("Cards by username hasn't been implemented yet");

			//return GetRequest<List<Card>>("/members/{0}/cards", username);
		}

		public IEnumerable<Board> AllBoards(string username)
		{
			return GetRequest<List<Board>>("/members/{0}/boards", username);
		}

		public IEnumerable<Board> PublicBoards(string username)
		{
			return GetRequest<List<Board>>("/members/{0}/boards/public", username);
		}

		public IEnumerable<Board> PinnedBoards(string username)
		{
			throw new NotImplementedException("Calls requiring authentication tokens haven't been implemented yet");

			//return GetRequest<List<Board>>("/members/{0}/boards/pinned", username);
		}

		public IEnumerable<Board> UnPinnedBoards(string username)
		{
			throw new NotImplementedException("Calls requiring authentication tokens haven't been implemented yet");

			//return GetRequest<List<Board>>("/members/{0}/boards/unpinned", username);
		}
	}
}
