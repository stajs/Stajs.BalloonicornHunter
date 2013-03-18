using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Stajs.BalloonicornHunter.Core.Exceptions;
using Stajs.BalloonicornHunter.Core.Extensions;

namespace Stajs.BalloonicornHunter.Core.MasterServer
{
	public class MasterServerResponse
	{
		public RawResponse RawResponse { get; private set; }

		public List<IPEndPoint> Servers { get; private set; }

		public MasterServerResponse(byte[] bytes)
		{
			RawResponse = new RawResponse(bytes);
			Servers = Parse(bytes);
		}

		private List<IPEndPoint> Parse(byte[] bytes)
		{
			const int headerLength = 6;
			var expectedHeader = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x66, 0x0A };
			var header = bytes.Take(headerLength).ToArray();

			if (!header.SequenceEqual(expectedHeader))
				throw new ResponseHeaderException();
			
			bytes = bytes.RemoveFromStart(headerLength);

			const int ipEndPointLength = 6;

			var servers = new List<IPEndPoint>();

			while (bytes.Length > 0)
			{
				var server = bytes.Take(ipEndPointLength).ToIPEndPoint();
				servers.Add(server);
				bytes = bytes.RemoveFromStart(ipEndPointLength);
			}

			return servers;
		}
	}
}