using ETD.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Objects
{
    /// <summary>
    /// Resource Model Object
    /// </summary>
    [Serializable()]
    public class Resource
    {
        //Database reflection variables
        private int resourceID;
        private int interventionID;

        private String resourceName;
        private Team team;
		private bool intervening;
        private DateTime moving;
		private bool movingBool;
        private DateTime arrival;
		private bool arrivedBool;

		//Constructors
		public Resource(String resourceName, Team team, bool intervening, DateTime moving, DateTime arrival)
		{
			this.resourceName = resourceName;
			this.team = team;
			this.intervening = intervening;
			this.moving = moving;
			this.arrival = arrival;
            this.resourceID = StaticDBConnection.NonQueryDatabaseWithID("INSERT INTO [Resources] (Intervention_ID, Name, Team_ID, Intervening, Moving, Arrival) VALUES (" + interventionID + ", '" + resourceName.Replace("'", "''") + "', " + team.getID() + ", " + intervening + "', '" + StaticDBConnection.DateTimeSQLite(moving) + "', '" + StaticDBConnection.DateTimeSQLite(arrival) + ")");
		}

        public Resource(Team team)
        {
            this.team = team;
			this.intervening = true;
			this.moving = DateTime.Now;
			this.movingBool = true;
            this.resourceID = StaticDBConnection.NonQueryDatabaseWithID("INSERT INTO [Resources] (Intervention_ID, Team_ID) VALUES (" + interventionID + ", " + team.getID() + ")");
        }
        public Resource(Intervention intervention, Team team)
        {
            this.team = team;
            this.intervening = true;
            this.moving = DateTime.Now;
            this.movingBool = true;
            this.interventionID = intervention.getID();
            this.resourceID = StaticDBConnection.NonQueryDatabaseWithID("INSERT INTO [Resources] (Intervention_ID, Team_ID) VALUES (" + interventionID + ", " + team.getID() + ")");
        }

		//Accessors

		//Returns resource name
        public String getResourceName()
        {
            return resourceName;
        }

		//Returns the team
        public Team getTeam()
        {
            return team;
        }

		//Checks if the team is intervening
		public bool getIntervening()
		{
			return intervening;
		}
	
		//Returns moving time
        public DateTime getMovingTime()
        {
            return moving;
        }

		//Checks if the resource has arrived
		public bool hasArrived()
		{
			return arrivedBool;
		}

		//Returns arrival time
        public DateTime getArrivalTime()
        {
            return arrival;
        }

		//Mutators

		//Set resource name
		public void setResourceName(string resourceName)
		{
			this.resourceName = resourceName;
            StaticDBConnection.NonQueryDatabase("UPDATE [Resources] SET Name='" + resourceName.Replace("'", "''") + "'WHERE Resource_ID=" + resourceID + ";");
		}

		//Set status of intervening
		public void setIntervening(bool intervening)
		{
			this.intervening = intervening;
            StaticDBConnection.NonQueryDatabase("UPDATE [Resources] SET Intervening='" + intervening + "' WHERE Resource_ID=" + resourceID + ";");
		}

		//Set status of moving
		public void setMoving(DateTime moving)
		{
			this.moving = moving;
			this.movingBool = true;
            StaticDBConnection.NonQueryDatabase("UPDATE [Resources] SET Moving='" + StaticDBConnection.DateTimeSQLite(moving) + "', HasArrived='TRUE' WHERE Resource_ID=" + resourceID + ";");
		}

		//Set arrival time
        public void setArrival(DateTime arrival)
        {
            this.arrival = arrival;
			this.arrivedBool = true;
            StaticDBConnection.NonQueryDatabase("UPDATE [Resources] SET Arrival='" + StaticDBConnection.DateTimeSQLite(arrival) + "', HasArrived='TRUE' WHERE Resource_ID=" + resourceID + ";");
        }

    }
}