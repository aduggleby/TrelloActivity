using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Chello.Core
{
	[DataContract]
	public class Member : ITrelloEntity
	{
		[JsonProperty("id")]
		[DataMember(Name = "id")]
		public string Id { get; set; }

		[JsonProperty("fullname")]
		[DataMember(Name = "fullname")]
		public string FullName { get; set; }

		[JsonProperty("username")]
		[DataMember(Name = "username")]
		public string Username { get; set; }

		[JsonProperty("gravatar")]
		[DataMember(Name = "gravatar")]
		public string Gravatar { get; set; }

		[JsonProperty("bio")]
		[DataMember(Name = "bio")]
		public string Bio { get; set; }

		[JsonProperty("url")]
		[DataMember(Name = "url")]
		public string Url { get; set; }
	}
}
