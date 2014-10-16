using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emergency_Team_Dispatcher
{
    class TeamMember
    {
        String name;
        public int trainingLevel; //0: First Aid, 1: First Responder, 2: Medicine
        public String departureTime;

        public TeamMember(String name, int training, String departureTime)
        {
            this.name = name;
            this.trainingLevel = training;
            this.departureTime = departureTime;

        }
    }
}
