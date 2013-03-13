using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stajs.BalloonicornHunter.Core.Server;

namespace Stajs.BalloonicornHunter.Core.Tests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			var playerResponse = new PlayerResponse(ExampleResponses.PlayerResponse);
		}
	}
}
