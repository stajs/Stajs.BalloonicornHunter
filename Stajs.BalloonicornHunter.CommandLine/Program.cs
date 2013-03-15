using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Stajs.BalloonicornHunter.Core.MasterServer;
using Stajs.BalloonicornHunter.Core.Server;

namespace Stajs.BalloonicornHunter.CommandLine
{
	class Program
	{
		static void Main(string[] args)
		{
			var test = new PlayerResponse();
			return;

			//var masterServerQuery = new MasterServerQuery();
			//var servers = masterServerQuery.GetServers();

			//var serverQuery = new ServerQuery(null);

			//foreach (var server in servers)
			//{
			//	serverQuery = new ServerQuery(server);
			//	var info = serverQuery.GetInfo();
			//	if (info.Players > 0)
			//		break;
			//}

			//var challenge = serverQuery.GetChallenge();
			//var players = serverQuery.GetPlayers(challenge);

			Console.WriteLine("I'm ah gonna get you...");
			//Console.WriteLine(info.Name);
			Console.ReadKey();
		}
	}
}
