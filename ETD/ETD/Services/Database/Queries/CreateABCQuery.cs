using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Database.Queries
{
    class CreateABCQuery : DBQuery
    {
        public CreateABCQuery(int interventionID)
        {
            sql = "INSERT INTO [ABCs] (Intervention_ID) VALUES (" + interventionID + ");";
        }
        public CreateABCQuery(int interventionID, string consciousness, bool disoriented, string airways, string breathing, int breathingFrequency, string circulation, int circulationFrequency)
        {
            sql = "INSERT INTO [ABCs] (Intervention_ID, Consciousness, Disoriented, Airways, Breathing, Breathing_Frequency, Circulation, Circulation_Frequency) VALUES (" + interventionID + ", '" + consciousness.Replace("'", "''") + "', " + disoriented + ", '" + airways.Replace("'", "''") + "', '" + breathing.Replace("'", "''") + "', " + breathingFrequency + ", '" + circulation.Replace("'", "''") + "', " + circulationFrequency + ");";
        }
    }
}
