using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
			InfoResponse response;

			using (var udpClient = new UdpClient(_server.Address.ToString(), _server.Port))
			{
				var bytesToSend = request.GetBytes();
				udpClient.Send(bytesToSend, bytesToSend.Length);

				var remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
				var bytesReceived = udpClient.Receive(ref remoteIpEndPoint);

				// Debugging
				var s = Encoding.UTF8.GetString(bytesReceived);

				response = new InfoResponse(bytesReceived);
			}

			return response;
		}
	}
}