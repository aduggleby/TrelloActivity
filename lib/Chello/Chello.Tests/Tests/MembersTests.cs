using System.Collections.Generic;
using System.Linq;
using Chello.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chello.Tests
{
	[TestClass]
	public class MembersTests : TestBase
	{
		[TestMethod]
		public void Single()
		{
			var chello = new ChelloClient();
			Member member = chello.Members.Single(TestData.UserName);

			Assert.IsNotNull(member);
			Assert.IsFalse(string.IsNullOrEmpty(member.Id));
			Assert.IsFalse(string.IsNullOrEmpty(member.Bio));
			Assert.IsFalse(string.IsNullOrEmpty(member.FullName));
			Assert.IsFalse(string.IsNullOrEmpty(member.Username));
			Assert.IsFalse(string.IsNullOrEmpty(member.Url));
		}

		[TestMethod]
		public void Organizations()
		{
			var chello = new ChelloClient();
			List<Organization> organizations = chello.Members.Organizations(TestData.UserName).ToList();

			Assert.AreEqual(1, organizations.Count);
			Assert.IsFalse(string.IsNullOrEmpty(organizations[0].Id));
			Assert.AreEqual("Test Org Name", organizations[0].Name);
			Assert.AreEqual("Test Org DisplayName", organizations[0].DisplayName);
			Assert.AreEqual("We Can Remember It for You Wholesale", organizations[0].Desc);
		}

		[TestMethod]
		public void AllBoards()
		{
			var chello = new ChelloClient();
			List<Board> boards = chello.Members.AllBoards(TestData.UserName).ToList();
			Assert.AreEqual(1, boards.Count);
		}

		[TestMethod]
		public void Bio()
		{
			var chello = new ChelloClient();
			string bio = chello.Members.Bio(TestData.UserName);
			Assert.IsNotNull(bio);
		}

		[TestMethod]
		public void Fullname()
		{
			var chello = new ChelloClient();
			string fullname = chello.Members.Fullname(TestData.UserName);
			Assert.IsNotNull(fullname);
		}

		[TestMethod]
		public void Url()
		{
			var chello = new ChelloClient();
			string url = chello.Members.Url(TestData.UserName);
			Assert.IsNotNull(url);
		}

		[TestMethod]
		public void Username()
		{
			var chello = new ChelloClient();
			string username = chello.Members.Username(TestData.UserName);
			Assert.IsNotNull(username);
		}

		[TestMethod]
		public void Notifications()
		{
			var chello = new ChelloClient();
			List<Notification> notifications = chello.Members.Notifications(TestData.UserName).ToList();
			Assert.AreNotEqual(0, notifications.Count);
		}

		[TestMethod]
		public void ReadNotifications()
		{
			var chello = new ChelloClient();
			List<Notification> notifications = chello.Members.ReadNotifications(TestData.UserName).ToList();
			Assert.AreNotEqual(0, notifications.Count);
		}

		[TestMethod]
		public void UnreadNotifications()
		{
			var chello = new ChelloClient();
			List<Notification> notifications = chello.Members.UnreadNotifications(TestData.UserName).ToList();
			Assert.AreNotEqual(0, notifications.Count);
		}
	}
}
