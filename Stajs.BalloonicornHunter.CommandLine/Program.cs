using System;
using System.Collections.Generic;
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
				HasPlayers = true,
				IsNotFull = true,
				IsVacProtected = true,
				Map = Map.Doomsday
			};

			var masterServerQuery = new MasterServerQuery();
			var servers = masterServerQuery.GetServers(filter);

			filter.Map = Map.GoldRush;
			servers.AddRange(masterServerQuery.GetServers(filter));

			filter.Map = Map.Turbine;
			servers.AddRange(masterServerQuery.GetServers(filter));
			
			if (!servers.Any())
			{
				Console.WriteLine("Oh dear.");
				Console.ReadKey();
				return;
			}

			var info = new List<string>();

			foreach (var server in servers)
			{
				var serverQuery = new ServerQuery(server);
				var infoResponse = serverQuery.GetInfo();
				var playerResponse = serverQuery.GetPlayers();

				info.Add(string.Format("Server: {0} | Map: {1} | # Players: {2}", infoResponse.Name, infoResponse.Map, playerResponse.PlayerCount));
			}

			Console.WriteLine("I'm ah gonna get you...");
			Console.WriteLine(info.First());
			Console.ReadKey();
		}
	}
}
