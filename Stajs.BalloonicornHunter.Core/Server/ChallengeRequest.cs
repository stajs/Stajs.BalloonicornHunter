using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stajs.BalloonicornHunter.Core.Server
{
	public class ChallengeRequest : IServerRequest
	{
		public byte[] ToBytes()
		{
			var request = new List<byte>();
			request.AddRange(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x55, 0xFF, 0xFF, 0xFF, 0xFF });

			return request.ToArray();
		}
	}
}