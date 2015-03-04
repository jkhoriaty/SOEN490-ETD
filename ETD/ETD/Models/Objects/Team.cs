using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETD.CustomObjects.CustomUIObjects;
using ETD.Models.ArchitecturalObjects;
using System.Windows;

/// <summary>
/// Team Model Object, containing TeamMember and Equipment classes
/// </summary>
namespace ETD.Models.Objects
{
    //Possible team status
	public enum Statuses {available, moving, intervening, unavailable};

    public class Team : Observable
	{
		private static List<Observer> observerList = new List<Observer>();//Contains a List of observers

		static List<Team> teamList = new List<Team>();//Contains a list of teams

        //Variables used for a team
        String name;
		List<TeamMember> memberList = new List<TeamMember>();
        List<Equipment> equipmentList = new List<Equipment>();
		Statuses status;
		Trainings highestLevelOfTraining = 0;

        //Creates a new team
        public Team(String name)
        {
            this.name = name;
			status = Statuses.available;
			teamList.Add(this);
			ClassModifiedNotification(typeof(Team));
        }

		//Deletes a Team
		public static void DeleteTeam(Team team)
		{
			teamList.Remove(team);
			ClassModifiedNotification(typeof(Team));
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
                if ((int)highestLevelOfTraining < (int)member.getTrainingLevel())
				{
                    highestLevelOfTraining = member.getTrainingLevel();
				}
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
				InstanceModifiedNotification();
                return true;
            }
            MessageBox.Show("Can not have more than three equipments");
            return false;
        }

		//Removing equipment from the team list
        public void RemoveEquipment(Equipment equipment)
        {

			equipmentList.Remove(equipment);
			InstanceModifiedNotification();
		}

		//Mutators

        //Sets the team name
        public void setName(String name)
        {
            this.name = name;
			InstanceModifiedNotification();
        }

        //Seets the team's status
        public void setStatus(String s)
        {
			this.status = (Statuses)Enum.Parse(typeof(Statuses), s);
			InstanceModifiedNotification();
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
