using System;
using System.Linq;

namespace Stajs.BalloonicornHunter.Core.Extensions
{
	internal static class ArrayExtensions
	{
		internal static int ToInt(this byte[] bytes)
		{
			return BitConverter.ToInt32(bytes, 0);
		}

		internal static int ToIpAddressOctet(this byte[] bytes)
		{
			return Convert.ToByte(bytes[0]);
		}

		internal static int ToIpAddressPort(this byte[] bytes)
		{
			// Eeish...
			var hex = BitConverter.ToString(bytes, 0, 2).Replace("-", "");
			int i1 = bytes[0] | (bytes[1] << 8);
			int i2 = (bytes[0] << 8) | bytes[1];
			return i2;
			//return BitConverter.ToUInt16(bytes, 0);
		}

		internal static byte[] RemoveFromStart(this byte[] bytes, int count)
		{
			if (count > bytes.Length)
				return bytes;

			return bytes
				.Skip(count)
				.ToArray();
		}

		internal static byte[] RemoveFromEnd(this byte[] bytes, int count)
		{
			bytes = bytes
				.Reverse()
				.ToArray();

			bytes = bytes.RemoveFromStart(count);

			return bytes
				.Reverse()
				.ToArray();
		}
	}
}