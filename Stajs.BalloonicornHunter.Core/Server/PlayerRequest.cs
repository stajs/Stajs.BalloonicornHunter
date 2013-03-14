using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stajs.BalloonicornHunter.Core.Server
{
	public class PlayerRequest : IServerRequest
	{
		private readonly int _token;

		public PlayerRequest() : this(-1)
		{
			
		}

		public PlayerRequest(int token)
		{
			_token = token;
		}

		public byte[] ToBytes()
		{
			var request = new List<byte>();
			request.AddRange(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x55 });
			request.AddRange(BitConverter.GetBytes(_token));

			return request.ToArray();
		}
	}
}