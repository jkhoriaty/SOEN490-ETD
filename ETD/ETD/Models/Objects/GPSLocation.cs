using ETD.Models.ArchitecturalObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Objects
{
	public class GPSLocation : Observable
	{
		private static Dictionary<string, GPSLocation> gpsLocationsDictionary = new Dictionary<string, GPSLocation>();
		internal static bool gpsConfigured = false;

		private string id;
		private double lattitude;
		private double longitude;

		public GPSLocation(string id, double lattitude, double longitude)
		{
			this.id = id;
			this.lattitude = lattitude;
			this.longitude = longitude;

			gpsLocationsDictionary.Add(id, this);
		}

		//Return GPSLocation instance using the phones ID
		internal static GPSLocation getGPSLocationFromID(string id)
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

        internal static Dictionary<string, GPSLocation> getDictionary()
        {
            return gpsLocationsDictionary;
        }

		//Method that will translate the lattitude to a map X value
		internal double getMapX()
		{
			return lattitude;
		}

		//Method that will translate the longitude to a map Y value
		internal double getMapY()
		{
			return longitude;
		}

		//Setters

		public void setGPSCoordinates(double lattitude, double longitude)
		{
			this.lattitude = lattitude;
			this.longitude = longitude;
			InstanceModifiedNotification();
		}
	}
}
