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
        private String resourceName;
        private Team team;
		private bool intervening;
        private DateTime moving;
        private DateTime arrival;

		public Resource(String resourceName, Team team, bool intervening, DateTime moving, DateTime arrival)
		{
			this.resourceName = resourceName;
			this.team = team;
			this.intervening = intervening;
			this.moving = moving;
			this.arrival = arrival;
		}

        public Resource(Team team)
        {
            this.team = team;
			this.intervening = true;
			this.moving = DateTime.Now;
        }

        public String getResourceName()
        {
            return resourceName;
        }

		public void setResourceName(string resourceName)
		{
			this.resourceName = resourceName;
		}

        public Team getTeam()
        {
            return team;
        }

		public bool getIntervening()
		{
			return intervening;
		}

		public void setIntervening(bool intervening)
		{
			this.intervening = intervening;
		}

		public void setMoving(DateTime moving)
		{
			this.moving = moving;
		}

        public DateTime getMovingTime()
        {
            return moving;
        }

        public void setArrival(DateTime arrival)
        {
            this.arrival = arrival;
        }

        public DateTime getArrivalTime()
        {
            return arrival;
        }
    }
}