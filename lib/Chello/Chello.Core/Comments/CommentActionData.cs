using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Chello.Core
{
	[DataContract]
	public class CommentActionData
	{
		[JsonProperty("text")]
		[DataMember(Name = "text")]
		public string Text { get; set; }

		[JsonProperty("card")]
		[DataMember(Name = "card")]
		public Card Card { get; set; }
	}
}
