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
        public String departure;

        public TeamMember(String name, int training, String dep)
        {
            this.name = name;
            this.trainingLevel = training;
            this.departure = dep;
            //departure.AddHours(23-departure.Hour);
            //departure.AddMinutes(59 - departure.Minute);
            //departure.AddSeconds(59 - departure.Second);

        }
    }
}
