using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynaCache;
using Ninject.Modules;
using Stajs.BalloonicornHunter.Core;

namespace Stajs.BalloonicornHunter.CommandLine
{
	public class StandardModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IDynaCacheService>().To<MemoryCacheService>();
			Bind<ISteamIdFinder>().To(Cacheable.CreateType<SteamIdFinder>());
		}
	}
}