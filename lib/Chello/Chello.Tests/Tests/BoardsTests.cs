using System.Collections.Generic;
using System.Linq;
using Chello.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chello.Tests
{
	[TestClass]
	public class BoardsTests : TestBase
	{
		[TestMethod]
		public void ForUser()
		{
			var chello = new ChelloClient();
			IEnumerable<Board> boards = chello.Boards.ForUser(TestData.UserName);
			Assert.IsTrue(boards.Count() > 0);
		}

		[TestMethod]
		public void Single()
		{
			var chello = new ChelloClient();
			Board board = chello.Boards.Single(TestData.BoardName);
			Assert.IsNotNull(board);
			Assert.AreEqual("Test Board", board.Name);
		}
	}
}
