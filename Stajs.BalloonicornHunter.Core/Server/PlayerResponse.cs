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
			ResponseFormat = (ResponseFormat) bytes.ReadInt();
			bytes = bytes.RemoveFromStart(4);

			if (ResponseFormat != ResponseFormat.Simple)
				throw new NotImplementedException("Not ready to handle multi-packet responses yet."); // TODO
			
			Header = bytes.ReadString(1);
			bytes = bytes.RemoveFromStart(1);

			bytes = bytes.RemoveFromStart(1);

			Players = new List<Player>();

			for (var i = 0; i < PlayerCount; i++)
			{
				var player = new Player();

				player.Index = bytes[0];
				bytes = bytes.RemoveFromStart(1);

				if (player.Index != 0)
					throw new HolyShitException("Player index is used!");

				int length;
				player.Name = bytes.ReadStringUntilNullTerminator(out length);

				bytes = bytes.RemoveFromStart(length + 1);

				player.Score = bytes.ReadInt();
				bytes = bytes.RemoveFromStart(4);

				player.Duration = bytes.ReadSingle();
				bytes = bytes.RemoveFromStart(4);

				Players.Add(player);
			}
			
			Debug.Assert(bytes.Length == 0);
		}
	}
}