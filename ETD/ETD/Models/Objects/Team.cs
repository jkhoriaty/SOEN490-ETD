using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETD.Models.CustomUIObjects;
using ETD.Models.ArchitecturalObjects;

namespace ETD.Models.Objects
{
	public enum Statuses {available, moving, intervening, unavailable};

    public class Team : Observable
    {
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
			NotifyAll();
        }

		//Delete Team
		public void DeleteTeam(Team team)
		{

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
				NotifyAll();
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
				NotifyAll();
                return true;
            }
            return false;
        }

		//Removing equipment from the team list
        public void RemoveEquipment(Equipment equipment)
        {
			equipmentList.Remove(equipment);
			NotifyAll();
		}

		/*
		 * Setters
		 */
        public void setName(String name)
        {
            this.name = name;
			NotifyAll();
        }

        public void setStatus(Statuses s)
        {
            this.status = s;
			NotifyAll();
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
    }
}
