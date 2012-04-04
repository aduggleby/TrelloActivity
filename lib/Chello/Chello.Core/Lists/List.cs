using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Chello.Core
{
	[DataContract]
	public class List : ITrelloEntity
	{
		[JsonProperty("id")]
		[DataMember(Name = "id")]
		public string Id { get; set; }

		[JsonProperty("idboard")]
		[DataMember(Name = "idboard")]
		public string IdBoard { get; set; }

		[JsonProperty("name")]
		[DataMember(Name = "name")]
		public string Name { get; set; }
	}
}
