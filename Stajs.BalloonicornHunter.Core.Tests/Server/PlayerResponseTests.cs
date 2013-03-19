using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stajs.BalloonicornHunter.Core.Server;

namespace Stajs.BalloonicornHunter.Core.Tests.Server
{
	[TestClass]
	public class PlayerResponseTests
	{
		[TestMethod]
		public void HandleUtfPlayerName()
		{
			const string base64PlayerResponseWithUtfCombinedCharactersInPlayerName = "/////0QWAHM4LiBrcnV6AI8AAAA4Y2hGAFNhdXNhZ2UgaW4gYSBCb3gACwAAANwpBUYASm95ZnVsU29ycm93WgAaAAAAoib9RQBKaW1teQCfAAAAjz/NRQBNeXN0ZXJpYWwgQj5mYW0gZmV6L2Jvb3RpZSB0aW1lABwAAADEBshFAFRvbSBIYW5rcwBQAAAAZ9q9RQBELUotTS05OQAmAAAAU2WtRQBCT0RZU0hPVEFJTUJPVC5FWEUARgAAAEkGmEUATWVyY2VuYXJ5AC4AAAAzpJJFAFBBSU5JUyBDVVBDQUtFAAAAAACaO4JFAHM4LiBOb20gSmVyZW15ABUAAAAzd3tFAEVkd2FyZCBSeWtmaWVsZAAIAAAAmo9xRQBMZU1vdGNoAAgAAACx1WtFAEJsYWNrIENhdmlhcgAXAAAAAlhpRQBbU3lsYXJd4oSiACAAAABd61dFAHBvbnl8ZC1fLWLimavimarimavimavimarimarimaoABAAAAIJP4kQAU2FsbW9uIDIxNDIAAwAAALiGiEQAVGVuZGVybG9pbnMABAAAAHX8dkQAeERzOkhpVEdpUkwgxrjMtcyh05zMtcyozITGtwADAAAAOlpeRADilqAgRG91Y2hlIEJpZ2Fsb3cg4pagAAMAAAB/xMJDAE1JTE9NT05TVEVSNQADAAAAayKSQwBDYWxkYXdn4oSiAAAAAADArRdC";
			const string playerNameWithUtfCombinedCharacters = "[Sylar]™";
			var bytes = Convert.FromBase64String(base64PlayerResponseWithUtfCombinedCharactersInPlayerName);

			var playerResponse = new PlayerResponse(bytes);

			playerResponse.Players.Should().ContainSingle(p => p.Name == playerNameWithUtfCombinedCharacters);
		}
	}
}
