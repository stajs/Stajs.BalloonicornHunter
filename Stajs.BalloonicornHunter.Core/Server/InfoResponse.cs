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
		public int Header { get; private set; }
		public string Header2 { get; private set; } //TODO
		public string Protocol { get; private set; }
		public string Name { get; private set; }
		public string Map { get; private set; }
		public string Folder { get; private set; }
		public string Game { get; private set; }

		public bool IsSplitResponse { get { return Header == -2; } }

		public InfoResponse(byte[] bytes)
		{
			Parse(bytes);
		}

		private void Parse(byte[] bytes)
		{
			Header = bytes.ReadInt();
			bytes = bytes.RemoveFromStart(4);

			Header2 = BitConverter.ToString(bytes, 0, 1);
			bytes = bytes.RemoveFromStart(1);

			Protocol = BitConverter.ToString(bytes, 0, 1);
			bytes = bytes.RemoveFromStart(1);

			Name = bytes.ReadString();
			bytes = bytes.RemoveFromStart(Name.Length + 1);

			Map = bytes.ReadString();
			bytes = bytes.RemoveFromStart(Map.Length + 1);

			Folder = bytes.ReadString();
			bytes = bytes.RemoveFromStart(Folder.Length + 1);

			Game = bytes.ReadString();
			bytes = bytes.RemoveFromStart(Game.Length + 1);
		}
	}
}
