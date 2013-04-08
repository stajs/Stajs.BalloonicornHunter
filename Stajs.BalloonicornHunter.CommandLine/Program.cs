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
			var filter = new Filter
			{
				Region = Region.Australia,
				Game = Game.TeamFortress2,
				HasPlayers = true,
				IsNotFull = true
			};

			var masterServerQuery = new MasterServerQuery();
			var servers = masterServerQuery
				.GetServers(filter)
				.Take(20)
				.ToList();

			Debug.Print("Servers Count: {0}", servers.Count);

			var limit = 0;

			for (var i = 0; i < servers.Count; i++)
			{
				var server = servers[i];
				var ping = server.GetPing();

				Debug.Print("Server {0}\t| {1}\t| {2}", i, ping, server);

				if (!ping.HasValue || ping > 50)
				{
					Debug.Print("Skipping, ping is: {0}", ping);
					continue;
				}
				
				var serverQuery = new ServerQuery(servers[i]);
				var serverInfo = serverQuery.GetServerInfo();
				var map = serverInfo.Map;
				
				if (!map.StartsWith("pl_"))
				{
					Debug.Print("Skipping, map is: {0}", map);
					continue;
				}

				if (limit++ > 1)
					break;

				var players = serverQuery.GetPlayers().Players;

				var msg = string.Format("{0} | {1} | {2}/{3} players",
					serverInfo.Name,
					map,
					players.Count,
					serverInfo.MaxPlayers);

				Debug.Print(msg);
				Console.WriteLine(msg);

				var steamIds = new List<long>();

				var finder = new SteamIdFinder();

				for (var index = 0; index < players.Count; index++)
				{
					var name = players[index].Name;
					var id = finder.GetSteamIdByName(name);

					Debug.Print("\tPlayer {0}\t| {1}\t| {2}", index, name, id);
					Console.WriteLine("\tPlayer {0}\t| {1}\t| {2}", index, name, id);

					if (id.HasValue)
						steamIds.Add(id.Value);
				}
			}

			Debug.WriteLine("\nSmash a key");
			Console.WriteLine("\nSmash a key");
			Console.ReadKey();
		}
	}
}