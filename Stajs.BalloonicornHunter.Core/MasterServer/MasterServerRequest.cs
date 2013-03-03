using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stajs.BalloonicornHunter.Core.MasterServer
{
	public class MasterServerRequest
	{
		public byte Type { get { return Convert.ToByte("1"); } }
		public byte Region { get { return Convert.ToByte("5"); } } // Australia
		public string IpAddress { get; set; }
		public string Filter { get; set; }

		public byte[] ToBytes()
		{
			const int typeLength = 1;
			const int regionLength = 1;
			const int nullTerminatorLength = 1;

			var utf = new UTF8Encoding();

			var ipAddress = utf.GetBytes(IpAddress ?? string.Empty);
			var filter = utf.GetBytes(Filter ?? string.Empty);

			var totalLength = typeLength
				+ regionLength
				+ ipAddress.Length
				+ nullTerminatorLength
				+ filter.Length
				+ nullTerminatorLength;

			var bytes = new byte[totalLength];
			var i = 0;

			bytes[i] = Type;
			i++;

			bytes[i] = Region;
			i++;

			if (ipAddress.Any())
			{
				ipAddress.CopyTo(bytes, i);
				i += ipAddress.Length;
			}

			bytes[i] = 0;
			i++;

			if (filter.Any())
			{
				filter.CopyTo(bytes, i);
				i++;
			}

			bytes[i] = 0;

			return bytes;
		}
	}
}