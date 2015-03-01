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
        private String consciousness;
        private bool disoriented;
        private String airways;
        private String breathing;
        private int breathingFrequency;
        private String circulation;
        private int circulationFrequency;

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

        public String getConsciousness()
        {
            return this.consciousness;
        }

        public void setConsciousness(String consciousness)
        {
            this.consciousness = consciousness;
        }

        public bool getDisoriented()
        {
            return this.disoriented;
        }

        public void setDisoriented(bool disoriented)
        {
            this.disoriented = disoriented;
        }

        public String getAirways()
        {
            return this.airways;
        }

        public void setAirways(String airways)
        {
            this.airways = airways;
        }

        public String getBreathing()
        {
            return this.breathing;
        }

        public void setBreathing(String breathing)
        {
            this.breathing = breathing;
        }

        public int getBreathingFrequency()
        {
            return this.breathingFrequency;
        }

        public void setBreathingFrequency(int breathingFrequency)
        {
            this.breathingFrequency = breathingFrequency;
        }

        public String getCirculation()
        {
            return this.circulation;
        }


        public void setCirculation(String circulation)
        {
            this.circulation = circulation;
        }
        public int getCirculationFrequency()
        {
            return this.circulationFrequency;
        }

        public void setCirculationFrequency(int circulationFrequency)
        {
            this.circulationFrequency = circulationFrequency;
        }
    }
}