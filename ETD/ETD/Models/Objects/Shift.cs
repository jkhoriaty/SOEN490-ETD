using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Objects
{
    class Shift
    {
        private static List<Shift> shiftsList = new List<Shift>();//Contains a list of shifts

        private String team;
        private String sector;
        private DayOfWeek date;
        private String sectorStatus;
        private DateTime teamsectorStartTimeMap;
        private DateTime teamsectorEndTimeMap;
        private int shiftDuration;
        private Shift[] shiftInfo = new Shift[100];

        //Creates a new shift
        public Shift(String team, String sector, DayOfWeek date, String sectorStatus, DateTime teamsectorStartTimeMap, DateTime teamsectorEndTimeMap, int shiftDuration) 
        {
            this.team = team;
            this.sector = sector;
            this.date = date;
            this.sectorStatus = sectorStatus;
            this.teamsectorStartTimeMap = teamsectorStartTimeMap;
            this.teamsectorEndTimeMap = teamsectorEndTimeMap;
            this.shiftDuration = shiftDuration;

            shiftsList.Add(this);
        }

        //Accessors

        //Returns the list of shifts
        public static List<Shift> getShiftsList()
        {
            return shiftsList;
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
    }
}
