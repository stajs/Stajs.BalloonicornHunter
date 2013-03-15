using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stajs.BalloonicornHunter.Core.Exceptions;
using Stajs.BalloonicornHunter.Core.Extensions;

namespace Stajs.BalloonicornHunter.Core.Server
{
	public class PlayerResponse
	{
		public byte[] Bytes { get; private set; }
		public string Hex { get; private set; }
		public string Base64 { get; private set; }
		public string RawString { get; private set; }

		public ResponseFormat ResponseFormat { get; private set; }
		public string Header { get; private set; }
		public byte PlayerCount { get; private set; }
		public List<Player> Players { get; private set; }

		private const string ExamplePlayerResponse = "/////0QWAHM4LiBrcnV6AI8AAAA4Y2hGAFNhdXNhZ2UgaW4gYSBCb3gACwAAANwpBUYASm95ZnVsU29ycm93WgAaAAAAoib9RQBKaW1teQCfAAAAjz/NRQBNeXN0ZXJpYWwgQj5mYW0gZmV6L2Jvb3RpZSB0aW1lABwAAADEBshFAFRvbSBIYW5rcwBQAAAAZ9q9RQBELUotTS05OQAmAAAAU2WtRQBCT0RZU0hPVEFJTUJPVC5FWEUARgAAAEkGmEUATWVyY2VuYXJ5AC4AAAAzpJJFAFBBSU5JUyBDVVBDQUtFAAAAAACaO4JFAHM4LiBOb20gSmVyZW15ABUAAAAzd3tFAEVkd2FyZCBSeWtmaWVsZAAIAAAAmo9xRQBMZU1vdGNoAAgAAACx1WtFAEJsYWNrIENhdmlhcgAXAAAAAlhpRQBbU3lsYXJd4oSiACAAAABd61dFAHBvbnl8ZC1fLWLimavimarimavimavimarimarimaoABAAAAIJP4kQAU2FsbW9uIDIxNDIAAwAAALiGiEQAVGVuZGVybG9pbnMABAAAAHX8dkQAeERzOkhpVEdpUkwgxrjMtcyh05zMtcyozITGtwADAAAAOlpeRADilqAgRG91Y2hlIEJpZ2Fsb3cg4pagAAMAAAB/xMJDAE1JTE9NT05TVEVSNQADAAAAayKSQwBDYWxkYXdn4oSiAAAAAADArRdC";

		public PlayerResponse() : this(Convert.FromBase64String(ExamplePlayerResponse))
		{
			
		}
	
		public PlayerResponse(byte[] bytes)
		{
			Bytes = bytes;
			Hex = BitConverter.ToString(bytes);
			Base64 = Convert.ToBase64String(bytes);
			RawString = Encoding.UTF8.GetString(bytes);
			Parse(bytes);
		}

		private void Parse(byte[] bytes)
		{
			var s = Encoding.UTF8.GetString(bytes);

			ResponseFormat = (ResponseFormat) bytes.ReadInt();
			bytes = bytes.RemoveFromStart(4);

			s = Encoding.UTF8.GetString(bytes);

			if (ResponseFormat != ResponseFormat.Simple)
				throw new NotImplementedException("Not ready to handle multi-packet responses yet."); // TODO
			
			Header = bytes.ReadString(1);
			bytes = bytes.RemoveFromStart(1);

			s = Encoding.UTF8.GetString(bytes);

			PlayerCount = bytes[0];
			bytes = bytes.RemoveFromStart(1);

			s = Encoding.UTF8.GetString(bytes);

			Players = new List<Player>();

			for (var i = 0; i < PlayerCount; i++)
			{
				var player = new Player();

				player.Index = bytes[0];
				bytes = bytes.RemoveFromStart(1);

				if (player.Index != 0)
					throw new HolyShitException("Player index is used!");

				s = Encoding.UTF8.GetString(bytes);

				int length;
				player.Name = bytes.ReadStringUntilNullTerminator(out length);

				bytes = bytes.RemoveFromStart(length + 1);

				s = Encoding.UTF8.GetString(bytes);

				player.Score = bytes.ReadInt();
				bytes = bytes.RemoveFromStart(4);

				s = Encoding.UTF8.GetString(bytes);

				player.Duration = bytes.ReadSingle();
				bytes = bytes.RemoveFromStart(4);

				s = Encoding.UTF8.GetString(bytes);

				Players.Add(player);
			}
			
			Debug.Assert(bytes.Length == 0);
		}
	}
}
