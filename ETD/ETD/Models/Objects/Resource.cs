﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Objects
{
    public class Resource
    {
        private String resourceName;
        private String team;
        private DateTime moving;
        private DateTime arrival;

        public Resource(String resourceName, String team, DateTime moving, DateTime arrival)
        {
            this.resourceName = resourceName;
            this.team = team;
            this.moving = moving;
            this.arrival = arrival;
        }

        public String getResourceName()
        {
            return resourceName;
        }

        public String getTeamName()
        {
            return team;
        }

        public DateTime getMovingTime()
        {
            return moving;
        }

        public DateTime getArrivalTime()
        {
            return arrival;
        }
    }
}