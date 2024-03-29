package concordia_university_capstone.etd_v10;

import android.content.Intent;

import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.net.SocketTimeoutException;

/**
 * Created by JK on 2015-01-30.
 */
public class ServerHandshakeThread implements Runnable
{
	UDPHandshakeCallbacks caller;
	String ip;
	int port;
	String request;

	public ServerHandshakeThread(UDPHandshakeCallbacks caller, String ip, int port, String deviceID, String name)
	{
		this.caller = caller;
		this.ip = ip;
		this.port = port;
		request = "0~" + deviceID + "~" + name + "~";
	}

	public void run()
	{
		boolean unsuccessful;
		int tries = 0;

		//Repeat until unsuccessful or for a max of 5 tries (~= 10 seconds)
		do
		{
			unsuccessful = false;
			tries++;
			try
			{   //Send the request
				DatagramSocket socket = new DatagramSocket();

				byte[] buffer = request.getBytes();
				DatagramPacket request = new DatagramPacket(buffer, buffer.length, InetAddress.getByName(ip), port);

				socket.send(request);

				//Wait for a reply
				buffer = new byte[100];
				DatagramPacket reply = new DatagramPacket(buffer, buffer.length);

				socket.setSoTimeout(2000);
				socket.receive(reply);
				socket.close();

				String[] replyMessage = (new String(buffer)).split("~");
				switch(Integer.parseInt(replyMessage[0]))
				{
					case 0:
						caller.HandshakeSucceeded();
						break;
					case 1:
						caller.HandshakeFailed("The name that you entered is already in use, please write your full name instead.");
						break;
				}

				return;
			}
			catch(SocketTimeoutException e)
			{
				unsuccessful = true;
			}
			catch (Exception e)
			{
				e.printStackTrace();
			}
		}
		while(unsuccessful && tries <= 5);

		//Callback to the login activity to notify of failure
		caller.HandshakeFailed("Connection to server failed!\nPlease try again later.");
	}
}
