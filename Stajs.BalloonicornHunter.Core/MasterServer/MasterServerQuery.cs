﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Stajs.BalloonicornHunter.Core.Extensions;

namespace Stajs.BalloonicornHunter.Core.MasterServer
{
	public class MasterServerQuery : IDisposable
	{
		private readonly IPEndPoint _server;
		private readonly Socket _socket;

		public MasterServerQuery()
		{
			//208.64.200.39:27011
			//208.64.200.65:27015
			//208.64.200.52:27011
			_server = new IPEndPoint(IPAddress.Parse("208.64.200.39"), 27011);
			_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
		}

		public List<string> GetServers()
		{
			var request = CreateRequest();
			using (var udpClient = new UdpClient("208.64.200.39", 27011))
			{
				var sent = udpClient.Send(request, request.Length);

				var remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

				var receiveBytes = udpClient.Receive(ref remoteIpEndPoint);
				var returnData = Encoding.UTF8.GetString(receiveBytes);

				Parse(receiveBytes);

			}

			return null; // WIP
		}

		private List<IPEndPoint> Parse(byte[] bytes)
		{
			// Header
			bytes = bytes.RemoveFromStart(6);
			// Debug
			var hex = BitConverter.ToString(bytes);

			var servers = new List<string>();
			
			while (bytes.Length > 0)
			{
				var sb = new StringBuilder();
				sb.AppendFormat("{0}.", bytes.ToIpAddressOctet());
				bytes = bytes.RemoveFromStart(1);
				hex = BitConverter.ToString(bytes);

				sb.AppendFormat("{0}.", bytes.ToIpAddressOctet());
				bytes = bytes.RemoveFromStart(1);
				hex = BitConverter.ToString(bytes);

				sb.AppendFormat("{0}.", bytes.ToIpAddressOctet());
				bytes = bytes.RemoveFromStart(1);
				hex = BitConverter.ToString(bytes);

				sb.AppendFormat("{0}:", bytes.ToIpAddressOctet());
				bytes = bytes.RemoveFromStart(1);
				hex = BitConverter.ToString(bytes);

				sb.Append(bytes.ToIpAddressPort());
				bytes = bytes.RemoveFromStart(2);
				hex = BitConverter.ToString(bytes);

				servers.Add(sb.ToString());
			}

			// Hacking!
			return null;
		}
		
		private byte[] CreateRequest()
		{
			// Not sure of format yet.
			var bytes = new byte[13];
			bytes[0] = 0x31;
			bytes[1] = 0xFF;
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

			return bytes;

			var request = new MasterServerRequest
			{
				IpAddress = "0.0.0.0:0"
			};

			return request.ToBytes();
		}

		private byte[] ReadFromSocket(int length)
		{
			var buffer = new byte[length];
			var position = 0;

			while (position < buffer.Length)
				position += _socket.Receive(buffer, position, buffer.Length - position, SocketFlags.None);

			return buffer;
		}

		void IDisposable.Dispose()
		{
			if (_socket.Connected)
				_socket.Close();
		}
	}
}