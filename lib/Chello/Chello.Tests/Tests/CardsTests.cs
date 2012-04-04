using System.Collections.Generic;
using System.Linq;
using Chello.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chello.Tests
{
	[TestClass]
	public class CardsTests : TestBase
	{
		[TestMethod]
		public void ForBoard()
		{
			var chello = new ChelloClient();
			IEnumerable<Card> cards = chello.Cards.ForBoard(TestData.BoardName);
			Assert.IsTrue(cards.Count() > 0);
		}

		[TestMethod]
		public void Single()
		{
			var chello = new ChelloClient();
			Card card = chello.Cards.Single(TestData.CardName);
			Assert.IsNotNull(card);
			Assert.AreEqual("Test Card Name", card.Name);
		}

		[TestMethod]
		public void MoveCard()
		{
			var chello = new ChelloClient();
			Card card = chello.Cards.Single(TestData.CardName);

			// Make sure the card is on List1 (othrwise the test won't be testing anything)
			Assert.AreEqual(TestData.List1Name, card.IdList);

			// Move the card to List2
			card.IdList = TestData.List2Name;
			chello.Cards.Update(card);

			// Fetch the card back
			card = chello.Cards.Single(TestData.CardName);

			// Check the card is now on List2
			Assert.AreEqual(TestData.List2Name, card.IdList);
		}

		[TestMethod]
		public void AddRemoveMember()
		{
			var chello = new ChelloClient();
			var members = chello.Members.ForCard(TestData.CardName);

			// Make sure the card has no members
			Assert.AreEqual(0, members.Count());

			// Add the test member to the card
			chello.Cards.AddMember(TestData.CardName, TestData.UserName);

			// Fetch the member list again
			members = chello.Members.ForCard(TestData.CardName);

			// Make sure the card has the member
			Assert.AreEqual(1, members.Count());

			// Remove the test member from the card
			chello.Cards.RemoveMember(TestData.CardName, TestData.UserName);

			// Fetch the member list again
			members = chello.Members.ForCard(TestData.CardName);

			// Make sure the card has no members
			Assert.AreEqual(0, members.Count());
		}
	}
}
