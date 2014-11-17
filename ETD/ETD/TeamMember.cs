using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETD
{
    public class TeamMember
    {
        public String name;
        public int trainingLevel; //0: First Aid, 1: First Responder, 2: Medicine
        public DateTime departure;

        public TeamMember(String name, int training, DateTime departure)
        {
            this.name = name;
            this.trainingLevel = training;
            this.departure = departure;
        }
    }
}
