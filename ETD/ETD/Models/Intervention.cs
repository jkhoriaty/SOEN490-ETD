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

        private DateTime timeOfCall = DateTime.Now;
        private String callerName;
        private String location;
        private String natureOfCall;

        public Resource[] resources = new Resource[10];

        private int code;
        private String gender;
        private String age;
		private String chiefComplaint;
		private String otherChiefComplaint;

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

			public Resource(String resourceName, String team, DateTime moving, DateTime arrival)
			{
				this.resourceName = resourceName;
				this.team = team;
				this.moving = moving;
				this.arrival = arrival;
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

		public void setTimeOfCall(DateTime timeOfCall)
		{
			this.timeOfCall = timeOfCall;
		}

		public DateTime getTimeOfCall()
		{
			return timeOfCall;
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

        public void setGender(String gender)
        {
            this.gender = gender;
        }
        public String getGender()
        {
            return this.gender;
        }

        public void setAge(String age) 
        {
            this.age = age;
        }
        public String getAge()
        {
            return this.age;
        }

		public void setChiefComplaint(String chiefComplaint)
		{
			this.chiefComplaint = chiefComplaint;
		}

		public String getChiefComplaint()
		{
			return chiefComplaint;
		}

		public void setOtherChiefComplaint(String otherChiefComplaint)
		{
			this.otherChiefComplaint = otherChiefComplaint;
		}

		public String getOtherChiefComplaint()
		{
			return otherChiefComplaint;
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
