using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Chello.Core
{
	public class Preferences
	{
		[JsonProperty("invitations")]
		[DataMember(Name = "invitations")]
		public string Invitations { get; set; }

		[JsonProperty("comments")]
		[DataMember(Name = "comments")]
		public string Comments { get; set; }

		[JsonProperty("voting")]
		[DataMember(Name = "voting")]
		public string Voting { get; set; }

		[JsonProperty("permissionlevel")]
		[DataMember(Name = "permissionlevel")]
		public string PermissionLevel { get; set; }
	}
}
