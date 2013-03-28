using System;
using System.Linq;
using System.Text;
using CsQuery;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Stajs.BalloonicornHunter.Core.Tests
{
	[TestClass]
	public class SteamIdFinderTests
	{
		private SteamIdFinder _finder;

		[TestInitialize]
		public void TestInitialize()
		{
			_finder = new SteamIdFinder();
		}

		private CQ GetMultiUserDom()
		{
			var bytes = Convert.FromBase64String(ExampleHtml.MultipleResults);
			var s = Encoding.UTF8.GetString(bytes);
			return CQ.CreateDocument(s);
		}

		[TestMethod]
		public void FindLinks_MultipleUsers_Returns50()
		{
			var links = _finder.FindUrls(GetMultiUserDom());

			links.Should().HaveCount(50);
			Assert.IsTrue(links.All(a => a.StartsWith("http://steamcommunity.com/profiles/") ||  a.StartsWith("http://steamcommunity.com/id/")));
		}

		[TestMethod]
		public void Get_MultipleUsers_ReturnsNull()
		{
			var id = _finder.Get(GetMultiUserDom());
			id.Should().NotHaveValue();
		}
	}
}
