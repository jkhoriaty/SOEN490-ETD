using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ETD.Models.Objects
{
    /// <summary>
    /// Team Member Model Object, contained by Team Class
    /// </summary>

    //Level of trainings
	public enum Trainings {firstAid, firstResponder, medicine};

    public class TeamMember
    {
        //Variables used for a team member
        public String name;
        public Trainings trainingLevel;
        public DateTime departure;
		private Grid nameGrid;

        //Creates a new team member
        public TeamMember(String name, Trainings training, DateTime departure)
        {
            this.name = name;
            this.trainingLevel = training;
            this.departure = departure;
        }

        //Accessors

        //Returns team member departure time
        public DateTime getDeparture()
        {
            return departure;
        }

        //Returns the team member's name
		public String getName()
		{
			return name;
		}

        //Returns the team member's level of training
		public Trainings getTrainingLevel()
		{
			return trainingLevel;
		}

        //Returns the team member's grid name, used to setup a new team on the team section
		public Grid getNameGrid()
		{
			return nameGrid;
		}

        //Mutators

        //Sets the name of a team member's grid, used to setup a new team on the team section
		public void setNameGrid(Grid gd)
		{
			this.nameGrid = gd;
		}
    }
}
