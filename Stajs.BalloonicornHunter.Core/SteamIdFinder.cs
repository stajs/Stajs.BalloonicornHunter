using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsQuery;
using Stajs.BalloonicornHunter.Core.Exceptions;

namespace Stajs.BalloonicornHunter.Core
{
	public class SteamIdFinder
	{
		private const string _urlMask = "http://steamcommunity.com/profiles/";

		public long? Get(string name)
		{
			return Get(CQ.CreateFragment(""));
		}

		public long? Get(CQ dom)
		{
			var urls = FindUrls(dom);

			if (urls.Count != 1)
				return null;

			var url = urls.Single();

			if (!url.StartsWith(_urlMask))
				return null; // TODO: resolve vanity URLs

			long id;
			if (!long.TryParse(url.Replace(_urlMask, ""), out id))
				throw new HolyShitException("This URL be fricked up son! " + url);

			return id;
		}

		internal List<string> FindUrls(CQ dom)
		{
			return dom["div.resultItem a.linkTitle"]
				.Select(a => a["href"])
				.ToList();
		}
	}
}