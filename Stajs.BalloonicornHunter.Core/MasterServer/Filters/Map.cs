using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stajs.BalloonicornHunter.Core.MasterServer.Filters
{
	public enum Map
	{
		[Description(@"\map\pl_goldrush")]
		GoldRush,

		[Description(@"\map\sd_doomsday")]
		Doomsday,

		[Description(@"\map\ctf_turbine")]
		Turbine
	}
}