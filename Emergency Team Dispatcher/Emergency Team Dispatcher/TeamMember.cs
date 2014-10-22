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
        public DateTime departure;

        public TeamMember(String name, int training)
        {
            this.name = name;
            this.trainingLevel = training;
            departure = DateTime.Now;
            departure.AddHours(23-departure.Hour);
            departure.AddMinutes(59 - departure.Minute);
            departure.AddSeconds(59 - departure.Second);

        }
        public TeamMember(String name, int training, DateTime dep)
        {
            this.name = name;
            if(training == 0 || training ==1)
                this.trainingLevel = training;
            if (DateTime.Now.CompareTo(dep) < 0)
                this.departure = dep;
        }   
    }
}
