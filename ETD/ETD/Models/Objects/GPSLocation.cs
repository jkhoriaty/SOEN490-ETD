﻿using ETD.Models.ArchitecturalObjects;
using ETD.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ETD.Models.Objects
{
    [Serializable()]
	public class GPSLocation : Observable
	{
		//Used to keep track of the difference between the to-scale image and the distorted map
		internal static double xRatio;
		internal static double yRatio;

		private static Dictionary<string, GPSLocation> gpsLocationsDictionary = new Dictionary<string, GPSLocation>();
		internal static List<GPSLocation> referencePoints = new List<GPSLocation>();
		internal static bool gpsConfigured = false;

		private const int toleratedFailedUpdates = 3;

		public string id;
		private double lattitude;
		private double longitude;
		private double X;
		private double Y;
		private int consecutiveFailedUpdates = 0;
		private bool teamSplit = false;

		//Creating GPS locations from volunteers positions
		public GPSLocation(string id, double lattitude, double longitude)
		{
			this.id = id;
			this.lattitude = lattitude;
			this.longitude = longitude;

			if (gpsConfigured)
			{
				TranslatePointToMap();
			}

			gpsLocationsDictionary.Add(id, this);
		}

		//Creating reference points
		public GPSLocation(double lattitude, double longitude, double X, double Y)
		{
			this.lattitude = lattitude;
			this.longitude = longitude;
			this.X = X;
			this.Y = Y;
		}

		//Return GPSLocation instance using the phones ID
		public static GPSLocation getGPSLocationFromID(string id)
		{
			if(gpsLocationsDictionary.ContainsKey(id))
			{
				return gpsLocationsDictionary[id];
			}
			else
			{
				return null;
			}
		}

		//Translate the GPS coordinates to X-Y Map coordinates, via X-Y Image coordinates
		private void TranslatePointToMap()
		{
			//Handling case when the current position is equal to the reference position
			if(lattitude == referencePoints.ElementAt(0).getLattitude() && longitude == referencePoints.ElementAt(0).getLongitude())
			{
				X = referencePoints.ElementAt(0).getX() / xRatio;
				Y = referencePoints.ElementAt(0).getY() / yRatio;
				return;
			}

			//Getting all the necessary lengths in order to get the triangle angles using them
			double distanceA = GPSServices.CalculateGPSDistance(referencePoints.ElementAt(1).getLattitude(), referencePoints.ElementAt(1).getLongitude(), lattitude, longitude);
			double distanceB = GPSServices.CalculateGPSDistance(referencePoints.ElementAt(0).getLattitude(), referencePoints.ElementAt(0).getLongitude(), lattitude, longitude);
			double distanceC = GPSServices.CalculateGPSDistance(referencePoints.ElementAt(0).getLattitude(), referencePoints.ElementAt(0).getLongitude(), referencePoints.ElementAt(1).getLattitude(), referencePoints.ElementAt(1).getLongitude());

			//Getting distance ratio
			double distanceRatio = distanceB / distanceC;

			//Getting all the angles of the triangle formed by the reference point 0, reference point 1, and the current team position 
			double angleA = Math.Acos(((distanceB * distanceB) + (distanceC * distanceC) - (distanceA * distanceA)) / (2 * distanceB * distanceC));

			//Find vector between reference points
			double dx = referencePoints.ElementAt(1).getX() - referencePoints.ElementAt(0).getX();
			double dy = referencePoints.ElementAt(1).getY() - referencePoints.ElementAt(0).getY();

			//Scaling the vector so that it has the appropriate length
			double dxScaled = dx * distanceRatio;
			double dyScaled = dy * distanceRatio;

			//Determining if the point is in the clockwise or anti-clockwise direction of the reference line
			double referenceBearing = GPSServices.CalculateGPSBearing(referencePoints.ElementAt(0).getLattitude(), referencePoints.ElementAt(0).getLongitude(), referencePoints.ElementAt(1).getLattitude(), referencePoints.ElementAt(1).getLongitude());
			double actualBearing = GPSServices.CalculateGPSBearing(referencePoints.ElementAt(0).getLattitude(), referencePoints.ElementAt(0).getLongitude(), lattitude, longitude);
			if(referenceBearing < 180)
			{
				if(actualBearing < referenceBearing || actualBearing > (referenceBearing + 180))
				{
					angleA = -angleA; //Changing direction of rotation
				}
			}
			else
			{
				if(actualBearing < referenceBearing && actualBearing > (referenceBearing - 180))
				{
					angleA = -angleA; //Changing direction of rotation
				}
			}

			//Rotate vector to get the vector that start at the first reference point and ends at the current location of the team
			double dxPrime = (dxScaled * Math.Cos(angleA)) - (dyScaled * Math.Sin(angleA));
			double dyPrime = (dxScaled * Math.Sin(angleA)) + (dyScaled * Math.Cos(angleA));

			//Getting the final image coordinates of the point at which the team is
			double xImage = referencePoints.ElementAt(0).getX() + dxPrime;
			double yImage = referencePoints.ElementAt(0).getY() + dyPrime;

			//Translating the image coordinates into map coordinates with the generated ratios
			X = xImage / xRatio;
			Y = yImage / yRatio;
		}

		// Getters

        public static Dictionary<string, GPSLocation> getDictionary()
        {
            return gpsLocationsDictionary;
        }

		public double getLattitude()
		{
			return lattitude;
		}

		public double getLongitude()
		{
			return longitude;
		}

		public double getX()
		{
			return X;
		}

		public double getY()
		{
			return Y;
		}

		internal bool PhoneOnline()
		{
			return (consecutiveFailedUpdates < 3 && !teamSplit);
		}

		//Setters

		public static void setConfigured(bool status)
		{
			gpsConfigured = status;
		}

		public void setGPSCoordinates(double lattitude, double longitude)
		{
			if(this.lattitude == lattitude && this.longitude == longitude) //The server did not receive a location update for that team
			{
				consecutiveFailedUpdates++;
			}
			else
			{
				consecutiveFailedUpdates = 0;
				
				this.lattitude = lattitude;
				this.longitude = longitude;
			}

			if (gpsConfigured)
			{
				TranslatePointToMap();
				InstanceModifiedNotification();
			}
		}

		internal void setTeamSplit(bool splitStatus)
		{
			teamSplit = splitStatus;
		}
	}
}
