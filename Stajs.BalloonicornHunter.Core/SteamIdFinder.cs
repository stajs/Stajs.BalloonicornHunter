using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsQuery;

namespace Stajs.BalloonicornHunter.Core
{
	public class SteamIdFinder
	{
		public Int64? Get(string name)
		{
			return Get(CQ.CreateFragment(""));
		}

		public Int64? Get(CQ dom)
		{
			return null;
		}

		internal List<string> FindUrls(CQ dom)
		{
			return dom["div.resultItem a.linkTitle"]
				.Select(a => a["href"])
				.ToList();
		}
	}
}