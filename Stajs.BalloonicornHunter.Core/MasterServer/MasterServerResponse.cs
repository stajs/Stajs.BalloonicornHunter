using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Stajs.BalloonicornHunter.Core.Extensions;

namespace Stajs.BalloonicornHunter.Core.MasterServer
{
	public class MasterServerResponse
	{
		private const int HeaderLength = 6;

		public byte[] Bytes { get; private set; }
		public List<IPEndPoint> Servers { get; private set; }

		public MasterServerResponse(byte[] bytes)
		{
			Bytes = bytes;
			Servers = Parse(bytes);
		}

		private List<IPEndPoint> Parse(byte[] bytes)
		{
			const int ipEndPointLength = 6;

			VerifyHeader(bytes);
			bytes = bytes.RemoveFromStart(HeaderLength);

			var servers = new List<IPEndPoint>();

			while (bytes.Length > 0)
			{
				var server = bytes.Take(ipEndPointLength).ToIPEndPoint();
				servers.Add(server);
				bytes = bytes.RemoveFromStart(ipEndPointLength);
			}

			return servers;
		}

		private void VerifyHeader(IEnumerable<byte> bytes)
		{
			const string expectedHeader = "FF-FF-FF-FF-66-0A";
			var header = BitConverter.ToString(bytes.Take(HeaderLength).ToArray());
			Debug.Assert(header == expectedHeader);
		}
	}
}