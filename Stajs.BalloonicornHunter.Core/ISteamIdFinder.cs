using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stajs.BalloonicornHunter.Core
{
	public interface ISteamIdFinder
	{
		long? Get(string name);
	}
}