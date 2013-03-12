using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stajs.BalloonicornHunter.Core.Server
{
	public class InfoRequest : IServerRequest
	{
		public byte[] ToBytes()
		{
			var request = new List<byte>();
			request.AddRange(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x54 });
			request.AddRange(Encoding.UTF8.GetBytes("Source Engine Query"));
			request.Add(0x00);

			return request.ToArray();
		}
	}
}