﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ETD.Services
{
    //Summary
    //Used for connecting the desktop application to the server in order to receive
    //Information from the android application.

	class NetworkServices
	{
		static String serverIP = "24.202.7.147";
		static int serverPort = 2000;

		internal static String[] ExecuteRequest(String request)
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

					Task<String[]> replyListener = new Task<String[]>(() => ReceiveReply(socket));
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

		private static String[] ReceiveReply(UdpClient socket)
		{
			IPEndPoint remoteServer = new IPEndPoint(IPAddress.Any, 0);
			Byte[] buffer = socket.Receive(ref remoteServer);
			return Encoding.ASCII.GetString(buffer).Split('~');
		}
	}
}
