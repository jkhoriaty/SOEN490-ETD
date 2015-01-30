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
	UDPCallbacks caller;
	String ip;
	int port;
	String request;

	public ServerHandshakeThread(UDPCallbacks caller, String ip, int port, String name)
	{
		this.caller = caller;
		this.ip = ip;
		this.port = port;
		request = "0~" + name + "~";
	}

	public void run()
	{
		boolean unsuccessful;
		int tries = 0;

		//Repeat until unsuccessful or for a max of 10 tries (~= 30 seconds)
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

				socket.setSoTimeout(3000);
				socket.receive(reply);
				socket.close();

				String[] replyMessage = (new String(buffer)).split("~");
				caller.HandshakeSucceeded(replyMessage[1]);
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
		while(unsuccessful && tries <= 10);

		//Callback to the login activity to notify of failure
		caller.HandshakeFailed();
	}
}
