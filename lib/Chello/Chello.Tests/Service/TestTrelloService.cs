using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using Chello.Core;

namespace Chello.Tests
{
	[ServiceContract]
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)] // This means one instance per test (since TestInitialize/Cleanup recreates for each test), to allow state changes.
	public class TestTrelloService
	{
		Dictionary<string, TestData> data = new Dictionary<string, TestData>();

		public TestTrelloService()
		{
			// Grab all test data
			// TODO: This should be loaded/parsed from somewhere, not hard-coded.
			// DT - The idea is to have sets of test data keyed by the AuthKey, so any test can define the set of
			// test data that it starts with, allowing custom data for different types of tests, without every test
			// having to set up its own set of data.
			// It would be good to extend this so that if tests do need specific data, they could define it, but that
			// might be tricky because the Tests and the Test Service can only currently communicate via the service
			var allTestData = new TestData[] {
				BlankTestData.Create(),
				BasicTestData.Create()
			};

			// Add all the test data to the dictionay, based on auth key.
			foreach (var testData in allTestData)
				data.Add(testData.AuthKey, testData);
		}

		#region Boards

		[OperationContract]
		[WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "boards/{boardId}?key={key}")]
		public Board Board(string key, string boardId)
		{
			return data[key].Get<Board>(boardId);
		}

		[OperationContract]
		[WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "organizations/{organizationId}/boards?key={key}")]
		public List<Board> BoardsForOrganization(string key, string organizationId)
		{
			// TODO: Make this support org name too
			return data[key].GetRelated<Organization, Board>(organizationId).ToList();
		}

		[OperationContract]
		[WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "members/{userId}/boards?key={key}")]
		public List<Board> BoardsForMember(string key, string userId)
		{
			// TODO: Make this support org name too
			return data[key].GetRelated<Member, Board>(userId).ToList();
		}

		#endregion

		#region Lists

		[OperationContract]
		[WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "lists/{listId}?key={key}")]
		public List List(string key, string listId)
		{
			return data[key].Get<List>(listId);
		}

		[OperationContract]
		[WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "boards/{boardId}/lists?key={key}")]
		public List<List> ListsForBoard(string key, string boardId)
		{
			return data[key].GetRelated<Board, List>(boardId).ToList();
		}

		#endregion

		#region Cards

		[OperationContract]
		[WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "cards/{cardId}?key={key}")]
		public Card Card(string key, string cardId)
		{
			return data[key].Get<Card>(cardId);
		}

		[OperationContract]
		[WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "boards/{boardId}/cards?key={key}")]
		public List<Card> CardsForBoard(string key, string boardId)
		{
			return data[key].GetRelated<Board, Card>(boardId).ToList();
		}

		[OperationContract]
		[WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "cards/{cardId}?key={key}")]
		public void UpdateCard(string key, string cardId, Card updatedCard)
		{
			// Make sure we store the card with the ID we were passed.
			updatedCard.Id = cardId;
			data[key].Update(updatedCard);
		}

		[OperationContract]
		[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "cards/{cardId}/members?key={key}")]
		public void CardAddMember(string key, string cardId, AddMemberData memberId)
		{
			data[key].AddRelationship<Card, Member>(cardId, memberId.Value);
		}

		[DataContract]
		public class AddMemberData
		{
			[DataMember(Name = "value")]
			public string Value { get; set; }
		}

		[OperationContract]
		[WebInvoke(Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "cards/{cardId}/members/{memberId}?key={key}")]
		public void CardRemoveMember(string key, string cardId, string memberId)
		{
			data[key].RemoveRelationship<Card, Member>(cardId, memberId);
		}

		#endregion

		#region Organizations

		[OperationContract]
		[WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "organizations/{organizationId}?key={key}")]
		public Organization Organization(string key, string organizationId)
		{
			// TODO: Make this work on org name as well as ID.
			return data[key].Get<Organization>(organizationId);
		}

		[OperationContract]
		[WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "members/{memberId}/organizations?key={key}")]
		public List<Organization> OrganizationsForMember(string key, string memberId)
		{
			// TODO: Make this work with username as well as ID.
			return data[key].GetRelated<Member, Organization>(memberId).ToList();
		}

		#endregion

		#region Members

		[OperationContract]
		[WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "members/{memberId}?key={key}")]
		public Member Member(string key, string memberId)
		{
			// TODO: Support username *and* user ID.
			return data[key].Get<Member>(memberId);
		}

		[OperationContract]
		[WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "organizations/{organizationId}/members?key={key}")]
		public List<Member> MembersForOrganization(string key, string organizationId)
		{
			return data[key].GetRelated<Organization, Member>(organizationId).ToList();
		}

		[OperationContract]
		[WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "boards/{boardId}/members?key={key}")]
		public List<Member> MembersForBoard(string key, string boardId)
		{
			return data[key].GetRelated<Board, Member>(boardId).ToList();
		}

		[OperationContract]
		[WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "cards/{cardId}/members?key={key}")]
		public List<Member> MembersForCard(string key, string cardId)
		{
			return data[key].GetRelated<Card, Member>(cardId).ToList();
		}

		[OperationContract]
		[WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "members/{memberId}/notifications?key={key}")]
		public List<Notification> Notifications(string key, string memberId)
		{
			return data[key].GetRelated<Member, Notification>(memberId).ToList();
		}

		[OperationContract]
		[WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "members/{memberId}/notifications/read?key={key}")]
		public List<Notification> ReadNotifications(string key, string memberId)
		{
			return data[key].GetRelated<Member, Notification>(memberId).Where(n => n.Unread == false).ToList();
		}

		[OperationContract]
		[WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "members/{memberId}/notifications/unread?key={key}")]
		public List<Notification> UnreadNotifications(string key, string memberId)
		{
			return data[key].GetRelated<Member, Notification>(memberId).Where(n => n.Unread == true).ToList();
		}
		#endregion
	}
}