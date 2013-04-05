using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using CsQuery;
using Stajs.BalloonicornHunter.Core.Domain;
using Stajs.BalloonicornHunter.Core.Exceptions;

namespace Stajs.BalloonicornHunter.Core
{
	public class SteamIdFinder : ISteamIdFinder
	{
		private const string UrlMask = "http://steamcommunity.com/profiles/";

		public long? Get(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				return null;

			Debug.Print(name);
			var cache = new CacheContext();
			var player = cache.Players.SingleOrDefault(p => p.Name == name);

			if (player != null)
			{
				Debug.Print("\tFound in cache");
				return player.SteamId;
			}

			Debug.Print("\tSleeping");
			Thread.Sleep(TimeSpan.FromSeconds(3));
			Debug.Print("\tSearching");
			var url = string.Format("http://steamcommunity.com/actions/Search?T=Account&K=%22{0}%22", HttpUtility.UrlEncode(name));
			var steamId = Get(CQ.CreateFromUrl(url));

			player = new Player
			{
				Name = name,
				SteamId = steamId
			};

			cache.Players.Add(player);
			cache.SaveChanges();

			return steamId;
		}

		internal long? Get(CQ dom)
		{
			var urls = FindUrls(dom);

			Debug.Print("\tCount: {0}", urls.Count);

			if (urls.Count != 1)
				return null;

			var url = urls.Single();

			Debug.Print("\t{0}", url);
			if (!url.StartsWith(UrlMask))
				return null; // TODO: resolve vanity URLs

			long id;
			if (!long.TryParse(url.Replace(UrlMask, ""), out id))
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