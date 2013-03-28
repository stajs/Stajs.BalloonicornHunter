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

		private CQ GetDom(string html)
		{
			var bytes = Convert.FromBase64String(html);
			var s = Encoding.UTF8.GetString(bytes);
			return CQ.CreateDocument(s);
		}

		private CQ GetEmpty()
		{
			return GetDom(ExampleHtml.Empty);
		}

		private CQ GetOneId()
		{
			return GetDom(ExampleHtml.OneId);
		}

		private CQ GetOneVanityUrl()
		{
			return GetDom(ExampleHtml.OneVanityUrl);
		}

		private CQ GetMultiple()
		{
			return GetDom(ExampleHtml.Multiple);
		}

		[TestMethod]
		public void FindLinks_Multiple_Returns50()
		{
			var links = _finder.FindUrls(GetMultiple());

			links.Should().HaveCount(50);
			Assert.IsTrue(links.All(a => a.StartsWith("http://steamcommunity.com/profiles/") ||  a.StartsWith("http://steamcommunity.com/id/")));
		}

		[TestMethod]
		public void FindLinks_OneId_Returns1()
		{
			var links = _finder.FindUrls(GetOneId());

			links.Should().HaveCount(1);
			Assert.IsTrue(links.All(a => a.StartsWith("http://steamcommunity.com/profiles/") ||  a.StartsWith("http://steamcommunity.com/id/")));
		}

		[TestMethod]
		public void FindLinks_OneVanityUrl_Returns1()
		{
			var links = _finder.FindUrls(GetOneVanityUrl());

			links.Should().HaveCount(1);
			Assert.IsTrue(links.All(a => a.StartsWith("http://steamcommunity.com/profiles/") || a.StartsWith("http://steamcommunity.com/id/")));
		}

		[TestMethod]
		public void FindLinks_Empty_Returns0()
		{
			var links = _finder.FindUrls(GetEmpty());

			links.Should().BeEmpty();
		}

		[TestMethod]
		public void Get_Multiple_ReturnsNull()
		{
			var id = _finder.Get(GetMultiple());
			id.Should().NotHaveValue();
		}

		[TestMethod]
		public void Get_OneId_ReturnsSomething()
		{
			var id = _finder.Get(GetOneId());
			var expected = 76561198043860630L;

			id.Should().HaveValue();
			id.Should().Be(expected);
		}

		[TestMethod]
		public void Get_OneVanityUrl_ReturnsNull()
		{
			var id = _finder.Get(GetEmpty());
			id.Should().NotHaveValue();
		}

		[TestMethod]
		public void Get_Empty_ReturnsNull()
		{
			var id = _finder.Get(GetEmpty());
			id.Should().NotHaveValue();
		}

		[TestMethod, Ignore]
		public void Wat()
		{
			var dom = CQ.CreateFromUrl("http://steamcommunity.com/actions/Search?T=Account&K=%22jdog611%22");
			var bytes = Encoding.UTF8.GetBytes(dom.Render());
			var s = Convert.ToBase64String(bytes);
		}
	}
}
