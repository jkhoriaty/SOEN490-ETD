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
        public void draw()
        {

        }
       

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

        }
        public String loadEquipment( Team team, int i)
       {
      
           String RelativePath;
        
               switch(team.getEquipment(i).getEquipmentName()){
                   case "ambulance cart":
                       RelativePath = @"\Icons\AmbulanceCart.png";
                       return RelativePath;
                      
                   case "mounted stretcher":
                       RelativePath = @"\Icons\MountedStretcher.png";
                       return RelativePath;
                    
                   case "wheel chair":
                       RelativePath = @"\Icons\WheelChair.png";
                       return RelativePath;
                     
                   case "transport stretcher":
                       RelativePath = @"\Icons\TransportStretcher.png";
                       return RelativePath;
                    
                   case "sitting cart":
                       RelativePath = @"\Icons\SittingCart.png";
                       return RelativePath;
                      
                   case "epipen":
                       RelativePath = @"\Icons\Epipen.png";
                       return RelativePath;

                   default:
                       return "";
               }

       }
        //to be moved to equipmentupdate form
        public void removeEquipment(String e)
        {
            String equip = e;
          for ( int i=0; i < equipments.Length; i++)
            {
                if (equip == equipments[i].getEquipmentName())
                    equipments[i] = null;
                EquipmentCount--;
                
            }
         }

        //to be moved to equipmentupdate form
        public bool addEquipment(Equipment EquipmentName)
        {
            if (EquipmentCount < 5)
            {

                equipments[EquipmentCount] = EquipmentName;

                EquipmentCount++;
                return true;
            }
            return false;
        }
    }
}
