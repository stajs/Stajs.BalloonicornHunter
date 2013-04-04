using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Ninject;
using Stajs.BalloonicornHunter.Core;
using Stajs.BalloonicornHunter.Core.Extensions;
using Stajs.BalloonicornHunter.Core.MasterServer;
using Stajs.BalloonicornHunter.Core.MasterServer.Filters;
using Stajs.BalloonicornHunter.Core.Server;

namespace Stajs.BalloonicornHunter.CommandLine
{
	class Program
	{
		static void Main(string[] args)
		{
			var finder = new SteamIdFinder();
			var x = finder.Get("");

			return;

			var filter = new Filter
			{
				Region = Region.Australia,
				Game = Game.TeamFortress2,
				HasPlayers = true,
				IsNotFull = true
			};

			var masterServerQuery = new MasterServerQuery();

			var server = masterServerQuery
				.GetServers(filter)
				.First();

			var ping = server.GetPing();
			var serverQuery = new ServerQuery(server);
			var info = serverQuery.GetInfo();
			var players = serverQuery.GetPlayers();

			Console.WriteLine("{0} | {1} | {2}/{3} players | {4} ms",
					info.Name,
					info.Map,
					players.PlayerCount,
					info.MaxPlayers,
					ping);

			// TODO: caching
			//var kernel = new StandardKernel(new StandardModule());

			//var steamIds = new List<long>();

			//var finder = kernel.Get<ISteamIdFinder>();

			//var player = players.Players.First();

			//foreach (var player in players.Players)
			//{
			//	var id = finder.Get(player.Name);

			//	Console.WriteLine("{0} | {1}", player.Name, id);

			//	// Be a good citizen, don't spam too hard
			//	Thread.Sleep(TimeSpan.FromSeconds(3));

			//	if (id.HasValue)
			//		steamIds.Add(id.Value);
			//}

			Console.ReadKey();
		}
	}
}
