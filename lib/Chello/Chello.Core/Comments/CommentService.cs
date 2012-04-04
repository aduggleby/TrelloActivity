using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Chello.Core
{
	public class CommentService : TrelloApiBase
	{
		public CommentService(string authKey, string authToken)
			: base(authKey, authToken)
		{
		}

		public IEnumerable<CommentAction> ForBoard(string boardId)
		{
			return GetRequest<List<CommentAction>>("/boards/{0}/actions?filter=commentCard", boardId);
		}
	}
}
