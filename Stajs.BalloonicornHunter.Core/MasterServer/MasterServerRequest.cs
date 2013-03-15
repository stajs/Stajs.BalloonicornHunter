using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Stajs.BalloonicornHunter.Core.MasterServer
{
	public class MasterServerRequest : IServerRequest
	{
		public byte RequestType { get; private set; }
		public Region Region { get; private set; }
		public IPEndPoint StartServer { get; private set; }
		public string Filter { get; private set; }

		public MasterServerRequest() : this("0.0.0.0", 0)
		{
		}

		public MasterServerRequest(string ipAddress, int port) : this(new IPEndPoint(IPAddress.Parse(ipAddress), port))
		{
		}

		public MasterServerRequest(IPEndPoint startServer)
		{
			if (startServer == null)
				throw new ArgumentNullException("startServer", "Starting server can not be null.");

			RequestType = 0x31;
			StartServer = startServer;
			Region = Region.Australia;
			Filter = @"\gamedir\tf\empty\1"; // Debugging
		}

		public byte[] ToBytes()
		{
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

			bytes[i] = RequestType;
			i++;

			bytes[i] = (byte) Region;
			i++;

			ipAddress.CopyTo(bytes, i);
			i += ipAddress.Length;

			bytes[i] = nullTerminator;
			i++;

			if (filter.Any())
				filter.CopyTo(bytes, i);
			else
				bytes[i] = nullTerminator;

			return bytes;
		}
	}
}