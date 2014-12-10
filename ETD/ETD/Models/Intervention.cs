using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models
{
    public class Intervention
    {
		private static int lastIntervention = 0;
		private int interventionNumber;
        private DateTime timeOfCall;
        private String callerName;
        private String location;
        private String natureOfCall;
        private Resource[] resources = new Resource[5];

        private int code;
        private char gender;
        private int age;

        private String additionalInfo;

		public Intervention()
		{
			interventionNumber = ++lastIntervention;
            this.timeOfCall = DateTime.Now;
		}

		public struct Resource
		{
			private String resourceName;
			private String team;
			private DateTime moving;
			private DateTime arrival;

			public void setResourceName(String resourceName)
			{
				this.resourceName = resourceName;
			}

			public String getResourceName()
			{
				return resourceName;
			}

			public void setTeam(String team)
			{
				this.team = team;
			}

			public String getTeam()
			{
				return team;
			}

			public void setMoving(DateTime moving)
			{
				this.moving = moving;
			}

			public DateTime getMoving()
			{
				return moving;
			}

			public void setArrival(DateTime arrival)
			{
				this.arrival = arrival;
			}

			public DateTime getArrival()
			{
				return arrival;
			}
		}

		public void setInterventionNumber(int interventionNumber)
		{
			this.interventionNumber = interventionNumber;
		}

		public int getInterventionNumber()
		{
			return interventionNumber;
		}

		public void setCallerName(String callerName)
		{
			this.callerName = callerName;
		}

		public String getCallerName()
		{
			return callerName;
		}

		public void setLocation(String location)
		{
			this.location = location;
		}

		public String getLocation()
		{
			return location;
		}

		public void setNatureOfCall(String natureOfCall)
		{
			this.natureOfCall = natureOfCall;
		}

		public String getNatureOfCall()
		{
			return natureOfCall;
		}

        public void setCode(int code)
        {
            this.code = code;
        }
        public int getCode()
        {
            return this.code;
        }

        public void setGender(char gender)
        {
            this.gender = gender;
        }
        public char getGender()
        {
            return this.gender;
        }

        public void setAge(int age) 
        {
            this.age = age;
        }
        public int getAge()
        {
            return this.age;
        }

        public void setAdditionalInfo(String info)
        {
            this.additionalInfo = info;
        }

        public String getAdditionalInfo()
        {
            return this.additionalInfo;
        }
    }
}
