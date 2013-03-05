using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stajs.BalloonicornHunter.Core.Extensions;

namespace Stajs.BalloonicornHunter.Core.Server
{
	public class InfoResponse
	{
		public ResponseFormat ResponseFormat { get; private set; }
		public string Header { get; private set; }
		public string Protocol { get; private set; }
		public string Name { get; private set; }
		public string Map { get; private set; }
		public string Folder { get; private set; }
		public string Game { get; private set; }
		public short Id { get; private set; }
		public byte Players { get; private set; }
		public byte MaxPlayers { get; private set; }
		public byte Bots { get; private set; }
		public ServerType ServerType { get; private set; }
		public Environment Environment { get; private set; }
		public bool RequiresPassword { get; private set; }
		public bool IsVacProtected { get; private set; }
		public string Version { get; private set; }
		public byte ExtraData { get; private set; }

		public InfoResponse(byte[] bytes)
		{
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

			Protocol = bytes.ReadHex();
			bytes = bytes.RemoveFromStart(1);

			Name = bytes.ReadStringUntilNullTerminator();
			bytes = bytes.RemoveFromStart(Name.Length + 1);

			Map = bytes.ReadStringUntilNullTerminator();
			bytes = bytes.RemoveFromStart(Map.Length + 1);

			Folder = bytes.ReadStringUntilNullTerminator();
			bytes = bytes.RemoveFromStart(Folder.Length + 1);

			Game = bytes.ReadStringUntilNullTerminator();
			bytes = bytes.RemoveFromStart(Game.Length + 1);

			Id = bytes.ReadInt16();
			bytes = bytes.RemoveFromStart(2);

			Players = bytes[0];
			bytes = bytes.RemoveFromStart(1);

			MaxPlayers = bytes[0];
			bytes = bytes.RemoveFromStart(1);

			Bots = bytes[0];
			bytes = bytes.RemoveFromStart(1);

			ServerType = (ServerType) bytes[0];
			bytes = bytes.RemoveFromStart(1);

			Environment = (Environment) bytes[0];
			bytes = bytes.RemoveFromStart(1);

			RequiresPassword = bytes.ReadBoolean();
			bytes = bytes.RemoveFromStart(1);

			IsVacProtected = bytes.ReadBoolean();
			bytes = bytes.RemoveFromStart(1);

			// Ignore "The Ship"

			Version = bytes.ReadStringUntilNullTerminator();
			bytes = bytes.RemoveFromStart(Version.Length + 1);

			ExtraData = bytes[0];
			bytes = bytes.RemoveFromStart(1);

			// Ignore actual extra data
			// TODO
		}
	}
}
