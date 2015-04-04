using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ETD.Models.Objects
{
    public class Shift 
    {
        private static List<Shift> shiftsList = new List<Shift>();//Contains a list of shifts

        private String team;
        private String sector;
        private DateTime teamsectorStartTimeMap;
        private int shiftDuration;
        private Shift[] shiftInfo = new Shift[100];

        //Creates a new shift
        public Shift(String team, String sector, int shiftDuration) 
        {
            this.team = team;
            this.sector = sector;
            //this.teamsectorStartTimeMap = teamsectorStartTimeMap;
            this.shiftDuration = shiftDuration;

            shiftsList.Add(this);
        }

        //Accessors

        //Returns the list of shifts
        public  List<Shift> getShiftsList()
        {
            return shiftsList;
        }

        public String getTeamName()
        {
            return team;
        }

        public String getSector()
        {
            return sector;
        }

        //Returns the shift's information
        public Shift[] getShiftInfo()
        {
            return shiftInfo;
        }

        //Mutators

        //Sets the shift's information
        public void setShiftInfo(int position, Shift shiftInfo)
        {
            this.shiftInfo[position] = shiftInfo;
        }

		public int getShiftDuration()
		{
			return shiftDuration;
		}
    }
}
