using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Chello.Core
{
	public class Board : ITrelloEntity
	{
		[JsonProperty("id")]
		[DataMember(Name = "id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		[DataMember(Name = "name")]
		public string Name { get; set; }

		[JsonProperty("desc")]
		[DataMember(Name = "desc")]
		public string Desc { get; set; }

		[JsonProperty("closed")]
		[DataMember(Name = "closed")]
		public bool IsClosed { get; set; }

		[JsonProperty("idorganization")]
		[DataMember(Name = "idorganization")]
		public string IdOrganization { get; set; }

		[JsonProperty("url")]
		[DataMember(Name = "url")]
		public string Url { get; set; }

		[JsonProperty("prefs")]
		[DataMember(Name = "prefs")]
		public Preferences Prefs { get; set; }

		public Board()
		{
			Prefs = new Preferences();
		}
	}
}
