using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Stajs.BalloonicornHunter.Core.MasterServer
{
	public class MasterServerRequest
	{
		public byte Type { get { return 0x31; } }
		public byte Region { get { return 0x05; } } // Australia
		public IPEndPoint StartServer { get; private set; }
		public string Filter { get; set; }

		public MasterServerRequest() : this("0.0.0.0", 0)
		{
		}

		public MasterServerRequest(string ipAddress, int port) : this(new IPEndPoint(IPAddress.Parse(ipAddress), port))
		{
		}

		public MasterServerRequest(IPEndPoint startServer)
		{
			Debug.Assert(startServer != null);
			StartServer = startServer;
		}

		public byte[] ToBytes()
		{
			/* Example
				var bytes = new byte[13];
				bytes[0] = 0x31;
				bytes[1] = 0x05;
				bytes[2] = 0x30;
				bytes[3] = 0x2E;
				bytes[4] = 0x30;
				bytes[5] = 0x2E;
				bytes[6] = 0x30;
				bytes[7] = 0x2E;
				bytes[8] = 0x30;
				bytes[9] = 0x3A;
				bytes[10] = 0x30;
				bytes[11] = 0x00;
				bytes[12] = 0x00;
			 */

			const int typeLength = 1;
			const int regionLength = 1;
			const int nullTerminatorLength = 1;
			const byte nullTerminator = 0x00;

			var utf = new UTF8Encoding();

			var ipAddress = utf.GetBytes(StartServer.ToString());
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

			ipAddress.CopyTo(bytes, i);
			i += ipAddress.Length;

			bytes[i] = nullTerminator;
			i++;

			if (filter.Any())
			{
				filter.CopyTo(bytes, i);
				i++;
			}

			bytes[i] = nullTerminator;

			return bytes;
		}
	}
}