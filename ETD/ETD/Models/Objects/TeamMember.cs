using ETD.Services.Database;
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
        //Database reflection variables
        private int volunteerID;
        private int operationID;

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
            if (Operation.currentOperation != null)
            {
                this.operationID = Operation.currentOperation.getID();
            }
            this.volunteerID = StaticDBConnection.NonQueryDatabaseWithID("INSERT INTO [Volunteers] (Name, Training_Level) VALUES ('" + name + "', " + 1+(int)training + ")");
        }

        //Accessors

        public int getID()
        {
            return volunteerID;
        }

        public int getParentID()
        {
            return operationID;
        }

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
