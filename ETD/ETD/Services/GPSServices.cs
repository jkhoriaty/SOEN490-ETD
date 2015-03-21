using ETD.Models.Objects;
using ETD.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ETD.Services
{
	class GPSServices
	{
		private GPSStatusCallbacks caller;
		private DispatcherTimer dispatcherTimer = new DispatcherTimer();

		private static Dictionary<string, string> registeredVolunteers = new Dictionary<string, string>();

		public GPSServices(GPSStatusCallbacks newCaller)
		{
			caller = newCaller;

			//Start polling the server for GPS locations
			dispatcherTimer.Tick += new EventHandler(Refresh);
			dispatcherTimer.Interval += new TimeSpan(0, 0, 5);
			dispatcherTimer.Start();
		}

		private void Refresh(object sender, EventArgs e)
		{
			UpdateRegistered();
			UpdateGPSLocations();
		}

		//Pinging server for registered volunteers and interpret the return
		internal Task UpdateRegistered()
		{
			Task<String[]> UpdateRegisteredTask = new Task<string[]>(UpdateRegisteredRequest);
			UpdateRegisteredTask.ContinueWith(task => UpdateRegisteredResultAnalysis(task.Result));
			UpdateRegisteredTask.Start();
			return UpdateRegisteredTask;
		}

		//Actual command message to send to the server to request the list of RegisteredVolunteers
		private String[] UpdateRegisteredRequest()
		{
			return NetworkServices.ExecuteRequest("2~");
		}

		//Interpret the servers return for UpdateRegistered
		private void UpdateRegisteredResultAnalysis(String[] reply)
		{
			if (reply == null) //Connection failed
			{
				caller.NotifyConnectionFail();
			}
			else
			{
				caller.NotifyConnectionSuccess();
				int newCtr = 0;
				for (int i = 1; i < (reply.Length - 1); i++)
				{
					String[] volunteerInfo = reply[i].Split('|');
					if (!registeredVolunteers.ContainsKey(volunteerInfo[0]))
					{
						registeredVolunteers.Add(volunteerInfo[0], volunteerInfo[1]);
						newCtr++;
					}
					else
					{
						registeredVolunteers[volunteerInfo[0]] = volunteerInfo[1];
					}
				}
				//Dispatcher.Invoke(() => newRegisteredCTR.Content = "" + newCtr);
			}
		}

		//Updating all GPS locations with the most recent information
		internal void UpdateGPSLocations()
		{
			foreach (string id in registeredVolunteers.Keys.ToList())
			{
				Task<String[]> UpdateGPSLocationsTask = new Task<string[]>(() => RequestGPSCoordinates(id));
				UpdateGPSLocationsTask.ContinueWith(task => RequestGPSCoordinatesResultAnalysis(task.Result));
				UpdateGPSLocationsTask.Start();
			}
		}

		//Actual command message to send to the server to request the location of a volunteer
		private string[] RequestGPSCoordinates(string id)
		{
			return NetworkServices.ExecuteRequest("3~" + id + "~");
		}

		//Interpret the servers return
		private void RequestGPSCoordinatesResultAnalysis(string[] reply)
		{
			if(reply != null && reply[0].Equals("0")) //0 for id has a location
			{
				//Call have to be made on main thread that holds UI control
				Application.Current.Dispatcher.Invoke(() => GPSLocationHandling(reply));
			}
		}

		//Called to create or modify a gps location depending on the reply message
		private void GPSLocationHandling(string[] reply)
		{
			if (GPSLocation.getGPSLocationFromID(reply[1]) == null)
			{
				GPSLocation gpsLocation = new GPSLocation(reply[1], Double.Parse(reply[2]), Double.Parse(reply[3]));
				Team team = new Team("A"); //TODO remove
				team.setGPSLocation(gpsLocation); //TODO remove
			}
			else
			{
				GPSLocation.getGPSLocationFromID(reply[1]).setGPSCoordinates(Double.Parse(reply[2]), Double.Parse(reply[3]));
			}
		}

        internal static Dictionary<string, string> getUsers()
        {
            return registeredVolunteers;
        }
	}
}
