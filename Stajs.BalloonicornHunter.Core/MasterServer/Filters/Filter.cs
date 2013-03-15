using System;
using System.Text;
using Stajs.BalloonicornHunter.Core.Extensions;

namespace Stajs.BalloonicornHunter.Core.MasterServer.Filters
{
	public class Filter
	{
		public Game? Game { get; set; }
		public bool? HasPlayers { get; set; }
		public bool? IsNotFull { get; set; }
		public bool? IsVacProtected { get; set; }
		public Map? Map { get; set; }

		public override string ToString()
		{
			var sb = new StringBuilder();

			if (Game.HasValue)
				sb.Append(Game.GetDescription());

			if (HasPlayers.HasValue)
				if (HasPlayers.Value)
					sb.Append(@"\empty\1");
				else
					sb.Append(@"\noplayers\1");

			if (IsNotFull.HasValue && IsNotFull.Value)
				sb.Append(@"\full\1");

			if (IsVacProtected.HasValue && IsVacProtected.Value)
				sb.Append(@"\secure\1");
			
			if (Map.HasValue)
				sb.Append(Map.GetDescription());

			return sb.ToString();
		}
	}
}