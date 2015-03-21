using ETD.Services.Database;
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

            this.abcID = StaticDBConnection.NonQueryDatabaseWithID("INSERT INTO [ABCs] (Intervention_ID) VALUES (" + interventionID + ");");
        }

        public ABC(Intervention intervention)
        {
            this.consciousness = "notSet";
            this.disoriented = false;
            this.airways = "notSet";
            this.breathing = "notSet";
            this.breathingFrequency = -1;
            this.circulation = "notSet";
            this.circulationFrequency = -1;
            this.interventionID = intervention.getID();

            this.abcID = StaticDBConnection.NonQueryDatabaseWithID("INSERT INTO [ABCs] (Intervention_ID) VALUES (" + interventionID + ");");
        }

        //User inputs default values for all ABC objects
        public ABC(int intervention, String consciousness, bool disoriented, String airways, String breathing, int breathingFrequency, String circulation, int circulationFrequency)
        {
            this.consciousness = consciousness;
            this.disoriented = disoriented;
            this.airways = airways;
            this.breathing = breathing;
            this.breathingFrequency = breathingFrequency;
            this.circulation = circulation;
            this.circulationFrequency = circulationFrequency;
            this.interventionID = intervention;
            this.abcID = StaticDBConnection.NonQueryDatabaseWithID("INSERT INTO [ABCs] (Intervention_ID, Consciousness, Disoriented, Airways, Breathing, Breathing_Frequency, Circulation, Circulation_Frequency) VALUES (" + interventionID + ", '" + consciousness + "', " + disoriented + ", '" + airways + "', '" + breathing + "', " + breathingFrequency + ", '" + circulation + "', " + circulationFrequency + ");");
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
            StaticDBConnection.NonQueryDatabase("UPDATE [ABCs] SET Consciousness='" + consciousness + "' WHERE Intervention_ID=" + abcID + ";");
        }

        public void setDisoriented(bool disoriented)
        {
            this.disoriented = disoriented;
            StaticDBConnection.NonQueryDatabase("UPDATE [ABCs] SET Disoriented='" + disoriented + "' WHERE Intervention_ID=" + abcID + ";");
        }

        public void setAirways(String airways)
        {
            this.airways = airways;
            StaticDBConnection.NonQueryDatabase("UPDATE [ABCs] SET Airways='" + airways + "' WHERE Intervention_ID=" + abcID + ";");
        }

        public void setBreathing(String breathing)
        {
            this.breathing = breathing;
            StaticDBConnection.NonQueryDatabase("UPDATE [ABCs] SET Breathing='" + breathing + "' WHERE Intervention_ID=" + abcID + ";");
        }

        public void setBreathingFrequency(int breathingFrequency)
        {
            this.breathingFrequency = breathingFrequency;
            StaticDBConnection.NonQueryDatabase("UPDATE [ABCs] SET Breathing_Frequency=" + breathingFrequency + " WHERE Intervention_ID=" + abcID + ";");
        }

        public void setCirculation(String circulation)
        {
            this.circulation = circulation;
            StaticDBConnection.NonQueryDatabase("UPDATE [ABCs] SET Circulation='" + circulation + "' WHERE Intervention_ID=" + abcID + ";");
        }
    
        public void setCirculationFrequency(int circulationFrequency)
        {
            this.circulationFrequency = circulationFrequency;
            StaticDBConnection.NonQueryDatabase("UPDATE [ABCs] SET Circulation_Frequency=" + circulationFrequency + " WHERE Intervention_ID=" + abcID + ";");
        }
    }
}