using ETD.Models.ArchitecturalObjects;
using ETD.Services.Database;
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

    [Serializable()]
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
		private DateTime firstTeamArrivalTime;

        private int callID;
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
            this.abc = new ABC(this);
			this.isConcludedBool = false;
			this.firstTeamArrivalTime = DateTime.MinValue;
            this.callID = -1;

            activeInterventionList.Add(this);
            ClassModifiedNotification(typeof(Intervention));
            
            if (Operation.currentOperation != null)
            {
                this.operationID = Operation.currentOperation.getID();
            }
            this.interventionID = StaticDBConnection.NonQueryDatabaseWithID("INSERT INTO [Interventions] (Operation_ID, Intervention_Number, Time_Of_Call) VALUES (" + operationID + ", " + interventionNumber + ", '" + StaticDBConnection.DateTimeSQLite(timeOfCall) + "')");
        }

        public Intervention(int id)
        {
            this.interventionID = id;
            this.callID = -1;
            System.Data.SQLite.SQLiteDataReader results = StaticDBConnection.QueryDatabase("SELECT Operation_ID, Intervention_Number, Time_Of_Call, Caller, Location, Nature_Of_Call, Code, Gender, Age, Chief_Complaint, Other_Chief_Complaint FROM [Interventions] WHERE Intervention_ID=" + id + ";");
            results.Read();

            this.operationID = results.GetInt32(0);
            this.interventionNumber = results.GetInt32(1);
            this.timeOfCall = results.GetDateTime(2);
            this.callerName = (results.IsDBNull(3)) ? "" : results.GetString(3);
            this.location = (results.IsDBNull(4)) ? "" : results.GetString(4);
            this.natureOfCall = (results.IsDBNull(5)) ? "" : results.GetString(5);
            this.code = (results.IsDBNull(6)) ? 0 : results.GetInt32(6);
            this.gender = (results.IsDBNull(7)) ? "" : results.GetString(7);
            this.age = (results.IsDBNull(8)) ? "" : results.GetInt32(8).ToString();
            this.chiefComplaint = (results.IsDBNull(9)) ? "" : results.GetString(9);
            this.otherChiefComplaint = (results.IsDBNull(10)) ? "" : results.GetString(10);

            this.resourceList = new List<Resource>();
            this.abc = new ABC(this.interventionID);


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
			if(firstTeamArrivalTime == DateTime.MinValue)
			{
				firstTeamArrivalTime = DateTime.Now;
			}
			team.incrementInterventionCount();
			resourceList.Add(new Resource(this,team));

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
		
		public DateTime getFirstTeamArrivalTime()
		{
			return this.firstTeamArrivalTime;
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
            
            StaticDBConnection.NonQueryDatabase("UPDATE [Interventions] SET Caller='" + callerName.Replace("'","''") + "' WHERE Intervention_ID=" + interventionID + ";");
		}

		public String getCallerName()
		{
			return callerName;
		}

		public void setLocation(String location)
		{
			this.location = location;
            StaticDBConnection.NonQueryDatabase("UPDATE [Interventions] SET Location='" + location.Replace("'", "''") + "' WHERE Intervention_ID=" + interventionID + ";");
		}

		public String getLocation()
		{
			return location;
		}

		public void setNatureOfCall(String natureOfCall)
		{
			this.natureOfCall = natureOfCall;
            StaticDBConnection.NonQueryDatabase("UPDATE [Interventions] SET Nature_Of_Call='" + natureOfCall.Replace("'", "''") + "' WHERE Intervention_ID=" + interventionID + ";");
		}

		public String getNatureOfCall()
		{
			return natureOfCall;
		}

        public void setCode(int code)
        {
            this.code = code;
            StaticDBConnection.NonQueryDatabase("UPDATE [Interventions] SET Code=" + code + " WHERE Intervention_ID=" + interventionID + ";");
			//InstanceModifiedNotification();
        }
        public int getCode()
        {
            return this.code;
        }

        public void setGender(String gender)
        {
            this.gender = gender;
            StaticDBConnection.NonQueryDatabase("UPDATE [Interventions] SET Gender='" + gender.Replace("'", "''") + "' WHERE Intervention_ID=" + interventionID + ";");
        }
        public String getGender()
        {
            return this.gender;
        }

        public void setAge(String age) 
        {
            this.age = age;
            if (age.Length > 0)
            {
                StaticDBConnection.NonQueryDatabase("UPDATE [Interventions] SET Age=" + int.Parse(age) + " WHERE Intervention_ID=" + interventionID + ";");
            }
        }
        public String getAge()
        {
            return this.age;
        }

		public void setChiefComplaint(String chiefComplaint)
		{
			this.chiefComplaint = chiefComplaint;
            StaticDBConnection.NonQueryDatabase("UPDATE [Interventions] SET Chief_Complaint='" + StaticDBConnection.GetResourceName(chiefComplaint).Replace("'", "''") + "' WHERE Intervention_ID=" + interventionID + ";");
			//InstanceModifiedNotification();
		}

		public String getChiefComplaint()
		{
			return chiefComplaint;
		}

		public void setOtherChiefComplaint(String otherChiefComplaint)
		{
			this.otherChiefComplaint = otherChiefComplaint;
            StaticDBConnection.NonQueryDatabase("UPDATE [Interventions] SET Other_Chief_Complaint='" + otherChiefComplaint.Replace("'", "''") + "' WHERE Intervention_ID=" + interventionID + ";");
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
            StaticDBConnection.NonQueryDatabase("UPDATE [ABCs] SET Intervention_ID=" + interventionID + " WHERE ABC_ID=" + abc.getID() + ";");
		}

		public ABC getABC()
		{
			return abc;
		}

        public void setAdditionalInfo(int position, InterventionAdditionalInfo info)
        {
            this.additionalInfo[position] = info;
            StaticDBConnection.NonQueryDatabase("UPDATE [Additional_Informations] SET Intervention_ID=" + interventionID + " WHERE Additional_Info_ID=" + info.getID() + ";");
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
            StaticDBConnection.NonQueryDatabase("UPDATE [Interventions] SET Conclusion='" + StaticDBConnection.GetResourceName(conclusion).Replace("'", "''") + "' WHERE Intervention_ID=" + interventionID + ";");
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
            StaticDBConnection.NonQueryDatabase("UPDATE [Interventions] SET Conclusion_Info='" + additionalInfo.Replace("'", "''") + "' WHERE Intervention_ID=" + interventionID + ";");
		}

		public String getConclusionAdditionalInfo()
		{
			return conclusionAdditionalInfo;
		}

		public void setConclusionTime(DateTime conclusionTime)
		{
			this.conclusionTime = conclusionTime;
            StaticDBConnection.NonQueryDatabase("UPDATE [Interventions] SET Conclusion_Time='" + StaticDBConnection.DateTimeSQLite(conclusionTime) + "' WHERE Intervention_ID=" + interventionID + ";");
		}

		public DateTime getConclusionTime()
		{
			return conclusionTime;
		}

		public void setCall911Time(DateTime call911Time)
		{
            this.callID = StaticDBConnection.NonQueryDatabaseWithID("INSERT INTO [Calls] (Intervention_ID) VALUES (" + this.interventionID + ");");
			if (callID > -1)
            {
                this.call911Time = call911Time;
                StaticDBConnection.NonQueryDatabase("UPDATE [Calls] SET Call_Time='" + StaticDBConnection.DateTimeSQLite(call911Time) + "' WHERE Call_ID=" + callID + ";");
            }
		}

		public DateTime getCall911Time()
		{
			return call911Time;
		}

		public void setMeetingPoint(String meetingPoint)
		{
            if (callID > -1)
            {
                this.meetingPoint = meetingPoint;
                StaticDBConnection.NonQueryDatabase("UPDATE [Calls] SET Meeting_Point='" + meetingPoint.Replace("'", "''") + "' WHERE Call_ID=" + callID + ";");
            }
		}

		public String getMeetingPoint()
		{
			return meetingPoint;
		}

		public void setFirstResponderCompany(String firstResponderCompany)
		{
            if (callID > -1)
            {
                this.firstResponderCompany = firstResponderCompany;
                StaticDBConnection.NonQueryDatabase("UPDATE [Calls] SET First_Responder_Company='" + firstResponderCompany.Replace("'", "''") + "' WHERE Call_ID=" + callID + ";");
            }
		}

		public String getFirstResponderCompany()
		{
			return firstResponderCompany;
		}

		public void setFirstResponderVehicle(String firstResponderVehicle)
		{
            if (callID > -1)
            {
                this.firstResponderVehicle = firstResponderVehicle;
                StaticDBConnection.NonQueryDatabase("UPDATE [Calls] SET First_Responder_Vehicle='" + firstResponderVehicle.Replace("'", "''") + "' WHERE Call_ID=" + callID + ";");
            }
		}

		public String getFirstResponderVehicle()
		{
			return firstResponderVehicle;
		}

		public void setFirstResponderArrivalTime(DateTime firstResponderArrivalTime)
		{
            if (callID > -1)
            {
                this.firstResponderArrivalTime = firstResponderArrivalTime;
                StaticDBConnection.NonQueryDatabase("UPDATE [Calls] SET Ambulance_Time='" + StaticDBConnection.DateTimeSQLite(firstResponderArrivalTime) + "' WHERE Call_ID=" + callID + ";");
            }
		}

		public DateTime getFirstResponderArrivalTime()
		{
			return firstResponderArrivalTime;
		}

		public void setAmbulanceCompany(String ambulanceCompany)
		{
            if (callID > -1)
            {
                this.ambulanceCompany = ambulanceCompany;
                StaticDBConnection.NonQueryDatabase("UPDATE [Calls] SET Ambulance_Company='" + ambulanceCompany.Replace("'", "''") + "' WHERE Call_ID=" + callID + ";");
            }
		}

		public String getAmbulanceCompany()
		{
			return ambulanceCompany;
		}

		public void setAmbulanceVehicle(String ambulanceVehicle)
		{
			if (callID > -1)
            {
                this.ambulanceVehicle = ambulanceVehicle;
                StaticDBConnection.NonQueryDatabase("UPDATE [Calls] SET Ambulance_Vehicle='" + ambulanceVehicle.Replace("'", "''") + "' WHERE Call_ID=" + callID + ";");
            }
		}

		public String getAmbulanceVehicle()
		{
			return ambulanceVehicle;
		}

		public void setAmbulanceArrivalTime(DateTime ambulanceArrivalTime)
		{
            if (callID > -1)
            {
                this.ambulanceArrivalTime = ambulanceArrivalTime;
                StaticDBConnection.NonQueryDatabase("UPDATE [Calls] SET Ambulance_Time='" + StaticDBConnection.DateTimeSQLite(ambulanceArrivalTime) + "' WHERE Call_ID=" + callID + ";");
            }
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
