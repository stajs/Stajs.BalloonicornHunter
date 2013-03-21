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
				Region = Region.Australia,
				Game = Game.TeamFortress2,
				Map = Map.Doomsday
			};

			var masterServerQuery = new MasterServerQuery();
			var servers = masterServerQuery.GetServers(filter);

			if (!servers.Any())
			{
				Console.WriteLine("Oh dear.");
				Console.ReadKey();
				return;
			}

			var serverQuery = new ServerQuery(servers.First());
			var info = serverQuery.GetInfo();
			var players = serverQuery.GetPlayers();

			Console.WriteLine("I'm ah gonna get you...");
			Console.WriteLine(info.Name);
			Console.WriteLine(info.Map);
			Console.ReadKey();
		}
	}
}
