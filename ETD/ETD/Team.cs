﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD
{
	public enum statuses {available, moving, intervening, busy};

    public class Team
    {
		public static Dictionary<String, Team> teams = new Dictionary<String, Team>();

        String name;
        TeamMember[] members;
        Equipment[] equipments;
		statuses status;

        int EquipmentCount = 0;
        int memberCount = 0;
        bool availability = true;
		trainings highestLevelOfTraining = 0;

        public Team(String name)
        {
            this.name = name;
            members = new TeamMember[3];
            equipments = new Equipment[5];
			status = statuses.available;

			//Adding team to the dictionnary of teams
			teams.Add(name, this);
        }

        public bool addMember(TeamMember mem)
        {
            if(memberCount <= 2)
            {
                members[memberCount] = mem;
				if((int) highestLevelOfTraining < (int) mem.getTrainingLevel())
				{
					highestLevelOfTraining = mem.getTrainingLevel();
				}
                memberCount++;
                return true;
            }
            return false;
        }
		
		public bool addEquipment(Equipment equipment)
        {
            if (EquipmentCount < 3)
            {
				equipments[EquipmentCount] = equipment;

                EquipmentCount++;
                return true;
            }
            return false;
        }

        public void removeEquipment(equipments equipment)
        {
			for ( int i=0; i < equipments.Length; i++)
            {
				if (equipment == equipments[i].getEquipmentName())
                    equipments[i] = null;
                EquipmentCount--;
            }
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
			if(i < memberCount)
			{
				return members[i];
			}
			else
			{
				return null;
			}
		}

		public trainings getHighestLevelOfTraining()
		{
			return highestLevelOfTraining;
		}

        public int getEquipmentCount()
        {
            return EquipmentCount;
        }
    }
}
