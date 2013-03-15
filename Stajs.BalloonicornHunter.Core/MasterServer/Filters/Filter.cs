using System;
using System.Text;
using Stajs.BalloonicornHunter.Core.Extensions;

namespace Stajs.BalloonicornHunter.Core.MasterServer.Filters
{
	public class Filter
	{
		public Game? Game { get; set; }
		public bool? IsNotEmpty { get; set; }
		public bool? IsNotFull { get; set; }
		public string Map { get; set; }

		public override string ToString()
		{
			var sb = new StringBuilder();

			if (Game.HasValue)
				sb.Append(Game.GetDescription());

			if (IsNotEmpty.HasValue && IsNotEmpty.Value)
				sb.Append(@"\empty\1");

			if (IsNotFull.HasValue && IsNotFull.Value)
				sb.Append(@"\full\1");

			if (!string.IsNullOrWhiteSpace(Map))
				sb.Append(@"\map\").Append(Map);

			return sb.ToString();
		}
	}
}