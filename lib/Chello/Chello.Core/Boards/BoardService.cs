using System.Collections.Generic;
using Newtonsoft.Json;

namespace Chello.Core
{
	public class BoardService : TrelloApiBase
	{
		public BoardService(string authKey, string authToken)
			: base(authKey, authToken)
		{
		}

		public IEnumerable<Board> ForUser(string username)
		{
			return GetRequest<List<Board>>("/members/{0}/boards", username);
		}

		public IEnumerable<Board> ForOrganization(string organizationName)
		{
			return GetRequest<List<Board>>("organizations/{0}/boards", organizationName);
		}


		public Board Single(string boardId)
		{
			return GetRequest<Board>("/boards/{0}", boardId);
		}
	}
}
