using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Chello.Core
{
	[DataContract]
	public class Notification : ITrelloEntity
	{
		[JsonProperty("id")]
		[DataMember(Name = "id")]
		public string Id { get; set; }

		[JsonProperty("unread")]
		[DataMember(Name = "unread")]
		public bool Unread { get; set; }

		[JsonProperty("type")]
		[DataMember(Name = "type")]
		public string Type { get; set; }

		[JsonProperty("date")]
		[DataMember(Name = "date")]
		public DateTime Date { get; set; }

		[JsonProperty("data")]
		[DataMember(Name = "data")]
		public string Data { get; set; }

		[JsonProperty("idMemberCreator")]
		[DataMember(Name = "idMemberCreator")]
		public string IdMemberCreator { get; set; }
	}
}
