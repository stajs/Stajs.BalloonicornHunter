using System;
using Stajs.BalloonicornHunter.Core.MasterServer;

namespace Stajs.BalloonicornHunter.CommandLine
{
	class Program
	{
		static void Main(string[] args)
		{
			var query = new MasterServerQuery();
			query.GetServers();

			Console.WriteLine("I'm ah gonna get you...");
			Console.ReadKey();
		}
	}
}
