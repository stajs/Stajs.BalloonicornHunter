using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CsQuery;
using Stajs.BalloonicornHunter.Core.Exceptions;

namespace Stajs.BalloonicornHunter.Core
{
	public class SteamIdFinder : ISteamIdFinder
	{
		private const string _urlMask = "http://steamcommunity.com/profiles/";

		public virtual long? Get(string name)
		{
			Debug.Print(name);
			var url = string.Format("http://steamcommunity.com/actions/Search?T=Account&K=%22{0}%22", HttpUtility.UrlEncode(name));
			return Get(CQ.CreateFromUrl(url));
		}

		internal long? Get(CQ dom)
		{
			var urls = FindUrls(dom);

			Debug.Print("\tCount: {0}", urls.Count);

			if (urls.Count != 1)
				return null;

			var url = urls.Single();

			Debug.Print("\t{0}", url);
			if (!url.StartsWith(_urlMask))
				return null; // TODO: resolve vanity URLs

			long id;
			if (!long.TryParse(url.Replace(_urlMask, ""), out id))
				throw new HolyShitException("This URL be fricked up son! " + url);

			Debug.Print("\t{0}", id);

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