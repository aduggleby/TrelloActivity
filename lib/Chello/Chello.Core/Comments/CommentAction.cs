using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Chello.Core
{
	[DataContract]
	public class CommentAction
	{
		[JsonProperty("id")]
		[DataMember(Name = "id")]
		public string Id { get; set; }

		[JsonProperty("data")]
		[DataMember(Name = "data")]
		public CommentActionData Data { get; set; }
	}
}
