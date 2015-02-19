using ETD.Models.ArchitecturalObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ETD.Models.Objects
{
	public enum Consciousness { alert, verbal, painful, unconscious, notSet };
	public enum Airways { clear, partiallyObstructed, completelyObstructed, notSet };
	public enum Breathing { normal, difficulty, absent, notSet };
	public enum Circulation { normal, chestPain, hemorrhage, noPulse, notSet };
	public enum Conclusions { returnToSite, returnToHome, referredToDoctor, equipmentDistribution, hospital, patientNotFound, noInterventions, other, notSet };

    public class Intervention : Observable
    {
		private static int lastIntervention = 0;
		private static List<Intervention> activeInterventionList = new List<Intervention>();
		private static List<Intervention> completedInterventionList = new List<Intervention>();

		private int interventionNumber;
		private List<Team> interveningTeamList = new List<Team>();

        private DateTime timeOfCall = DateTime.Now;
        private String callerName;
        private String location;
        private String natureOfCall;

        private int code;
        private String gender;
        private String age;
		private String chiefComplaint;
		private String otherChiefComplaint;

        private Resource[] resources = new Resource[10];

		private ABC abc;

        private AdditionalInformation[] additionalInfo;

		private Conclusions conclusion;
		private String conclusionAdditionalInfo;
		private DateTime conclusionTime;

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
			interventionNumber = ++lastIntervention;
            this.timeOfCall = DateTime.Now;
            additionalInfo = new AdditionalInformation[10];

			activeInterventionList.Add(this);
			MessageBox.Show("Create intervention notify");
			NotifyAll();
		}

		public void Completed()
		{
			activeInterventionList.Remove(this);
			completedInterventionList.Add(this);
			NotifyAll();
		}

		public static List<Intervention> getActiveInterventionList()
		{
			return activeInterventionList;
		}

		public static List<Intervention> getCompletedInterventionList()
		{
			return completedInterventionList;
		}

		public void AddTeam(Team team)
		{
			interveningTeamList.Add(team);
			MessageBox.Show("Add team to intervention notify");
			NotifyAll();
		}

		public void RemoveTeam(Team team)
		{
			interveningTeamList.Remove(team);
			NotifyAll();
		}

		public List<Team> getInterveningTeamList()
		{
			return interveningTeamList;
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

            public String getResourceName()
            {
                return resourceName;
            }

            public String getTeamName()
            {
                return team;
            }
		}

		public struct ABC
		{
			private Consciousness consciousness;
			private bool disoriented;
			private Airways airways;
			private Breathing breathing;
			private int breathingFrequency;
			private Circulation circulation;
			private int circulationFrequency;

			public ABC(Consciousness consciousness, bool disoriented, Airways airways, Breathing breathing, int breathingFrequency, Circulation circulation, int circulationFrequency)
			{
				this.consciousness = consciousness;
				this.disoriented = disoriented;
				this.airways = airways;
				this.breathing = breathing;
				this.breathingFrequency = breathingFrequency;
				this.circulation = circulation;
				this.circulationFrequency = circulationFrequency;
			}
		}

		public struct AdditionalInformation
		{
			private String information;
			private DateTime timestamp;

			public AdditionalInformation(String information, DateTime timestamp)
			{
				this.information = information;
				this.timestamp = timestamp;
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

		public void setResources(int position, Resource resource)
		{
			resources[position] = resource;
		}

		public Resource[] getResources()
		{
			return resources;
		}

		public void setABC(ABC abc)
		{
			this.abc = abc;
		}

		public ABC getABC()
		{
			return abc;
		}

        public void setAdditionalInfo(int position, AdditionalInformation info)
        {
            this.additionalInfo[position] = info;
        }

		public AdditionalInformation getAdditionalInfo(int position)
        {
            return this.additionalInfo[position];
        }

		public void setConclusion(Conclusions conclusion)
		{
			this.conclusion = conclusion;
		}

		public Conclusions getConclusion()
		{
			return conclusion;
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
    }
}
