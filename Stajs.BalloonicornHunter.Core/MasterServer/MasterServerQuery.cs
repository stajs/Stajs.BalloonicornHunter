using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Stajs.BalloonicornHunter.Core.Extensions;

namespace Stajs.BalloonicornHunter.Core.MasterServer
{
	public class MasterServerQuery
	{
		private const int HeaderLength = 6;

		public List<IPEndPoint> GetServers()
		{
			// Master servers
			// 208.64.200.39:27011
			// 208.64.200.65:27015
			// 208.64.200.52:27011

			var request = CreateRequest();

			using (var udpClient = new UdpClient("208.64.200.39", 27011))
			{
				udpClient.Send(request, request.Length);

				var remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
				var response = udpClient.Receive(ref remoteIpEndPoint);

				return Parse(response);
			}
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

		private byte[] CreateRequest()
		{
			var request = new MasterServerRequest();
			return request.ToBytes();
		}
	}
}