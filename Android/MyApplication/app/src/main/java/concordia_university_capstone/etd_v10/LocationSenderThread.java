package concordia_university_capstone.etd_v10;

import android.location.Location;

import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;

/**
 * Created by JK on 2015-01-30.
 */
public class LocationSenderThread implements Runnable
{
	String ip;
	int port;

	String request;

	public LocationSenderThread(String ip, int port, String senderID, Location location)
	{
		this.ip = ip;
		this.port = port;

		request = "1~" + senderID + "~" + location.getLatitude() + "~" + location.getLongitude() + "~";
	}

	public void run()
	{
		try
		{
			DatagramSocket socket = new DatagramSocket();

			byte[] buffer = request.getBytes();
			DatagramPacket packet = new DatagramPacket(buffer, buffer.length, InetAddress.getByName(ip), port);

			socket.send(packet);

			socket.close();
		}
		catch(Exception e)
		{
			e.printStackTrace();
		}
	}
}
