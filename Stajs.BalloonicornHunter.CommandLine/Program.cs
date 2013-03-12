using System;
using System.Linq;
using Stajs.BalloonicornHunter.Core.MasterServer;
using Stajs.BalloonicornHunter.Core.Server;

namespace Stajs.BalloonicornHunter.CommandLine
{
	class Program
	{
		static void Main(string[] args)
		{
			var masterServerQuery = new MasterServerQuery();
			var servers = masterServerQuery.GetServers();

			var serverQuery = new ServerQuery(servers.First());
			var info = serverQuery.GetInfo();

			Console.WriteLine("I'm ah gonna get you...");
			Console.WriteLine(info.Name);
			Console.ReadKey();
		}
	}
}
