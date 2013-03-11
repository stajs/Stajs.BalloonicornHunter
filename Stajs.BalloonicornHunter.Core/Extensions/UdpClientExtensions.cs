using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Stajs.BalloonicornHunter.Core.MasterServer;

namespace Stajs.BalloonicornHunter.Core.Extensions
{
	public static class UdpClientExtensions
	{
		public static int Send(this UdpClient udpClient, MasterServerRequest request)
		{
			return udpClient.Send(request.ToBytes(), request.ToBytes().Length);
		}
	}
}