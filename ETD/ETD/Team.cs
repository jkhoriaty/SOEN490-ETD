using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD
{
    public class Team
    {
		public static List<Team> teams = new List<Team>();

        String name;
        TeamMember[] members;
        Equipment[] equipments;
        int EquipmentCount = 0;
        int memberCount = 0;
        bool availability = true;
		int highestLevelOfTraining = 0;

        public Team(String name)
        {
            this.name = name;
            members = new TeamMember[3];
            equipments = new Equipment[5];

			//Adding the team to the list of teams
			teams.Add(this);
        }

        public bool addMember(TeamMember mem)
        {
            if(memberCount <= 2)
            {
                members[memberCount] = mem;
				if(highestLevelOfTraining < (int) mem.getTrainingLevel())
				{
					highestLevelOfTraining = (int) mem.getTrainingLevel();
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

		public int getHighestLevelOfTraining()
		{
			return highestLevelOfTraining;
		}

        //Generate shape to represent team on screen
        public String draw(Team team)
        {
            String relpath;
            switch (highestLevelOfTraining)
            {
                case 0:
                    if (availability == true)
                    {
                        relpath = @"\Icons\FirstAid_available.png";

                        return relpath;
                    }
                    else 
                    {
                        relpath = @"\Icons\FirstAid_busy.png";
                        return relpath;
                    }

                case 1:
                    if (availability == true)
                    {
                        relpath = @"\Icons\FirstResponder_available.png";

                        return relpath;
                    }
                    else
                    {
                        relpath = @"\Icons\FirstResponder_busy.png";
                        return relpath;
                    }


                case 2:
                    if (availability == true)
                    {
                        relpath = @"\Icons\Medicine_available.png";

                        return relpath;
                    }
                    else
                    {
                        relpath = @"\Icons\Medicine_busy.png";
                        return relpath;
                    }

                default:
                    return "";

            }
        }
       
		/*
        public Equipment getEquipment(int i)
        {
            if (i < EquipmentCount)
            {
                return equipments[i];
            }
            else
            {
                return null;
            }

        }*/
    }
}
