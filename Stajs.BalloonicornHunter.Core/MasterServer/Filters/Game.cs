using System.ComponentModel;

namespace Stajs.BalloonicornHunter.Core.MasterServer.Filters
{
	public enum Game
	{
		[Description(@"\gamedir\tf")]
		TeamFortress2,

		[Description(@"\gamedir\cstrike")]
		CounterStrikeSource
	}
}