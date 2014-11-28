using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETD.Models
{
	public enum trainings {firstAid, firstResponder, medicine};

    public class TeamMember
    {
        public String name;
        public trainings trainingLevel;
        public DateTime departure;

        public TeamMember(String name, trainings training, DateTime departure)
        {
            this.name = name;
            this.trainingLevel = training;
            this.departure = departure;
        }

		public String getName()
		{
			return name;
		}

		public trainings getTrainingLevel()
		{
			return trainingLevel;
		}
    }
}
