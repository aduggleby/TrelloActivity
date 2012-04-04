using System.Collections.Generic;
using System.Linq;
using Chello.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chello.Tests
{
	[TestClass]
	public class OrganizationTests : TestBase
	{
		[TestMethod]
		public void Single()
		{
			var chello = new ChelloClient();
			Organization organization = chello.Organizations.Single(TestData.OrganizationName);

			Assert.IsNotNull(organization);
			Assert.IsFalse(string.IsNullOrEmpty(organization.Id));
			Assert.AreEqual("Test Org Name", organization.Name);
			Assert.AreEqual("Test Org DisplayName", organization.DisplayName);
			Assert.AreEqual("We Can Remember It for You Wholesale", organization.Desc);
		}

		[TestMethod]
		public void AllBoards()
		{
			var chello = new ChelloClient();
			List<Board> boards = chello.Organizations.AllBoards(TestData.OrganizationName).ToList();
			Assert.AreEqual(1, boards.Count);
		}

		[TestMethod]
		public void AllMembers()
		{
			var chello = new ChelloClient();
			List<Member> members = chello.Organizations.AllMembers(TestData.OrganizationName).ToList();
			Assert.AreEqual(1, members.Count);

			Assert.IsFalse(string.IsNullOrEmpty(members[0].Id));
			Assert.IsFalse(string.IsNullOrEmpty(members[0].Bio));
			Assert.IsFalse(string.IsNullOrEmpty(members[0].FullName));
			Assert.IsFalse(string.IsNullOrEmpty(members[0].Username));
			Assert.IsFalse(string.IsNullOrEmpty(members[0].Url));
		}

		[TestMethod]
		public void Desc()
		{
			var chello = new ChelloClient();
			string desc = chello.Organizations.Description(TestData.OrganizationName);
			Assert.IsNotNull(desc);
		}

		[TestMethod]
		public void DisplayName()
		{
			var chello = new ChelloClient();
			string displayName = chello.Organizations.DisplayName(TestData.OrganizationName);
			Assert.IsNotNull(displayName);
		}

		[TestMethod]
		public void Name()
		{
			var chello = new ChelloClient();
			string fullname = chello.Organizations.Name(TestData.OrganizationName);
			Assert.IsNotNull(fullname);
		}

		[TestMethod]
		public void Url()
		{
			var chello = new ChelloClient();
			string url = chello.Organizations.Url(TestData.OrganizationName);
			Assert.IsNotNull(url);
		}
	}
}
