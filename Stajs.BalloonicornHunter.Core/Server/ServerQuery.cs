using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Stajs.BalloonicornHunter.Core.Extensions;

namespace Stajs.BalloonicornHunter.Core.Server
{
	public class ServerQuery
	{
		private readonly IPEndPoint _server;

		public ServerQuery(IPEndPoint server)
		{
			_server = server;
		}

		public InfoResponse GetInfo()
		{
			var request = new InfoRequest();
			var udpClient = new UdpClient(_server.Address.ToString(), _server.Port);
			udpClient.Send(request);

			var remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
			var bytesReceived = udpClient.Receive(ref remoteIpEndPoint);

			var response = new InfoResponse(bytesReceived);

			return response;
		}
	}
}