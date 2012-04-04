using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Chello.Core
{
	public class CardService : TrelloApiBase
	{
		public CardService(string authKey, string authToken)
			: base(authKey, authToken)
		{
		}

		public IEnumerable<Card> ForBoard(string boardId)
		{
			return GetRequest<List<Card>>("/boards/{0}/cards", boardId);
		}

		public IEnumerable<Card> ForList(string listId)
		{
			return GetRequest<List<Card>>("/lists/{0}/cards", listId);
		}

		public Card Single(string cardId)
		{
			return GetRequest<Card>("/cards/{0}", cardId);
		}

		public Card Update(Card card)
		{
			return PutRequest<Card, Card>(card, "/cards/{0}", card.Id);
		}

		public Card Create(Card card)
		{
			return PostRequest<Card, Card>(card, "/cards");
		}

		public List<Comment> AddComment(string cardId, string commentText)
		{
			return PostRequest<List<Comment>, object>(new { text = commentText }, "/cards/{0}/actions/comments", cardId);
		}

		public List<Member> AddMember(string cardId, string memberId)
		{
			return PostRequest<List<Member>, object>(new { value = memberId }, "/cards/{0}/members", cardId);
		}

		public void RemoveMember(string cardId, string memberId)
		{
			DeleteRequest("/cards/{0}/members/{1}", cardId, memberId);
		}
	}
}
