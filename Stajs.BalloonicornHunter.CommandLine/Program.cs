using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
			var servers = masterServerQuery.GetServers(filter);

			if (!servers.Any())
			{
				Console.WriteLine("Oh dear.");
				Console.ReadKey();
				return;
			}

			var info = new List<string>();

			Debug.Print("Found {0} server(s)", servers.Count);

			foreach (var server in servers.Select((Value, Index) => new { Value, Index }))
			{
				Debug.Print("Pinging {0}", server.Value);
				var ping = server.Value.GetPing();
				
				if (!ping.HasValue)
				{
					Debug.Print("Could not contact server.");
					continue;
				}

				if (ping > 100)
				{
					Debug.Print("Ping too high!");
					continue;
				}

				var serverQuery = new ServerQuery(server.Value);
				var infoResponse = serverQuery.GetInfo();

				var signed = infoResponse.ExtraData.SignedServerSteamId;
				var unsigned = infoResponse.ExtraData.UnsignedServerSteamId;

				var playerResponse = serverQuery.GetPlayers();
				var serverInfo = string.Format("{0} | {1} | {2}/{3} players | {4}",
					infoResponse.Name,
					infoResponse.Map,
					playerResponse.PlayerCount,
					infoResponse.MaxPlayers,
					ping);

				Debug.Print("[{0}] {1}\n\t{2}", server.Index, serverInfo, string.Join("\n\t", playerResponse.Players));

				info.Add(serverInfo);
			}

			Console.WriteLine("I'm ah gonna get you...");
			Console.WriteLine(info.First());
			Console.ReadKey();
		}
	}
}
