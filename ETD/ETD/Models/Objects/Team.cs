using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Objects
{
	public enum Statuses {available, moving, intervening, unavailable};

    public class Team
    {
		public static Dictionary<String, Team> teamsList = new Dictionary<String, Team>();

        String name;
		List<TeamMember> memberList = new List<TeamMember>();
        List<Equipment> equipmentList = new List<Equipment>();
		Statuses status;

		Trainings highestLevelOfTraining = 0;

        public Team(String name)
        {
            this.name = name;
			status = Statuses.available;

			teamsList.Add(name, this);
        }

        public bool addMember(TeamMember mem)
        {
            if(memberList.Count <= 2)
            {
                memberList.Add(mem);
				if((int) highestLevelOfTraining < (int) mem.getTrainingLevel())
				{
					highestLevelOfTraining = mem.getTrainingLevel();
				}
                return true;
            }
            return false;
        }
		
		public bool addEquipment(Equipment equipment)
        {
            if (equipmentList.Count < 3)
            {
				equipmentList.Add(equipment);
                return true;
            }
            return false;
        }

        public void removeEquipment(Equipment equipment)
        {
			equipmentList.Remove(equipment);
		}

		//
		//Getters and Setters
		//
        public void setName(String name)
        {
            this.name = name;
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

        public void setStatus(Statuses s)
        {
            this.status = s;
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
