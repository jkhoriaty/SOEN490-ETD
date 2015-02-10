using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ETD.Services
{
	class NetworkServices
	{
		static String serverIP = "24.202.7.147";
		static int serverPort = 2000;

		private static String[] execute(String request)
		{
			UdpClient socket = new UdpClient();

			bool unsuccessful;
			int tries = 0;
			do
			{
				unsuccessful = false;
				tries++;
				try
				{
					Byte[] buffer = Encoding.ASCII.GetBytes(request);
					socket.Send(buffer, buffer.Length, serverIP, serverPort);

					Task<String[]> replyListener = new Task<String[]>(() => receiveReply(socket));
					replyListener.Start();

					if (replyListener.Wait(TimeSpan.FromSeconds(2)))
					{
						return replyListener.Result;
					}
					else
					{
						unsuccessful = true;
					}

				}
				catch (Exception e)
				{
					unsuccessful = true;
				}
			}
			while (unsuccessful && tries < 5);
			return null;
		}

		private static String[] receiveReply(UdpClient socket)
		{
			IPEndPoint remoteServer = new IPEndPoint(IPAddress.Any, 0);
			Byte[] buffer = socket.Receive(ref remoteServer);
			return Encoding.ASCII.GetString(buffer).Split('~');
		}

		public static String[] UpdateRegisted()
		{
			return execute("2~");
		}
	}
}
