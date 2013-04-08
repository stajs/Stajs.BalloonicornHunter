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

			var server = masterServerQuery
				.GetServers(filter)
				.First();

			var ping = server.GetPing();
			var serverQuery = new ServerQuery(server);
			var info = serverQuery.GetInfo();
			var players = serverQuery.GetPlayers();

			var msg = string.Format("{0} | {1} | {2}/{3} players | {4} ms",
					info.Name,
					info.Map,
					players.PlayerCount,
					info.MaxPlayers,
					ping);

			Debug.Print(msg);
			Console.WriteLine(msg);

			var steamIds = new List<long>();

			var finder = new SteamIdFinder();

			foreach (var player in players.Players.Select((v, i) => new { Index = i, Value = v }))
			{
				var name = player.Value.Name;

				var id = finder.GetSteamIdByName(name);

				Console.WriteLine("\t{0}\t| {1}\t| {2}", player.Index, name, id);

				if (id.HasValue)
					steamIds.Add(id.Value);
			}

			Debug.WriteLine("\nSmash a key");
			Console.WriteLine("\nSmash a key");
			Console.ReadKey();
		}
	}
}