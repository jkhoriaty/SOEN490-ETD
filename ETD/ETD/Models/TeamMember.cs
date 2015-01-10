using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETD.Models
{
	public enum Trainings {firstAid, firstResponder, medicine};

    public class TeamMember
    {
        public String name;
        public Trainings trainingLevel;
        public DateTime departure;

        public TeamMember(String name, Trainings training, DateTime departure)
        {
            this.name = name;
            this.trainingLevel = training;
            this.departure = departure;
        }

        public DateTime getDeparture()
        {
            return departure;
        }

		public String getName()
		{
			return name;
		}

		public Trainings getTrainingLevel()
		{
			return trainingLevel;
		}
    }
}
