using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Chello.Core
{
	[DataContract]
	public class Comment : ITrelloEntity
	{
		[JsonProperty("id")]
		[DataMember(Name = "id")]
		public string Id { get; set; }

		[JsonProperty("text")] // TODO: See if we need these, or if DataMember is checked by Newtonsoft too
		[DataMember(Name = "text")]
		public string Text { get; set; }

		[JsonProperty("idCard")]
		[DataMember(Name = "idCard")]
		public string IdCard { get; set; }
	}
}
