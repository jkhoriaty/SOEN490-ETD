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
		public static List<Observer> observerList = new List<Observer>();//Contains a list of observers

		private static int lastIntervention = 0;
		private static List<Intervention> activeInterventionList = new List<Intervention>();//Contains a list of active interventions
		private static List<Intervention> completedInterventionList = new List<Intervention>();//Contains a list of completed intervention

		private int interventionNumber;

        //Variables used for an intervention
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

        private DateTime call911Time;
        private String meetingPoint;
        private String firstResponderCompany;
        private String firstResponderVehicle;
        private DateTime firstResponderArrivalTime;
        private String ambulanceCompany;
        private String ambulanceVehicle;
        private DateTime ambulanceArrivalTime;

        //Creates an intervention
        public Intervention()
        {
            this.resourceList = new List<Resource>();
            this.interventionNumber = ++lastIntervention;
            this.timeOfCall = DateTime.Now;
            this.additionalInfo = new InterventionAdditionalInfo[10];
            this.abc = new ABC();

            activeInterventionList.Add(this);
            ClassModifiedNotification(typeof(Intervention));
        }


        //Set the intervention as completed,removes it from the list of active interventions and adds it to the list of completed interventions.
        public void Completed()
        {
            activeInterventionList.Remove(this);
            completedInterventionList.Add(this);
			ClassModifiedNotification(typeof(Intervention));
        }

        //Assign a team to an intervention
		public void AddInterveningTeam(Team team)
		{
			resourceList.Add(new Resource(team));
			InstanceModifiedNotification();
		}

        //Remove a team from an intervention
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

        //When a team reaches the intervention point, set the arrival time to the current time
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

        //Checks if the intervantion is still active
        public bool IsActive()
        {
            return activeInterventionList.Contains(this);
        }

        //Checks if the intervention is completed
        public bool IsCompleted()
        {
            return completedInterventionList.Contains(this);
        }


        //Accessors

        //Returns a list of active interventions
        public static List<Intervention> getActiveInterventionList()
        {
            return activeInterventionList;
		}

        //Returns a list of completed interventions
		public static List<Intervention> getCompletedInterventionList()
		{
			return completedInterventionList;
		}

        //Returns a list of team assigned to an intervention
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

        //Returns the intervention number
		public int getInterventionNumber()
		{
			return interventionNumber;
		}

        //Returns the time of call
	    public DateTime getTimeOfCall()
		{
			return timeOfCall;
		}

        //Returns the caller's name
		public String getCallerName()
		{
			return callerName;
		}

        //Return the location of the intervention
		public String getLocation()
		{
			return location;
		}

        //Returns the nature of the call
	    public String getNatureOfCall()
		{
			return natureOfCall;
		}

        //Returns the intervention's code
        public int getCode()
        {
            return this.code;
        }

        //Returns the gender of the patient
        public String getGender()
        {
            return this.gender;
        }

        //Returns the chief complaint
		public String getChiefComplaint()
		{
			return chiefComplaint;
		}

        //Returns other chief complaints
		public String getOtherChiefComplaint()
		{
			return otherChiefComplaint;
		}

        //Returns the list of ressources
	    public List<Resource> getResourceList()
		{
			return resourceList;
		}

        //Returns the ABC
	    public ABC getABC()
		{
			return abc;
		}

        //Returns additional information
		public InterventionAdditionalInfo getAdditionalInfo(int position)
        {
            return this.additionalInfo[position];
        }

        //Returns a list of additional information
        public InterventionAdditionalInfo[] getAllAdditionalInfo()
        {
            return this.additionalInfo;
        }

        //Returns the patient's age
        public String getAge()
        {
            return this.age;
        }

        //Retruns the conclusion's additional information 
	    public String getConclusionAdditionalInfo()
		{
			return conclusionAdditionalInfo;
		}

        //Returns the conclusion information
		public String getConclusion()
		{
			return conclusion;
		}

        //Returns the time of completion of the intervention
	    public DateTime getConclusionTime()
		{
			return conclusionTime;
		}
		
        //Returns the time of the 911 call
		public DateTime getCall911Time()
		{
			return call911Time;
		}

        //Returns the meeting point
        public String getMeetingPoint()
        {
            return meetingPoint;
        }

        //Returns the first responder's name
        public String getFirstResponderCompany()
        {
            return firstResponderCompany;
        }

        //Returns the first responder's vehicle
        public String getFirstResponderVehicle()
        {
            return firstResponderVehicle;
        }

        //Returns the first responder's arrival time
        public DateTime getFirstResponderArrivalTime()
        {
            return firstResponderArrivalTime;
        }

        //Returns the ambulance company
        public String getAmbulanceCompany()
        {
            return ambulanceCompany;
        }

        //Returns the type of ambulance vehicle
        public String getAmbulanceVehicle()
        {
            return ambulanceVehicle;
        }

        //Returns the ambulance arrival time
        public DateTime getAmbulanceArrivalTime()
        {
            return ambulanceArrivalTime;
        }

        //Returns the time it took to complete the intervention
        public TimeSpan getElapsed()
        {
            if (activeInterventionList.Contains(this))
            {
                return DateTime.Now - timeOfCall;
            }
            else
            {
                return conclusionTime - timeOfCall;
            }
        }

        //Mutators

        //Sets the intervention number/priority
        public void setInterventionNumber(int interventionNumber)
        {
            this.interventionNumber = interventionNumber;
        }

        //Sets the time of call
		public void setTimeOfCall(DateTime timeOfCall)
		{
			this.timeOfCall = timeOfCall;
		}

        //Sets the caller's name
		public void setCallerName(String callerName)
		{
			this.callerName = callerName;
		}

        //Sets the location of the intervention
		public void setLocation(String location)
		{
			this.location = location;
		}

        //Sets the nature of the call
		public void setNatureOfCall(String natureOfCall)
		{
			this.natureOfCall = natureOfCall;
		}

	    //Sets the intervention's code
        public void setCode(int code)
        {
            this.code = code;
        }
    
        //Sets the gender of the patient
        public void setGender(String gender)
        {
            this.gender = gender;
        }
  
        //Sets the patient's age
        public void setAge(String age) 
        {
            this.age = age;
        }
        
        //Sets the chief of complaint
		public void setChiefComplaint(String chiefComplaint)
		{
			this.chiefComplaint = chiefComplaint;
		}

        //Sets the other chief of complaint
		public void setOtherChiefComplaint(String otherChiefComplaint)
		{
			this.otherChiefComplaint = otherChiefComplaint;
		}

	    //Sets the intervention's ABC
		public void setABC(ABC abc)
		{
			this.abc = abc;
		}
	
        //Sets the additional information for an intervention
        public void setAdditionalInfo(int position, InterventionAdditionalInfo info)
        {
            this.additionalInfo[position] = info;
        }

        //Sets the intervention's conclusion
		public void setConclusion(String conclusion)
		{
			this.conclusion = conclusion;
		}

        //Sets the intervention's additional information
		public void setConclusionAdditionalInfo(String additionalInfo)
		{
            this.conclusionAdditionalInfo = additionalInfo;
		}

        //Sets the end time of an intervention
        public void setConclusionTime(DateTime conclusionTime)
		{
			this.conclusionTime = conclusionTime;
		}

        //Sets the 911 call time
		public void setCall911Time(DateTime call911Time)
		{
			this.call911Time = call911Time;
		}

	    //Sets the intervention location
		public void setMeetingPoint(String meetingPoint)
		{
			this.meetingPoint = meetingPoint;
		}

	    //Sets the first responder company
		public void setFirstResponderCompany(String firstResponderCompany)
		{
			this.firstResponderCompany = firstResponderCompany;
		}
	
        //Sets the first respodner vehicle
	    public void setFirstResponderVehicle(String firstResponderVehicle)
		{
			this.firstResponderVehicle = firstResponderVehicle;
		}

        //Sets the first responder arrival time
		public void setFirstResponderArrivalTime(DateTime firstResponderArrivalTime)
		{
			this.firstResponderArrivalTime = firstResponderArrivalTime;
		}
	
        //Sets the ambulance company
		public void setAmbulanceCompany(String ambulanceCompany)
		{
			this.ambulanceCompany = ambulanceCompany;
		}

        //Sets the ambulance vehicle
		public void setAmbulanceVehicle(String ambulanceVehicle)
		{
			this.ambulanceVehicle = ambulanceVehicle;
		}
	
        //Sets the ambulance arrival time
		public void setAmbulanceArrivalTime(DateTime ambulanceArrivalTime)
		{
			this.ambulanceArrivalTime = ambulanceArrivalTime;
		}
    }
}
