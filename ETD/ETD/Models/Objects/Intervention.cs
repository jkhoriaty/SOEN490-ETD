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

		//Constructor
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

		//Set intervention as completed
		public void Completed()
		{
			//Set teams to unavailable when the team is completed
			foreach (Team team in getInterveningTeamList())
			{
				team.setStatus("unavailable");
			}
			activeInterventionList.Remove(this);
			completedInterventionList.Add(this);
			isConcludedBool = true;
			ClassModifiedNotification(typeof(Intervention));
		}

		//Return list of active interventions
		public static List<Intervention> getActiveInterventionList()
		{
			return activeInterventionList;
		}

		//Return list of completed interventions
		public static List<Intervention> getCompletedInterventionList()
		{
			return completedInterventionList;
		}

		//Add intervening teams
		public void AddInterveningTeam(Team team)
		{
			foreach (Resource resource in resourceList)
			{
				if (resource.getTeam() == team)
				{
					resource.setIntervening(true);
					InstanceModifiedNotification();
					if (team.getStatus().ToString().Equals("intervening"))
					{
						team.setStatus("moving");
					}
					return;
				}
			}

			//Set up arrival time
			if (firstTeamArrivalTime == DateTime.MinValue)
			{
				firstTeamArrivalTime = DateTime.Now;
			}
			team.incrementInterventionCount();
			resourceList.Add(new Resource(this, team));

			StaticDBConnection.NonQueryDatabase("INSERT INTO [Intervening_Teams] (Intervention_ID, Team_ID, Started_Intervening) VALUES (" + interventionID + ", " + team.getID() + ", '" + StaticDBConnection.DateTimeSQLite(DateTime.Now) + "');");
			InstanceModifiedNotification();
		}

		//Remove a team from an intervention
		public void RemoveInterveningTeam(Team team)
		{
			Resource resourceToRemove = null;
			foreach (Resource resource in resourceList)
			{
				if (resource.getTeam() == team)
				{
					if (!resource.hasArrived())
					{
						resourceToRemove = resource;
					}
					else
					{
						resource.setIntervening(false);
					}
					StaticDBConnection.NonQueryDatabase("UPDATE [Intervening_Teams] SET Stopped_Intervening='" + StaticDBConnection.DateTimeSQLite(DateTime.Now) + "' WHERE Intervention_ID=" + interventionID + " AND Team_ID=" + team.getID() + ";");
				}
			}
			if (resourceToRemove != null)
			{
				resourceList.Remove(resourceToRemove);
			}
			InstanceModifiedNotification();
		}

		//Return the list of teams intervening on an intervention
		public List<Team> getInterveningTeamList()
		{
			List<Team> interveningTeams = new List<Team>();
			foreach (Resource resource in resourceList)
			{
				if (resource.getIntervening() == true)
				{
					interveningTeams.Add(resource.getTeam());
				}
			}
			return interveningTeams;
		}

		//Set the arrival time of a team on an intervention
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

		//Checks if the intervention is still active
		public bool IsActive()
		{
			return activeInterventionList.Contains(this);
		}

		//Checks if the intervention is completed
		public bool IsCompleted()
		{
			return completedInterventionList.Contains(this);
		}

		//Called if a resource is modified
		public void ResourceModified()
		{
			InstanceModifiedNotification();
		}

		//Add active interventions to the list of active interventions
		public static void AddActiveIntervention(Intervention intervention)
		{
			activeInterventionList.Add(intervention);
			ClassModifiedNotification(typeof(Intervention));
		}


		//Add the completed intervention to the completed intervention list
		public static void AddCompletedIntervention(Intervention intervention)
		{
			completedInterventionList.Add(intervention);
			ClassModifiedNotification(typeof(Intervention));
		}

		//Accessors

		//Returns intervention number
		public int getInterventionNumber()
		{
			return interventionNumber;
		}

		//Returns arrival time
		public DateTime getFirstTeamArrivalTime()
		{
			return this.firstTeamArrivalTime;
		}

		//Returns return time of call
		public DateTime getTimeOfCall()
		{
			return timeOfCall;
		}

		//Returns return caller's name
		public String getCallerName()
		{
			return callerName;
		}

		//Returns location
		public String getLocation()
		{
			return location;
		}

		//Returns intervention code
		public int getCode()
		{
			return this.code;
		}

		//Returns gender
		public String getGender()
		{
			return this.gender;
		}

		//Returns age
		public String getAge()
		{
			return this.age;
		}

		//Returns nature of call
		public String getNatureOfCall()
		{
			return natureOfCall;
		}

		//Returns chief complain
		public String getChiefComplaint()
		{
			return chiefComplaint;
		}

		//Returns additional chief complaint
		public String getOtherChiefComplaint()
		{
			return otherChiefComplaint;
		}

		//Returns resource list
		public List<Resource> getResourceList()
		{
			return resourceList;
		}

		//Returns ABC
		public ABC getABC()
		{
			return abc;
		}

		//Returns additional info
		public InterventionAdditionalInfo getAdditionalInfo(int position)
		{
			return this.additionalInfo[position];
		}

		//Returns list of additional info
		public InterventionAdditionalInfo[] getAllAdditionalInfo()
		{
			return this.additionalInfo;
		}

		//Returns additional conclusion info
		public String getConclusionAdditionalInfo()
		{
			return conclusionAdditionalInfo;
		}

		//Returns conclusion info
		public String getConclusion()
		{
			return conclusion;
		}

		//Returns conclusion time
		public DateTime getConclusionTime()
		{
			return conclusionTime;
		}

		//Returns meeting point
		public String getMeetingPoint()
		{
			return meetingPoint;
		}

		//Returns 911 call time
		public DateTime getCall911Time()
		{
			return call911Time;
		}

		//Returns first responder company
		public String getFirstResponderCompany()
		{
			return firstResponderCompany;
		}

		//Returns first responder vehicle
		public String getFirstResponderVehicle()
		{
			return firstResponderVehicle;
		}

		//Returns first responder arrival time
		public DateTime getFirstResponderArrivalTime()
		{
			return firstResponderArrivalTime;
		}

		//Returns ambulance company
		public String getAmbulanceCompany()
		{
			return ambulanceCompany;
		}

		//Returns ambulance vehicle
		public String getAmbulanceVehicle()
		{
			return ambulanceVehicle;
		}

		//Returns intervention id
		public int getID()
		{
			return interventionID;
		}

		//Returns ambulance arrival time
		public DateTime getAmbulanceArrivalTime()
		{
			return ambulanceArrivalTime;
		}

		//Mutators
		//Set intervntion number
		public void setInterventionNumber(int interventionNumber)
		{
			this.interventionNumber = interventionNumber;
		}
		//Set  time of call
		public void setTimeOfCall(DateTime timeOfCall)
		{
			this.timeOfCall = timeOfCall;
			InstanceModifiedNotification();
		}
		//Set caller name
		public void setCallerName(String callerName)
		{
			this.callerName = callerName;

			StaticDBConnection.NonQueryDatabase("UPDATE [Interventions] SET Caller='" + callerName.Replace("'", "''") + "' WHERE Intervention_ID=" + interventionID + ";");
		}

		//Set  location
		public void setLocation(String location)
		{
			this.location = location;
			StaticDBConnection.NonQueryDatabase("UPDATE [Interventions] SET Location='" + location.Replace("'", "''") + "' WHERE Intervention_ID=" + interventionID + ";");
		}

		//Set nature of call
		public void setNatureOfCall(String natureOfCall)
		{
			this.natureOfCall = natureOfCall;
			StaticDBConnection.NonQueryDatabase("UPDATE [Interventions] SET Nature_Of_Call='" + natureOfCall.Replace("'", "''") + "' WHERE Intervention_ID=" + interventionID + ";");
		}

		//Set intervention code
		public void setCode(int code)
		{
			this.code = code;
			StaticDBConnection.NonQueryDatabase("UPDATE [Interventions] SET Code=" + code + " WHERE Intervention_ID=" + interventionID + ";");
		}

		//Set gender
		public void setGender(String gender)
		{
			this.gender = gender;
			StaticDBConnection.NonQueryDatabase("UPDATE [Interventions] SET Gender='" + gender.Replace("'", "''") + "' WHERE Intervention_ID=" + interventionID + ";");
		}

		//Set age
		public void setAge(String age)
		{
			this.age = age;
			if (age.Length > 0)
			{
				StaticDBConnection.NonQueryDatabase("UPDATE [Interventions] SET Age=" + int.Parse(age) + " WHERE Intervention_ID=" + interventionID + ";");
			}
		}

		//Set chief complaint
		public void setChiefComplaint(String chiefComplaint)
		{
			this.chiefComplaint = chiefComplaint;
			StaticDBConnection.NonQueryDatabase("UPDATE [Interventions] SET Chief_Complaint='" + StaticDBConnection.GetResourceName(chiefComplaint).Replace("'", "''") + "' WHERE Intervention_ID=" + interventionID + ";");
		}

		//Set  other chief complaint
		public void setOtherChiefComplaint(String otherChiefComplaint)
		{
			this.otherChiefComplaint = otherChiefComplaint;
			StaticDBConnection.NonQueryDatabase("UPDATE [Interventions] SET Other_Chief_Complaint='" + otherChiefComplaint.Replace("'", "''") + "' WHERE Intervention_ID=" + interventionID + ";");
		}

		//Set abc
		public void setABC(ABC abc)
		{
			this.abc = abc;
			StaticDBConnection.NonQueryDatabase("UPDATE [ABCs] SET Intervention_ID=" + interventionID + " WHERE ABC_ID=" + abc.getID() + ";");
		}

		//Set additional info
		public void setAdditionalInfo(int position, InterventionAdditionalInfo info)
		{
			this.additionalInfo[position] = info;
			StaticDBConnection.NonQueryDatabase("UPDATE [Additional_Informations] SET Intervention_ID=" + interventionID + " WHERE Additional_Info_ID=" + info.getID() + ";");
		}

		//Set conclusion
		public void setConclusion(String conclusion)
		{
			this.conclusion = conclusion;
			StaticDBConnection.NonQueryDatabase("UPDATE [Interventions] SET Conclusion='" + StaticDBConnection.GetResourceName(conclusion).Replace("'", "''") + "' WHERE Intervention_ID=" + interventionID + ";");
		}

		//Set additional conclusion info
		public void setConclusionAdditionalInfo(String additionalInfo)
		{
			this.conclusionAdditionalInfo = additionalInfo;
			StaticDBConnection.NonQueryDatabase("UPDATE [Interventions] SET Conclusion_Info='" + additionalInfo.Replace("'", "''") + "' WHERE Intervention_ID=" + interventionID + ";");
		}

		//Set conclusion time
		public void setConclusionTime(DateTime conclusionTime)
		{
			this.conclusionTime = conclusionTime;
			StaticDBConnection.NonQueryDatabase("UPDATE [Interventions] SET Conclusion_Time='" + StaticDBConnection.DateTimeSQLite(conclusionTime) + "' WHERE Intervention_ID=" + interventionID + ";");
		}

		//Set 911 call time
		public void setCall911Time(DateTime call911Time)
		{
			this.callID = StaticDBConnection.NonQueryDatabaseWithID("INSERT INTO [Calls] (Intervention_ID) VALUES (" + this.interventionID + ");");
			this.call911Time = call911Time;
			StaticDBConnection.NonQueryDatabase("UPDATE [Calls] SET Call_Time='" + StaticDBConnection.DateTimeSQLite(call911Time) + "' WHERE Call_ID=" + callID + ";");

		}

		//Set meeting point
		public void setMeetingPoint(String meetingPoint)
		{
			this.meetingPoint = meetingPoint;
			StaticDBConnection.NonQueryDatabase("UPDATE [Calls] SET Meeting_Point='" + meetingPoint.Replace("'", "''") + "' WHERE Call_ID=" + callID + ";");

		}

		//Set first responder company
		public void setFirstResponderCompany(String firstResponderCompany)
		{
			this.firstResponderCompany = firstResponderCompany;
			StaticDBConnection.NonQueryDatabase("UPDATE [Calls] SET First_Responder_Company='" + firstResponderCompany.Replace("'", "''") + "' WHERE Call_ID=" + callID + ";");
		}

		//Set first responder vehicle
		public void setFirstResponderVehicle(String firstResponderVehicle)
		{
			this.firstResponderVehicle = firstResponderVehicle;
			StaticDBConnection.NonQueryDatabase("UPDATE [Calls] SET First_Responder_Vehicle='" + firstResponderVehicle.Replace("'", "''") + "' WHERE Call_ID=" + callID + ";");
		}

		//Set first responder arrival time
		public void setFirstResponderArrivalTime(DateTime firstResponderArrivalTime)
		{
			this.firstResponderArrivalTime = firstResponderArrivalTime;
			StaticDBConnection.NonQueryDatabase("UPDATE [Calls] SET Ambulance_Time='" + StaticDBConnection.DateTimeSQLite(firstResponderArrivalTime) + "' WHERE Call_ID=" + callID + ";");
		}

		//Set ambulance company
		public void setAmbulanceCompany(String ambulanceCompany)
		{
			this.ambulanceCompany = ambulanceCompany;
			StaticDBConnection.NonQueryDatabase("UPDATE [Calls] SET Ambulance_Company='" + ambulanceCompany.Replace("'", "''") + "' WHERE Call_ID=" + callID + ";");
		}

		//Set ambulance vehicle
		public void setAmbulanceVehicle(String ambulanceVehicle)
		{
			this.ambulanceVehicle = ambulanceVehicle;
			StaticDBConnection.NonQueryDatabase("UPDATE [Calls] SET Ambulance_Vehicle='" + ambulanceVehicle.Replace("'", "''") + "' WHERE Call_ID=" + callID + ";");
		}

		//Set ambulance arrival time
		public void setAmbulanceArrivalTime(DateTime ambulanceArrivalTime)
		{
			this.ambulanceArrivalTime = ambulanceArrivalTime;
			StaticDBConnection.NonQueryDatabase("UPDATE [Calls] SET Ambulance_Time='" + StaticDBConnection.DateTimeSQLite(ambulanceArrivalTime) + "' WHERE Call_ID=" + callID + ";");
		}

	}
}
