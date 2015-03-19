using ETD.Models.ArchitecturalObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ETD.Models.Objects
{
    /// <summary>
    /// Intervention Model Object
    /// </summary>

    public class Intervention : Observable
    {
		public static List<Observer> observerList = new List<Observer>();

        //Database reflection variables
        private int interventionID;
        private int operationID;

        private static int lastIntervention = 0;
		private static List<Intervention> activeInterventionList = new List<Intervention>();
		private static List<Intervention> completedInterventionList = new List<Intervention>();

		private int interventionNumber;

        private DateTime timeOfCall;
        private String callerName;
        private String location;
        private String natureOfCall;

        private int code;
        private String gender;
        private String age;
		private String chiefComplaint;
		private String otherChiefComplaint;

        private List<Resource> resourceList;

		private ABC abc;

        private InterventionAdditionalInfo[] additionalInfo;

        private String conclusion;
        private String conclusionAdditionalInfo;
        private DateTime conclusionTime;
		private bool isConcludedBool;

        private DateTime call911Time;
        private String meetingPoint;
        private String firstResponderCompany;
        private String firstResponderVehicle;
        private DateTime firstResponderArrivalTime;
        private String ambulanceCompany;
        private String ambulanceVehicle;
        private DateTime ambulanceArrivalTime;

        public Intervention()
        {
            this.resourceList = new List<Resource>();
            this.interventionNumber = ++lastIntervention;
            this.timeOfCall = DateTime.Now;
            this.additionalInfo = new InterventionAdditionalInfo[10];
            this.abc = new ABC();
			this.isConcludedBool = false;

            activeInterventionList.Add(this);
            ClassModifiedNotification(typeof(Intervention));
        }

		//Set intervention as completed
        public void Completed()
        {
			//Set teams to unavailable when the team is completed
			foreach(Team team in getInterveningTeamList())
			{
				team.setStatus("unavailable");
			}
            activeInterventionList.Remove(this);
            completedInterventionList.Add(this);
			isConcludedBool = true;
			ClassModifiedNotification(typeof(Intervention));
        }

        public static List<Intervention> getActiveInterventionList()
        {
            return activeInterventionList;
		}

		public static List<Intervention> getCompletedInterventionList()
		{
			return completedInterventionList;
		}

		public void AddInterveningTeam(Team team)
		{
			resourceList.Add(new Resource(team));
			InstanceModifiedNotification();
		}

		public void RemoveInterveningTeam(Team team)
		{
			foreach(Resource resource in resourceList)
			{
				if(resource.getTeam() == team)
				{
					resource.setIntervening(false);
				}
			}
			InstanceModifiedNotification();
		}

		public List<Team> getInterveningTeamList()
		{
			List<Team> interveningTeams = new List<Team>();
			foreach(Resource resource in resourceList)
			{
				if(resource.getIntervening() == true)
				{
					interveningTeams.Add(resource.getTeam());
				}
			}
			return interveningTeams;
        }

		public void InterveningTeamArrived(Team team)
		{
			foreach (Resource resource in resourceList)
			{
				if (resource.getTeam() == team)
				{
					resource.setArrival(DateTime.Now);
					InstanceModifiedNotification();
				}
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
			InstanceModifiedNotification();
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
			InstanceModifiedNotification();
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
			InstanceModifiedNotification();
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

		/*public void setResource(int position, Resource resource)
		{
			resources[position] = resource;
		}*/

		public List<Resource> getResourceList()
		{
			return resourceList;
		}

		public void setABC(ABC abc)
		{
			this.abc = abc;
		}

		public ABC getABC()
		{
			return abc;
		}

        public void setAdditionalInfo(int position, InterventionAdditionalInfo info)
        {
            this.additionalInfo[position] = info;
        }

		public InterventionAdditionalInfo getAdditionalInfo(int position)
        {
            return this.additionalInfo[position];
        }
        public InterventionAdditionalInfo[] getAllAdditionalInfo()
        {
            return this.additionalInfo;
        }

		public void setConclusion(String conclusion)
		{
			this.conclusion = conclusion;
		}

		public String getConclusion()
		{
			return conclusion;
		}

		public bool isConcluded()
		{
			return isConcludedBool;
		}

		public void setConclusionAdditionalInfo(String additionalInfo)
		{
            this.conclusionAdditionalInfo = additionalInfo;
		}

		public String getConclusionAdditionalInfo()
		{
			return conclusionAdditionalInfo;
		}

		public void setConclusionTime(DateTime conclusionTime)
		{
			this.conclusionTime = conclusionTime;
		}

		public DateTime getConclusionTime()
		{
			return conclusionTime;
		}

		public void setCall911Time(DateTime call911Time)
		{
			this.call911Time = call911Time;
		}

		public DateTime getCall911Time()
		{
			return call911Time;
		}

		public void setMeetingPoint(String meetingPoint)
		{
			this.meetingPoint = meetingPoint;
		}

		public String getMeetingPoint()
		{
			return meetingPoint;
		}

		public void setFirstResponderCompany(String firstResponderCompany)
		{
			this.firstResponderCompany = firstResponderCompany;
		}

		public String getFirstResponderCompany()
		{
			return firstResponderCompany;
		}

		public void setFirstResponderVehicle(String firstResponderVehicle)
		{
			this.firstResponderVehicle = firstResponderVehicle;
		}

		public String getFirstResponderVehicle()
		{
			return firstResponderVehicle;
		}

		public void setFirstResponderArrivalTime(DateTime firstResponderArrivalTime)
		{
			this.firstResponderArrivalTime = firstResponderArrivalTime;
		}

		public DateTime getFirstResponderArrivalTime()
		{
			return firstResponderArrivalTime;
		}

		public void setAmbulanceCompany(String ambulanceCompany)
		{
			this.ambulanceCompany = ambulanceCompany;
		}

		public String getAmbulanceCompany()
		{
			return ambulanceCompany;
		}

		public void setAmbulanceVehicle(String ambulanceVehicle)
		{
			this.ambulanceVehicle = ambulanceVehicle;
		}

		public String getAmbulanceVehicle()
		{
			return ambulanceVehicle;
		}

		public void setAmbulanceArrivalTime(DateTime ambulanceArrivalTime)
		{
			this.ambulanceArrivalTime = ambulanceArrivalTime;
		}

		public DateTime getAmbulanceArrivalTime()
		{
			return ambulanceArrivalTime;
		}

        public bool IsActive()
        {
            return activeInterventionList.Contains(this);
        }

        public bool IsCompleted()
        {
            return completedInterventionList.Contains(this);
        }

		public void ResourceModified()
		{
			InstanceModifiedNotification();
		}

        public int getID()
        {
            return interventionID;
        }

        public int getParentID()
        {
            return operationID;
        }
    }
}
