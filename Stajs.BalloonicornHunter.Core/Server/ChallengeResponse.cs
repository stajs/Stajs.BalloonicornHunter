using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stajs.BalloonicornHunter.Core.Extensions;

namespace Stajs.BalloonicornHunter.Core.Server
{
	public class ChallengeResponse
	{
		public byte[] Bytes { get; private set; }
		public string Hex { get; private set; }
		public string Base64 { get; private set; }

		public ResponseFormat ResponseFormat { get; private set; }
		public string Header { get; private set; }
		public int Token { get; private set; }

		public ChallengeResponse(byte[] bytes)
		{
			Bytes = bytes;
			Hex = BitConverter.ToString(bytes);
			Base64 = Convert.ToBase64String(bytes);
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

			Token = bytes.ReadInt();
			bytes = bytes.RemoveFromStart(4);

			Debug.Assert(!bytes.Any());
		}
	}
}