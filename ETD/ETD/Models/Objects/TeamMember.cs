using ETD.Services.Database;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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

    [Serializable()]
    public class TeamMember
    {
        //Database reflection variables
		private int volunteerID;
        private int teamID;

        //Variables used for a team member
        public String name;
        public Trainings trainingLevel;
        public DateTime departure;
        [field: NonSerialized()]
		private Grid nameGrid;

        //Creates a new team member
        public TeamMember(String name, Trainings training, DateTime departure)
        {
            this.name = name;
            this.trainingLevel = training;
            this.departure = departure;
			try
			{
                using (SQLiteDataReader reader = StaticDBConnection.QueryDatabase("Select Volunteer_ID FROM [Volunteers] WHERE Name='" + name + "'"))
                {
                    reader.Read();
                    this.volunteerID = Convert.ToInt32(reader["Volunteer_ID"].ToString());
                }
                StaticDBConnection.CloseConnection();
				StaticDBConnection.NonQueryDatabase("UPDATE [Volunteers] SET Training_Level=" + (int)this.trainingLevel + " WHERE Volunteer_ID=" + this.volunteerID + ";");
			}
			catch
			{
				this.volunteerID = StaticDBConnection.NonQueryDatabaseWithID("INSERT INTO [Volunteers] (Name, Training_Level) VALUES ('" + name.Replace("'", "''") + "', " + (int)training + ")");
			}
        }

        //Accessors
		//Return volunteer is
        public int getID()
        {
            return volunteerID;
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
