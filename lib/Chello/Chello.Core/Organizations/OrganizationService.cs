using System.Collections.Generic;

namespace Chello.Core
{
	public class OrganizationService : TrelloApiBase
	{
		public OrganizationService(string authKey, string authToken)
			: base(authKey, authToken)
		{
		}

		public Organization Single(string organizationName)
		{
			return GetRequest<Organization>("/organizations/{0}", organizationName);
		}

		// TODO: Some methods are blah.ForSomething, and others are something.AllBlahs()... Can we make this more consistent?
		public IEnumerable<Board> AllBoards(string organizationName)
		{
			return GetRequest<List<Board>>("/organizations/{0}/boards", organizationName);
		}

		public IEnumerable<Member> AllMembers(string organizationName)
		{
			return GetRequest<List<Member>>("/organizations/{0}/members", organizationName);
		}

		public string Name(string organizationName)
		{
			return GetRequest("/organizations/{0}/name", organizationName);
		}

		public string DisplayName(string organizationName)
		{
			return GetRequest("/organizations/{0}/displayName", organizationName);
		}

		public string Description(string organizationName)
		{
			return GetRequest("/organizations/{0}/desc", organizationName);
		}

		public string Url(string organizationName)
		{
			return GetRequest("/organizations/{0}/url", organizationName);
		}
	}
}
