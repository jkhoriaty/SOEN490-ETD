
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETD.CustomObjects.CustomUIObjects;
using ETD.Models.ArchitecturalObjects;
using System.Windows;
using ETD.Services.Database;

/// <summary>
/// Team Model Object, containing TeamMember and Equipment classes
/// </summary>
namespace ETD.Models.Objects
{
    //Possible team status
	public enum Statuses {available, moving, intervening, unavailable};

    [Serializable()]
    public class Team : Observable
	{

		private static List<Observer> observerList = new List<Observer>();//Contains a List of observers

		static List<Team> teamList = new List<Team>();//Contains a list of teams

        //Database reflection variables
        private int teamID;
        private int operationID;

        //Variables used for a team
        String name;
		List<TeamMember> memberList = new List<TeamMember>();
        List<Equipment> equipmentList = new List<Equipment>();
		Statuses status;

		Trainings highestLevelOfTraining = Trainings.firstAid;
		GPSLocation gpsLocation;
		int interventionCount = 0;
		int code1Count = 0;
		int code2Count = 0;

        //Creates a new team
        public Team(String name)
        {
            this.name = name;
			status = Statuses.available;
            if (Operation.currentOperation != null)
            {
                this.operationID = Operation.currentOperation.getID();
            }
            this.teamID = StaticDBConnection.NonQueryDatabaseWithID("INSERT INTO [Teams] (Operation_ID, Name, Status) VALUES (" + operationID + ", '" + name + "', " + 1+(int)status + ")");
            teamList.Add(this);
			ClassModifiedNotification(typeof(Team));
            
        }

		//Deletes a Team
		public static void DeleteTeam(Team team)
		{
			teamList.Remove(team);
            while(team.memberList.Count > 0)
            {
                StaticDBConnection.NonQueryDatabase("UPDATE [Team_Members] SET Disbanded='" + StaticDBConnection.DateTimeSQLite(DateTime.Now) + "' WHERE Volunteer_ID=" + team.getMember(0).getID() + " AND Team_ID=" + team.getID() + ";");
                team.memberList.RemoveAt(0);
                
            }
			ClassModifiedNotification(typeof(Team));
		}

		public void IncrementCode1()
		{
			this.code1Count++;
		}

		public void ResetCodeCount()
		{
			this.code1Count = 0;
			this.code2Count = 0;
		}
		
		public void IncrementCode2()
		{
			this.code2Count++;
		}

		public int getCode1Count()
		{
			return this.code1Count;
		}

		public int getCode2Count()
		{
			return this.code2Count;
		}

        //Checks if the team with the same name was already created
		public static bool TeamListContains(String teamName)
		{
			foreach(Team team in teamList)
			{
				if(team.getName().Equals(teamName))
				{
					return true;
				}
			}
			return false;
		}

		//Adds a maximum of 3 new member to the team
        public bool AddMember(TeamMember member)
        {
            if(memberList.Count <= 2)
            {
                memberList.Add(member);
                StaticDBConnection.NonQueryDatabase("INSERT INTO [Team_Members] (Team_ID, Volunteer_ID, Departure, Joined) VALUES (" + teamID + ", " + member.getID() + ", '" + StaticDBConnection.DateTimeSQLite(member.getDeparture()) + "', '" + StaticDBConnection.DateTimeSQLite(DateTime.Now) + "');");
                if ((int)highestLevelOfTraining < (int)member.getTrainingLevel())
				{
                    highestLevelOfTraining = member.getTrainingLevel();
				}
				InstanceModifiedNotification();
                return true;
            }
            return false;
        }

        private bool RemoveMember(TeamMember member)
        {
            if (memberList.Contains(member))
            {
                memberList.Remove(member);
                StaticDBConnection.NonQueryDatabase("UPDATE [Team_Members] SET Disbanded='" + StaticDBConnection.DateTimeSQLite(DateTime.Now) + "' WHERE Volunteer_ID=" + member.getID() + ";");
                InstanceModifiedNotification();
                return true;
            }
            return false;
        }

		//Adds a maximum of 3 equipments to the team
		public bool AddEquipment(Equipment equipment)
        {
            if (equipmentList.Count < 3)
            {
				equipmentList.Add(equipment);
                StaticDBConnection.NonQueryDatabase("INSERT INTO [Assigned_Equipment] (Equipment_ID, Team_ID, Assigned_Time) VALUES (" + equipment.getID() + ", " + teamID + ", '" + StaticDBConnection.DateTimeSQLite(DateTime.Now) + "');");
				InstanceModifiedNotification();
                return true;
            }
            MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_EquipmentLimit);
            return false;
        }

		//Removing equipment from the team list
        public void RemoveEquipment(Equipment equipment)
        {
            if (equipmentList.Contains(equipment))
            {
                equipmentList.Remove(equipment);
                StaticDBConnection.NonQueryDatabase("UPDATE [Assigned_Equipment] SET Removed_Time='" + StaticDBConnection.DateTimeSQLite(DateTime.Now) + "' WHERE Equipment_ID=" + equipment.getID() + " AND Team_ID=" + teamID + ";");
                InstanceModifiedNotification();
            }
		}

		//Mutators

        //Sets the team name
        public void setName(String name)
        {
            this.name = name;
            StaticDBConnection.NonQueryDatabase("UPDATE [Resources] SET Name='" + name + "', HasArrived='TRUE' WHERE Team_ID=" + teamID + ";");
			InstanceModifiedNotification();
        }

        //Seets the team's status
        public void setStatus(String s)
        {
			this.status = (Statuses)Enum.Parse(typeof(Statuses), s);
			InstanceModifiedNotification();
            
        }

		//Associating the team to GPS locations
		public void setGPSLocation(GPSLocation gpsLocation)
		{
			this.gpsLocation = gpsLocation;
			ClassModifiedNotification(typeof(Team)); //Called so that the TeamPin registers interest in the GPSLocation upon creation
		}

		//Accessors

        //Returns the list of teams
		public static List<Team> getTeamList()
		{
			return teamList;
		}

        //Returns the team's name
        public String getName()
        {
            return this.name;
        }

        //Returns a team's team member
		public TeamMember getMember(int i)
		{
			if(i < memberList.Count)
			{
				return memberList[i];
			}
			else
			{
				return null;
			}
		}

        //Returns the status of a team
        public Statuses getStatus()
        {
            return status;
        }

        //Returns the team's level of training
		public Trainings getHighestLevelOfTraining()
		{
			return highestLevelOfTraining;
		}

        //Returns the number of equipments the team is currently holding
        public int getEquipmentCount()
        {
            return equipmentList.Count;
        }

        //Returns the list of equipments
        public List<Equipment> getEquipmentList()
        {
            return equipmentList;
        }

        //Returns the list of team members
		public List<TeamMember> getMemberList()
		{
			return memberList;
		}
        
        //Returns the Team Object from teamList with team name as an input
        public static Team getTeamObject(String teamName)
        {
            if (teamList.Count > 0)
            {
                foreach (Team team in teamList)
                {
                    if (teamName == team.getName())
                    {
                        return team;
                    }
                }
            }
            return null;
        }

	public void incrementInterventionCount()
	{
		interventionCount++;
	}
	public int getInterventionCount()
	{
		return interventionCount;
	}


        public int getID()
        {
            return teamID;
        }

        public int getParentID()
        {
            return operationID;
        }

		public GPSLocation getGPSLocation()
		{
			return gpsLocation;
		}

        //Swapping method used to reorder teams in the team section
        public static void Swap(Team team, String direction)
        {
            int currentPosition = teamList.IndexOf(team);
            int newPosition;
            if (teamList.Count >= 2)
            {
                if (direction == "up")
                {
                    if (currentPosition == 0)
                    {
                    }
                    else 
                    {
                        newPosition = currentPosition - 1;
                        Team temp = teamList.ElementAt(newPosition);
                        teamList[newPosition] = teamList[currentPosition];
                        teamList[currentPosition] = temp;
                    }
                    
                }
                else if (direction == "down")
                {
                    if (currentPosition == teamList.Count - 1)
                    {
                    }
                    else
                    {
                        newPosition = currentPosition + 1;
                        Team temp = teamList.ElementAt(newPosition);
                        teamList[newPosition] = teamList[currentPosition];
                        teamList[currentPosition] = temp;
                    }                    
                }
            }
        }
    }
}
