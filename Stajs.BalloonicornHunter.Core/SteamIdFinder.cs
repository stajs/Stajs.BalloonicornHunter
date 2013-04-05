﻿using System;
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
		public long? Get(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				return null;

			Debug.Print("Finding Steam ID for: {0}", name);

			var cache = new CacheContext();
			var player = cache.Players.SingleOrDefault(p => p.Name == name);

			if (player != null)
			{
				Debug.Print("\tFound in cache: {0}", player.SteamId);
				return player.SteamId;
			}

			// TODO: keep a static LastQueriedAt instead of a dumb Thread.Sleep()
			Debug.Print("\tSleeping");
			Thread.Sleep(TimeSpan.FromSeconds(3));

			Debug.Print("\tSearching");
			var searchUrl = string.Format("http://steamcommunity.com/actions/Search?T=Account&K=%22{0}%22", HttpUtility.UrlEncode(name));
			var dom = CQ.CreateFromUrl(searchUrl);
			var profileUrls = FindProfileUrls(dom);

			Debug.Print("\tProfile URL Count: {0}", profileUrls.Count);

			var steamId = Get(profileUrls);

			player = new Player
			{
				Name = name,
				SteamId = steamId,
				QueryCount = profileUrls.Count,
				QueriedAt = DateTime.Now
			};

			cache.Players.Add(player);
			cache.SaveChanges();

			return steamId;
		}

		internal long? Get(List<string> profileUrls)
		{
			if (profileUrls.Count != 1)
				return null;

			var url = profileUrls.Single();

			Debug.Print("\tProfile URL: {0}", url);
			
			const string urlMask = "http://steamcommunity.com/profiles/";

			if (!url.StartsWith(urlMask))
				return null; // TODO: resolve vanity URLs

			long id;
			if (!long.TryParse(url.Replace(urlMask, ""), out id))
				throw new HolyShitException("This URL be fricked up son! " + url);

			Debug.Print("\tSteam ID: {0}", id);

			return id;
		}

		internal List<string> FindProfileUrls(CQ dom)
		{
			return dom["div.resultItem a.linkTitle"]
				.Select(a => a["href"])
				.ToList();
		}
	}
}