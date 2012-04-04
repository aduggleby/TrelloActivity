using System.Collections.Generic;

namespace Chello.Core
{
	public class ListService : TrelloApiBase
	{
		public ListService(string authKey, string authToken)
			: base(authKey, authToken)
		{
		}

		public IEnumerable<List> ForBoard(string boardId)
		{
			return GetRequest<List<List>>("/boards/{0}/lists", boardId);
		}

		public List Single(string listId)
		{
			return GetRequest<List>("/lists/{0}", listId);
		}
	}
}
