using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Objects
{
    /// <summary>
    /// Airways Breathing Circulation Model Object
    /// </summary>

    public class ABC
    {
        //Database reflection variables
        private int abcID;
        private int interventionID;

        //ABC objects
        private String consciousness;
        private bool disoriented;
        private String airways;
        private String breathing;
        private int breathingFrequency;
        private String circulation;
        private int circulationFrequency;

        //Set default values for all ABC objects
        public ABC()
        {
            this.consciousness = "notSet";
            this.disoriented = false;
            this.airways = "notSet";
            this.breathing = "notSet";
            this.breathingFrequency = -1;
            this.circulation = "notSet"; 
            this.circulationFrequency = -1;
        }

        //User inputs default values for all ABC objects
        public ABC(String consciousness, bool disoriented, String airways, String breathing, int breathingFrequency, String circulation, int circulationFrequency)
        {
            this.consciousness = consciousness;
            this.disoriented = disoriented;
            this.airways = airways;
            this.breathing = breathing;
            this.breathingFrequency = breathingFrequency;
            this.circulation = circulation;
            this.circulationFrequency = circulationFrequency;
        }

        //Accessor methods
        public int getID()
        {
            return abcID;
        }

        public int getParentID()
        {
            return interventionID;
        }
        public String getConsciousness()
        {
            return this.consciousness;
        }

        public bool getDisoriented()
        {
            return this.disoriented;
        }

        public String getAirways()
        {
            return this.airways;
        }

        public String getBreathing()
        {
            return this.breathing;
        }

        public int getBreathingFrequency()
        {
            return this.breathingFrequency;
        }

        public String getCirculation()
        {
            return this.circulation;
        }

        public int getCirculationFrequency()
        {
            return this.circulationFrequency;
        }

        //Mutator methods
        public void setConsciousness(String consciousness)
        {
            this.consciousness = consciousness;
        }

        public void setDisoriented(bool disoriented)
        {
            this.disoriented = disoriented;
        }

        public void setAirways(String airways)
        {
            this.airways = airways;
        }

        public void setBreathing(String breathing)
        {
            this.breathing = breathing;
        }

        public void setBreathingFrequency(int breathingFrequency)
        {
            this.breathingFrequency = breathingFrequency;
        }

        public void setCirculation(String circulation)
        {
            this.circulation = circulation;
        }
    
        public void setCirculationFrequency(int circulationFrequency)
        {
            this.circulationFrequency = circulationFrequency;
        }
    }
}