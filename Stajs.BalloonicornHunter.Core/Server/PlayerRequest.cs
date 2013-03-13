using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stajs.BalloonicornHunter.Core.Server
{
	public class PlayerRequest : IServerRequest
	{
		public byte[] ToBytes()
		{
			var request = new List<byte>();
			request.AddRange(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x55 });
			request.AddRange(BitConverter.GetBytes(-1L));

			return request.ToArray();
		}
	}
}