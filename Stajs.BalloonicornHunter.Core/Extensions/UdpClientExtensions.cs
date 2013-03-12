using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Stajs.BalloonicornHunter.Core.MasterServer;
using Stajs.BalloonicornHunter.Core.Server;

namespace Stajs.BalloonicornHunter.Core.Extensions
{
	public static class UdpClientExtensions
	{
		public static int Send(this UdpClient udpClient, IServerRequest request)
		{
			var bytes = request.ToBytes();
			return udpClient.Send(bytes, bytes.Length);
		}
	}
}