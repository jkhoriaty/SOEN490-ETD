using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETD.CustomObjects.CustomUIObjects;
using ETD.Models.ArchitecturalObjects;
using System.Windows;

/// <summary>
/// Team Model Object
/// </summary>
namespace ETD.Models.Objects
{
	public enum Statuses {available, moving, intervening, unavailable};

    public class Team : Observable
	{
		private static List<Observer> observerList = new List<Observer>();

		static List<Team> teamList = new List<Team>();

        String name;
		List<TeamMember> memberList = new List<TeamMember>();
        List<Equipment> equipmentList = new List<Equipment>();
		Statuses status;

		Trainings highestLevelOfTraining = 0;

        public Team(String name)
        {
            this.name = name;
			status = Statuses.available;

			teamList.Add(this);
			ClassModifiedNotification(typeof(Team));
        }

		//Delete Team
		public static void DeleteTeam(Team team)
		{
			teamList.Remove(team);
			ClassModifiedNotification(typeof(Team));
		}

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

		//Adding a maximum of 3 members to the team
        public bool AddMember(TeamMember mem)
        {
            if(memberList.Count <= 2)
            {
                memberList.Add(mem);
				if((int) highestLevelOfTraining < (int) mem.getTrainingLevel())
				{
					highestLevelOfTraining = mem.getTrainingLevel();
				}
				InstanceModifiedNotification();
                return true;
            }
            return false;
        }

		//Adding a maximum of 3 equipments to the team
		public bool AddEquipment(Equipment equipment)
        {
            if (equipmentList.Count < 3)
            {
				equipmentList.Add(equipment);
				InstanceModifiedNotification();
                return true;
            }
            return false;
        }

		//Removing equipment from the team list
        public void RemoveEquipment(Equipment equipment)
        {
			equipmentList.Remove(equipment);
			InstanceModifiedNotification();
		}

		/*
		 * Setters
		 */
        public void setName(String name)
        {
            this.name = name;
			InstanceModifiedNotification();
        }

        public void setStatus(String s)
        {
			this.status = (Statuses)Enum.Parse(typeof(Statuses), s);
			InstanceModifiedNotification();
        }

		/*
		 * Getters
		 */
		public static List<Team> getTeamList()
		{
			return teamList;
		}

        public String getName()
        {
            return this.name;
        }

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

        public Statuses getStatus()
        {
            return status;
        }

		public Trainings getHighestLevelOfTraining()
		{
			return highestLevelOfTraining;
		}

        public int getEquipmentCount()
        {
            return equipmentList.Count;
        }

		public List<TeamMember> getMemberList()
		{
			return memberList;
		}

        //swapping method to swap teams around inside teamList up or down
        internal void Swap(Team team, String direction)
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
