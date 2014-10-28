using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emergency_Team_Dispatcher
{
    public class TeamMember
    {
        public String name;
        public int trainingLevel; //0: First Aid, 1: First Responder, 2: Medicine
        public string departure;

        public TeamMember(String name, int training, string departure)
        {
            this.name = name;
            this.trainingLevel = training;
            this.departure = departure;
        }

        public String getName()
        {
            return this.name;
        }
        public int getTrainingLevel()
        {
            return this.trainingLevel;
        }
    }
}
