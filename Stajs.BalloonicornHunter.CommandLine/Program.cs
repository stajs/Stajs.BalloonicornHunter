using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Stajs.BalloonicornHunter.Core.MasterServer;
using Stajs.BalloonicornHunter.Core.MasterServer.Filters;
using Stajs.BalloonicornHunter.Core.Server;

namespace Stajs.BalloonicornHunter.CommandLine
{
	class Program
	{
		static void Main(string[] args)
		{
			var filter = new Filter
			{
				Game = Game.TeamFortress2,
				IsNotEmpty = true,
				Map = "pl_goldrush"
			};

			var masterServerQuery = new MasterServerQuery();
			var servers = masterServerQuery.GetServers(filter);

			var serverQuery = new ServerQuery(null);

			InfoResponse info;
			foreach (var server in servers)
			{
				serverQuery = new ServerQuery(server);
				info = serverQuery.GetInfo();
				if (info.Players > 0)
					break;
			}

			var challenge = serverQuery.GetChallenge();
			var players = serverQuery.GetPlayers(challenge);

			Console.WriteLine("I'm ah gonna get you...");
			//Console.WriteLine(info.Name);
			Console.ReadKey();
		}
	}
}
