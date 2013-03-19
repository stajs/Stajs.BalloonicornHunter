using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Stajs.BalloonicornHunter.Core.Extensions;
using Stajs.BalloonicornHunter.Core.MasterServer.Filters;

namespace Stajs.BalloonicornHunter.Core.MasterServer
{
	public class MasterServerQuery
	{
		// Master servers
		// 208.64.200.39:27011
		// 208.64.200.65:27015
		// 208.64.200.52:27011

		private IPEndPoint _masterServer { get; set; }

		public MasterServerQuery() : this("208.64.200.39", 27011) { }
		
		public MasterServerQuery(string host, int port) : this(new IPEndPoint(IPAddress.Parse(host), port)) { }
		
		public MasterServerQuery(IPEndPoint masterServer)
		{
			_masterServer = masterServer;
		}

		public List<IPEndPoint> GetServers(Filter filter)
		{
			var udpClient = new UdpClient(_masterServer.Address.ToString(), _masterServer.Port);
			udpClient.Send(new MasterServerRequest(filter));

			var remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
			var bytes = udpClient.Receive(ref remoteIpEndPoint);
			var response = new MasterServerResponse(bytes);

			return response.Servers;
		}

		public override string ToString()
		{
			return _masterServer.ToString();
		}
	}
}