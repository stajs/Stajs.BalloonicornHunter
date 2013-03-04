using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Stajs.BalloonicornHunter.Core.Extensions
{
	internal static class ArrayExtensions
	{
		internal static int ReadInt(this byte[] bytes)
		{
			return BitConverter.ToInt32(bytes, 0);
		}

		internal static int ReadByte(this byte[] bytes)
		{
			return BitConverter.ToInt32(bytes, 0);
		}

		internal static string ReadString(this byte[] bytes)
		{
			const byte nullTerminator = 0x00;
			var subArray = bytes.TakeWhile(b => b != nullTerminator).ToArray();
			return Encoding.UTF8.GetString(subArray);
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