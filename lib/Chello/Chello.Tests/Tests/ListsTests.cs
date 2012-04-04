using System.Collections.Generic;
using System.Linq;
using Chello.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chello.Tests
{
	[TestClass]
	public class ListsTests : TestBase
	{
		[TestMethod]
		public void ForBoard()
		{
			var chello = new ChelloClient();
			IEnumerable<List> lists = chello.Lists.ForBoard(TestData.BoardName);
			Assert.IsTrue(lists.Count() > 0);
		}

		[TestMethod]
		public void Single()
		{
			var chello = new ChelloClient();
			List list = chello.Lists.Single(TestData.List1Name);
			Assert.IsNotNull(list);
			Assert.AreEqual("Test List", list.Name);
		}
	}
}
