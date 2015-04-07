import java.net.DatagramSocket;
import java.net.DatagramPacket;
import java.util.HashMap;
import java.util.Map;

public class ETD
{
	static HashMap<String, String> senders = new HashMap<String, String>();
	static HashMap<String, String[]> locations = new HashMap<String, String[]>();

	public static void main(String[] args)
	{
		DatagramSocket socket = null;
		try
		{
			socket = new DatagramSocket(2000);
		}
		catch(Exception e)
		{
			System.out.println("Socket creation error");
			e.printStackTrace();
		}

		try
		{
			int ctr = 0; //Used for testing only, need to be erased for actual operations
			while(true)
			{
				byte[] buffer = new byte[100];
				DatagramPacket packet = new DatagramPacket(buffer, buffer.length);

				socket.receive(packet);

				System.out.println("Request: " + new String(buffer));

				String[] requestMessage = (new String(buffer)).split("~");
				String reply = null;
				switch(Integer.parseInt(requestMessage[0]))
				{
					case 0: //Android - Volunteer registering
						if(!senders.containsValue(requestMessage[1]))
						{
							senders.put(requestMessage[1], requestMessage[2]);
							reply = "0~";
						}
						else
						{
							reply = "1~";
						}
						break;
					case 1: //Android - Volunteer location update
						String[] LatLon = {requestMessage[2], requestMessage[3]};
						locations.put(requestMessage[1], LatLon);
						break;
					case 2: //ETD request registered volunteers
						if(senders.size() == 0)
						{
							reply = "1~";
						}
						else
						{
							reply = "0~";
							for(Map.Entry<String, String> entry : senders.entrySet())
							{
								reply += entry.getKey() + "|" + entry.getValue() + "~";
							}
						}
						break;
					case 3: //GPS coordinate request
						//String[] currentLatLon = locations.get(requestMessage[1]);
						//reply = "0~" + requestMessage[1] + "~" + currentLatLon[0] + "~" + currentLatLon[1] + "~";
					
						//Proof of concept, hard coded, here for testing purposes.
						//For actual operation, delete this switch statement and everything it contains, as well as comment out the previous 2 lines.
						switch(ctr)
						{
							case 0:
								reply = "0~" + requestMessage[1] + "~45.555682~-73.552468~bottomLeftCornerOfSector800~";
								ctr++;
								break;
							case 1:
								reply = "0~" + requestMessage[1] + "~45.556152~-73.551180~dome~";
								ctr++;
								break;
							case 2:
								reply = "0~" + requestMessage[1] + "~45.554405~-73.552093~smallStageArea~";
								ctr++;
								break;
							case 3:
								reply = "0~" + requestMessage[1] + "~45.554682~-73.554372~pieIXSerbrook~";
								ctr++;
								break;
							case 4:
								reply = "0~" + requestMessage[1] + "~45.556575~-73.552686~stairsRight~";
								ctr = 0;
								break;
						}
						break;
				}

				if(reply != null)
				{
					System.out.println("Reply: " + reply);

					buffer = reply.getBytes();
					DatagramPacket replyPacket = new DatagramPacket(buffer, buffer.length, packet.getAddress(), packet.getPort());

					socket.send(replyPacket);
				}
				
			}
		}
		catch(Exception e)
		{
			System.out.println("Reception error");
			e.printStackTrace();
		}
	}
}