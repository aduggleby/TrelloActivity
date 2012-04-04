using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Chello.Core
{
	[DataContract]
	public class Organization : ITrelloEntity
	{
		[JsonProperty("id")]
		[DataMember(Name = "id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		[DataMember(Name = "name")]
		public string Name { get; set; }

		[JsonProperty("displayname")]
		[DataMember(Name = "displayname")]
		public string DisplayName { get; set; }

		[JsonProperty("desc")]
		[DataMember(Name = "desc")]
		public string Desc { get; set; }

		[JsonProperty("url")]
		[DataMember(Name = "url")]
		public string Url { get; set; }
	}
}
