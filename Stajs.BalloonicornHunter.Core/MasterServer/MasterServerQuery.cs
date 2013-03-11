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
		public List<IPEndPoint> GetServers()
		{
			// Master servers
			// 208.64.200.39:27011
			// 208.64.200.65:27015
			// 208.64.200.52:27011

			var udpClient = new UdpClient("208.64.200.39", 27011);
			udpClient.Send(new MasterServerRequest());

			var remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
			var bytes = udpClient.Receive(ref remoteIpEndPoint);
			var response = new MasterServerResponse(bytes);

			return response.Servers;
		}
	}
}