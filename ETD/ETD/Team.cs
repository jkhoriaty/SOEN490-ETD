using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD
{
    public class Team
    {
        String name;
        TeamMember[] members;
        Equipment[] equipments;
        int EquipmentCount = 0;
        int memberCount = 0;
        bool availability = true;

        public Team()
        {
            name = "Alpha";
            members = new TeamMember[400];
            equipments =  new Equipment[5];
      
        }
        public Team(String name)
        {
            this.name = name;
            members = new TeamMember[400];
            equipments = new Equipment[5];
        }

        public bool addMember(TeamMember mem)
        {
            if(memberCount <= 2)
            {
                members[memberCount] = mem;
                memberCount++;
                return true;
            }
            return false;
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

        //Generate shape to represent team on screen
        public String draw(int HighestlevelOftraining, Team team)
        {
            String relpath;
            switch (HighestlevelOftraining)
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

        //to be moved to equipmentupdate form
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

        //to be moved to equipmentupdate form
        public void removeEquipment(equipments equipment)
        {
			for ( int i=0; i < equipments.Length; i++)
            {
				if (equipment == equipments[i].getEquipmentName())
                    equipments[i] = null;
                EquipmentCount--;
            }
		}

        public int getEquipmentCount()
        {
            return EquipmentCount;
        }
    }
}
