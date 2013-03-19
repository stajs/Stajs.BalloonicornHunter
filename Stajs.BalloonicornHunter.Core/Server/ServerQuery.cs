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

		private byte[] GetResponseBytes(IServerRequest request)
		{
			var udpClient = new UdpClient(_server.Address.ToString(), _server.Port);
			udpClient.Send(request);

			var remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
			var bytesReceived = udpClient.Receive(ref remoteIpEndPoint);

			return bytesReceived;
		}

		public InfoResponse GetInfo()
		{
			var bytesReceived = GetResponseBytes(new InfoRequest());
			var response = new InfoResponse(bytesReceived);

			return response;
		}

		public ChallengeResponse GetChallenge()
		{
			var bytesReceived = GetResponseBytes(new ChallengeRequest());
			var response = new ChallengeResponse(bytesReceived);

			return response;
		}

		public PlayerResponse GetPlayers()
		{
			var bytesReceived = GetResponseBytes(new PlayerRequest());
			var response = new PlayerResponse(bytesReceived);

			return response;
		}

		public PlayerResponse GetPlayers(ChallengeResponse challenge)
		{
			var bytesReceived = GetResponseBytes(new PlayerRequest(challenge.Token));
			var response = new PlayerResponse(bytesReceived);

			return response;
		}

		public override string ToString()
		{
			return _server.ToString();
		}
	}
}