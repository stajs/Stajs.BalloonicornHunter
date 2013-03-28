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
		private CQ GetMultiUserDom()
		{
			var bytes = Convert.FromBase64String(ExampleHtml.MultipleResults);
			var s = Encoding.UTF8.GetString(bytes);
			return CQ.CreateDocument(s);
		}

		[TestMethod]
		public void FindLinks_MultipleUsers_Returns50()
		{
			var steamIdFinder = new SteamIdFinder();
			var links = steamIdFinder.FindUrls(GetMultiUserDom());

			links.Should().HaveCount(50);
			Assert.IsTrue(links.All(a => a.StartsWith("http://steamcommunity.com/profiles/") ||  a.StartsWith("http://steamcommunity.com/id/")));
		}
	}
}
