using System;
using System.Diagnostics;
using System.Linq;

namespace Stajs.BalloonicornHunter.Core.Extensions
{
	internal static class ArrayExtensions
	{
		internal static int ToInt(this byte[] bytes)
		{
			return BitConverter.ToInt32(bytes, 0);
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