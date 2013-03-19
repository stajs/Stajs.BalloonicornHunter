using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Stajs.BalloonicornHunter.Core.Extensions;

namespace Stajs.BalloonicornHunter.Core.Tests.Extensions
{
	[TestClass]
	public class ArrayExtensionsTests
	{
		[TestMethod]
		public void HandleUtfCombinedCharacters()
		{
			const string base64PlayerNameWithCombinedCharacters = "W1N5bGFyXeKEog==";
			const string expected = "[Sylar]™";

			var bytes = Convert.FromBase64String(base64PlayerNameWithCombinedCharacters);
			
			int length;
			var actual = bytes.ReadStringUntilNullTerminator(out length);

			actual.Should().Be(expected);
			length.Should().BeGreaterThan(actual.Length);
		}
	}
}
