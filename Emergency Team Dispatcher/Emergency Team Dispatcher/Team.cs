using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency_Team_Dispatcher
{
    class Team
    {
        String name;

        int trainingLevel = 0; //0: First Aid, 1: First Responder, 2: Medicine
        TeamMember[] members;
        int memberCount = 0;
        DateTime departure;

        public Team()
        {
            name = "Alpha";
        }
        public Team(String name)
        {
            this.name = name;
        }

        public bool addMember(TeamMember mem)
        {
            if(memberCount < 2)
            {
                members[memberCount] = mem;
                memberCount++;
                if(this.trainingLevel < mem.trainingLevel)
                    this.trainingLevel = mem.trainingLevel;
                if(this.departure.CompareTo(mem.departure) > 0)
                    this.departure = mem.departure;
                return true;
            }
            return false;
        }

        public void setName(String name)
        {
            this.name = name;
        }
        public String getName()
        {
            return this.name;
        }
        //Generate shape to represent team on screen
        public void draw()
        {

        }
    }
}
