using Chello.Core;

namespace Chello.Tests
{
	class BasicTestData
	{
		public static TestData Create()
		{
			// TODO: Make this better. For now, we just want to get the existing tests working against the test service.
			var testData = new TestData("BASIC");

			testData.Add(new Board { Id = "TESTBOARD1", Name = "Test Board" });
			testData.Add(new List { Id = "TESTLIST1", IdBoard = "TESTBOARD1", Name = "Test List" });
			testData.Add(new Card { Id = "TESTCARD1", IdBoard = "TESTBOARD1", IdList = "TESTLIST1", Name = "Test Card Name" });
			testData.Add(new Organization { Id = "TESTORG1", Name = "Test Org Name", DisplayName = "Test Org DisplayName", Desc = "We Can Remember It for You Wholesale" });
			testData.Add(new Member { Id = "TESTMEMB1", Username = "TESTMEMB1", FullName = "Test Member", Bio = "Test", Gravatar = "gra", Url = "http://test" });
			testData.Add(new Notification { Id = "UNREADMSG1", Unread = true });
			testData.Add(new Notification { Id = "READMSG1", Unread = false });

			// TODO: Can we make this suff automatically link up with the IdBoard (etc.) properties?
			testData.AddRelationship<Member, Organization>("TESTMEMB1", "TESTORG1");
			testData.AddRelationship<Card, Board>("TESTCARD1", "TESTBOARD1");
			testData.AddRelationship<List, Board>("TESTLIST1", "TESTBOARD1");
			testData.AddRelationship<Member, Board>("TESTMEMB1", "TESTBOARD1");
			testData.AddRelationship<Organization, Board>("TESTORG1", "TESTBOARD1");
			testData.AddRelationship<Member, Notification>("TESTMEMB1", "UNREADMSG1");
			testData.AddRelationship<Member, Notification>("TESTMEMB1", "READMSG1");

			return testData;
		}
	}
}
