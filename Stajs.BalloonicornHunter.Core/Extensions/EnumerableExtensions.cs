using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace Stajs.BalloonicornHunter.Core.Extensions
{
	internal static class EnumerableExtensions
	{
		internal static IPEndPoint ToIPEndPoint(this IEnumerable<byte> bytes)
		{
			var array = bytes.ToArray();
			Debug.Assert(array.Length == 6);

			var ipAddressArray = array
				.Take(4);

			var portArray = array
				.Skip(4)
				.Take(2);

			var ipAddress = ipAddressArray.ToIpAddress();
			var port = portArray.ToPort();
			
			return new IPEndPoint(ipAddress, port);
		}

		private static IPAddress ToIpAddress(this IEnumerable<byte> bytes)
		{
			var array = bytes.ToArray();
			Debug.Assert(array.Length == 4);
			return new IPAddress(array);
		}

		private static int ToPort(this IEnumerable<byte> bytes)
		{
			var array = bytes.ToArray();
			Debug.Assert(array.Length == 2);
			return (array[0] << 8) | array[1];
		}
	}
}