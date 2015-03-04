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
    public class Resource
    {
        //Variables used for a resource
        private String resourceName;
        private Team team;
		private bool intervening;
        private DateTime moving;
        private DateTime arrival;

        //Creates a new resource
		public Resource(String resourceName, Team team, bool intervening, DateTime moving, DateTime arrival)
		{
			this.resourceName = resourceName;
			this.team = team;
			this.intervening = intervening;
			this.moving = moving;
			this.arrival = arrival;
		}

        //Creates a new resource
        public Resource(Team team)
        {
            this.team = team;
			this.intervening = true;
			this.moving = DateTime.Now;
        }

        //Accessors

        //Returns the resource's name
        public String getResourceName()
        {
            return resourceName;
        }

        //Returns the team assigned to the resource
        public Team getTeam()
        {
            return team;
        }

        //Checks if the resource is being used
        public bool getIntervening()
		{
			return intervening;
		}

        //Returns the time it took for a resource to reach the destination point
        public DateTime getMovingTime()
        {
            return moving;
        }

        //Returns the time of arrival of a resource
        public DateTime getArrivalTime()
        {
            return arrival;
        }

        //Returns the resource's name
		public void setResourceName(String resourceName)
		{
			this.resourceName = resourceName;
		}

    
        //Mutators

        //Sets the resource as currently in use
		public void setIntervening(bool intervening)
		{
			this.intervening = intervening;
		}

        //Sets the resource was currently moving
		public void setMoving(DateTime moving)
		{
			this.moving = moving;
		}

        //Sets the resource's time of arrival
        public void setArrival(DateTime arrival)
        {
            this.arrival = arrival;
        }
    }
}