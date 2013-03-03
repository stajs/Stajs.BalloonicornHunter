using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stajs.BalloonicornHunter.Core.MasterServer
{
	public class MasterServerQuery
	{
		public List<string> GetServers()
		{
			var messageType = Convert.ToByte("1");
			var region = Region.Australia;

			var ip = "0.0.0.0:0";

			return null;
		}
	}
}