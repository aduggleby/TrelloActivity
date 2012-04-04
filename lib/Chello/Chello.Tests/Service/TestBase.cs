using System;
using System.ServiceModel.Web;
using Chello.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace Chello.Tests
{
	[TestClass]
	public class TestBase
	{
		private WebServiceHost _testServiceHost;
		private Uri _testServiceAddress;

		static TestBase()
		{
			Config.AuthKey = ConfigurationManager.AppSettings["AuthKey"]; // may need to change this to per instance
			Config.AuthToken = ConfigurationManager.AppSettings["AuthToken"];
			Config.ApiBaseUrl = ConfigurationManager.AppSettings["TestServiceUrl"];
		}

		[TestInitialize]
		public void Initialize()
		{
			string authKey = ConfigurationManager.AppSettings["AuthKey"];

			_testServiceAddress = new Uri(ConfigurationManager.AppSettings["TestServiceUrl"]);
			_testServiceHost = new WebServiceHost(typeof(TestTrelloService), _testServiceAddress);
			_testServiceHost.Open();
		}

		[TestCleanup]
		public void Cleanup()
		{
			_testServiceHost.Close();
		}
	}
}
