using ETD.CustomObjects.CustomUIObjects;
using ETD.Models.Objects;
using ETD.Services.Interfaces;
using ETD.ViewsPresenters.MapSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ETD.Services
{
	class GPSServices
	{
		private static GPSStatusCallbacks gpsStatusCallbacks;
		private static DispatcherTimer dispatcherTimer = new DispatcherTimer();
		private static MapSectionPage mapSection;
		internal static bool connectedToServer = false;
		internal static bool setupOngoing = false;

		private static Dictionary<string, string> registeredVolunteers = new Dictionary<string, string>();
        private static Dictionary<string, int> connectionStatus = new Dictionary<string,int> ();
        private static Dictionary<string, int> timeMIA = new Dictionary<string,int> ();

		internal static void StartServices(GPSStatusCallbacks newCaller)
		{
			gpsStatusCallbacks = newCaller;

			//Start polling the server for GPS locations
			dispatcherTimer.Tick += new EventHandler(Refresh);
			dispatcherTimer.Interval += new TimeSpan(0, 0, 5);
			dispatcherTimer.Start();
		}

		private static void Refresh(object sender, EventArgs e)
		{
			UpdateRegistered();
			UpdateGPSLocations();
		}

		//Pinging server for registered volunteers and interpret the return
		internal static Task UpdateRegistered()
		{
			Task<String[]> UpdateRegisteredTask = new Task<string[]>(UpdateRegisteredRequest);
			UpdateRegisteredTask.ContinueWith(task => UpdateRegisteredResultAnalysis(task.Result));
			UpdateRegisteredTask.Start();
			return UpdateRegisteredTask;
		}

		//Actual command message to send to the server to request the list of RegisteredVolunteers
		private static String[] UpdateRegisteredRequest()
		{
			return NetworkServices.ExecuteRequest("2~");
		}

		//Interpret the servers return for UpdateRegistered
		private static void UpdateRegisteredResultAnalysis(String[] reply)
		{
			if (reply == null) //Connection failed
			{
				gpsStatusCallbacks.NotifyConnectionFail();
				connectedToServer = false;
			}
			else
			{
				gpsStatusCallbacks.NotifyConnectionSuccess();
				connectedToServer = true;
				int newCtr = 0;
				for (int i = 1; i < (reply.Length - 1); i++)
				{
					String[] volunteerInfo = reply[i].Split('|');
					if (!registeredVolunteers.ContainsKey(volunteerInfo[0]))
					{
						registeredVolunteers.Add(volunteerInfo[0], volunteerInfo[1]);
                        connectionStatus.Add(volunteerInfo[0], 1);
                        timeMIA.Add(volunteerInfo[0], 0);
						newCtr++;
					}
					else
					{
                        if (registeredVolunteers[volunteerInfo[0]] == volunteerInfo[1])
                        {
                            timeMIA[volunteerInfo[0]]++;
                            if (timeMIA[volunteerInfo[0]] <= 3)
                            {
                                connectionStatus[volunteerInfo[0]] = 0; //Connection status unknown (failed connection but waiting for retry before setting it as not connected)
                            }
                            else
                            {
                                connectionStatus[volunteerInfo[0]] = -1; //Not connected
                            }
                        }
                        else
                        {
                            timeMIA[volunteerInfo[0]] = 0;
                            connectionStatus[volunteerInfo[0]] = 1; //Connected
                            registeredVolunteers[volunteerInfo[0]] = volunteerInfo[1]; //Updating location
                        }
					}
				}
				//Dispatcher.Invoke(() => newRegisteredCTR.Content = "" + newCtr);
			}
		}

		//Updating all GPS locations with the most recent information
		internal static void UpdateGPSLocations()
		{
			foreach (string id in registeredVolunteers.Keys.ToList())
			{
				Task<String[]> UpdateGPSLocationsTask = new Task<string[]>(() => RequestGPSCoordinates(id));
				UpdateGPSLocationsTask.ContinueWith(task => RequestGPSCoordinatesResultAnalysis(task.Result));
				UpdateGPSLocationsTask.Start();
			}
		}

		//Actual command message to send to the server to request the location of a volunteer
		private static string[] RequestGPSCoordinates(string id)
		{
			return NetworkServices.ExecuteRequest("3~" + id + "~");
		}

		//Interpret the servers return
		private static void RequestGPSCoordinatesResultAnalysis(string[] reply)
		{
			if(reply != null && reply[0].Equals("0")) //0 for id has a location
			{
				//Call have to be made on main thread that holds UI control
				Application.Current.Dispatcher.Invoke(() => GPSLocationHandling(reply));
			}
		}

		//Called to create or modify a gps location depending on the reply message
		private static void GPSLocationHandling(string[] reply)
		{
			if (GPSLocation.getGPSLocationFromID(reply[1]) == null)
			{
				GPSLocation gpsLocation = new GPSLocation(reply[1], Double.Parse(reply[2]), Double.Parse(reply[3]));
			}
			else
			{
				GPSLocation.getGPSLocationFromID(reply[1]).setGPSCoordinates(Double.Parse(reply[2]), Double.Parse(reply[3]));
			}
		}

		//Getter for list of registered volunteers
        internal static Dictionary<string, string> getUsers()
        {
            return registeredVolunteers;
        }



		//Setting up translation of GPS coordinates (lattitude and longitude) to Map coordinates (x, y)
		internal static void SetupGPSToMapTranslation_Start(MapSectionPage mapSectionInstance, List<Team> registeredTeams)
		{
			setupOngoing = true;

			MessageBox.Show("Entering setup, please refrain from creating new teams, equipements and interventions while in the setup phase (while the GPS setup button is red).\n"
				+ "Click on the GPS setup button to exit setup, if normal operation of the software is required.");

			mapSection = mapSectionInstance;

			//Get the TeamPins of the registered teams
			List<TeamPin> registeredTeamPins = new List<TeamPin>();
			List<Pin> pinList = Pin.getPinList();
			foreach (Pin pin in pinList)
			{
				if (registeredTeams.Contains(pin.getRelatedObject()))
				{
					registeredTeamPins.Add((TeamPin)pin);
				}
			}

			//Hiding all pins except the pins of registered teams, change their context menu for team selection
			Pin.ClearAllPins(mapSection.Canvas_map);
			foreach (TeamPin teamPin in registeredTeamPins)
			{
				MenuItem menuItem = new MenuItem();
				menuItem.Header = "Use this team for setup";
				menuItem.Click += ChooseTeamForSetup_Click;

				ContextMenu contextMenu = new ContextMenu();
				contextMenu.Items.Add(menuItem);
				teamPin.ContextMenu = contextMenu;

				mapSection.Canvas_map.Children.Add(teamPin);
				double[] previousPinPosition = Pin.getPreviousPinPosition(teamPin.getRelatedObject());
				if (previousPinPosition == null)
				{
					previousPinPosition = new double[] { teamPin.Width / 2, teamPin.Height / 2 }; //Top-left corner
				}
				teamPin.setPinPosition(previousPinPosition[0], previousPinPosition[1]);
			}

			MessageBox.Show("From the teams that remain on the map, select one of them that will be used for the setup by right-clicking on the team and selecting \"Use this team for setup\".");
		}

		//Called when team is picked for setup, prepare team pin for setup
		private static void ChooseTeamForSetup_Click(object sender, RoutedEventArgs e)
		{
			MenuItem mi = (MenuItem)sender;
			ContextMenu cm = (ContextMenu)mi.Parent;
			TeamPin teamPin = (TeamPin)cm.PlacementTarget;

			//Hiding all pins except the pin of the selected team, change its context menu for point tagging
			Pin.ClearAllPins(mapSection.Canvas_map);

			//Customizing the context menu for the exact use needed
			MenuItem tagPointMenuItem = new MenuItem();
			tagPointMenuItem.Header = "Tag point";
			tagPointMenuItem.Click += TagPoint_Click;

			ContextMenu contextMenu = new ContextMenu();
			contextMenu.Items.Add(tagPointMenuItem);
			teamPin.ContextMenu = contextMenu;

			//Replacing pin on the map for tagging
			mapSection.Canvas_map.Children.Add(teamPin);
			double[] previousPinPosition = Pin.getPreviousPinPosition(teamPin.getRelatedObject());
			if (previousPinPosition == null)
			{
				previousPinPosition = new double[] { teamPin.Width / 2, teamPin.Height / 2 }; //Top-left corner
			}
			teamPin.setPinPosition(previousPinPosition[0], previousPinPosition[1]);

			//Clearing the reference points in case the user is going through the setup again for any reason
			if(GPSLocation.gpsConfigured == true)
			{
				GPSLocation.gpsConfigured = false;
				GPSLocation.referencePoints.Clear();
				MessageBox.Show("Previous setup is cleared. You can now proceed to re-setup the GPS");
			}

			MessageBox.Show("Team " + teamPin.getTeam().getName() + " selected. Please ask the team to position themselves at different locations of the site.\n"
				+ "As soon as they reach a particular location, wait at least 10 seconds and then tag the position by placing the team pin on the equivalent point on the map, right click on it, and select \"Tag point\".\n"
				+ "Only 2 points are required. For best results, have the points on two different sides of the map.\nSetup will complete as soon as the second point is tagged.");
		}

		//Tag the point at which the team is
		private static void TagPoint_Click(object sender, RoutedEventArgs e)
		{
			MenuItem mi = (MenuItem)sender;
			ContextMenu cm = (ContextMenu)mi.Parent;
			TeamPin teamPin = (TeamPin)cm.PlacementTarget;
			GPSLocation.referencePoints.Add(new GPSLocation(teamPin.getTeam().getGPSLocation().getLattitude(), teamPin.getTeam().getGPSLocation().getLongitude(), teamPin.getX() * GPSLocation.xRatio, teamPin.getY() * GPSLocation.yRatio));
			MessageBox.Show("Point tagged");
			if(GPSLocation.referencePoints.Count == 2)
			{
				MessageBox.Show("Setup completed");

				//CONCORDIA TESTING - debug
				//List<GPSLocation> list = new List<GPSLocation>();
				//list.Add(new GPSLocation(45.497401, -73.578224, 179, 313));
				//list.Add(new GPSLocation(45.495637, -73.579325, 580, 473));
				//GPSLocation.referencePoints = list;

				GPSLocation.setConfigured(true); //Change flag to signify that the setup is successfully done
				mapSection.Update(); //Readding all the pins to the map
				setupOngoing = false;
				gpsStatusCallbacks.SetupCompleted(); //Notifying caller
			}
		}

		//Returning distance from 2 GPS coordinates
		internal static double CalculateGPSDistance(double pointALattitude, double pointALongitude, double pointBLattitude, double pointBLongitude)
		{
			return 6372.795477598 * Math.Acos(Math.Sin(Radians(pointALattitude)) * Math.Sin(Radians(pointBLattitude)) + Math.Cos(Radians(pointALattitude)) * Math.Cos(Radians(pointBLattitude)) * Math.Cos(Radians(pointALongitude) - Radians(pointBLongitude)));
		}

		//Returning the value of the angle formed by the line from A to B points and the longitude axis with only clockwise direction (angle between 0 and and 2PI)
		internal static double CalculateGPSBearing(double pointALattitude, double pointALongitude, double pointBLattitude, double pointBLongitude)
		{
			double dx = Math.Abs(Radians(pointALongitude) - Radians(pointBLongitude));
			double dy = Math.Abs(Math.Log((Math.Tan((Radians(pointBLattitude / 2)) + (Math.PI / 4))) / (Math.Tan((Radians(pointALattitude) / 2) + (Math.PI / 4))), Math.E));

			double angle = Degrees(Math.Atan(dy / dx));
			double bearing;
			if(pointBLongitude < pointALongitude)
			{
				if (pointBLattitude < pointALattitude)
				{
					bearing = 270 - angle;
				}
				else
				{
					bearing = 270 + angle;
				}
			}
			else
			{
				if (pointBLattitude < pointALattitude)
				{
					bearing = 90 + angle;
				}
				else
				{
					bearing = 90 - angle;
				}
			}
			return bearing;
		}

		//Convert angle from degrees to radians
		internal static double Radians(double d)
		{
			return d * Math.PI / 180;
		}

		//Convert angle from radians to degrees
		internal static double Degrees(double d)
		{
			return d * 180 / Math.PI;
		}
	}
}
