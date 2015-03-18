using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Database.Queries
{
    class UpdateABCQuery : DBQuery
    {
        public UpdateABCQuery(int abcID, string consciousness = null, bool disoriented = false, string airways = null, string breathing = null, int breathingFrequency = -1, string circulation = null, int circulationFrequency = -1)
        {
            sql = "UPDATE [ABCs] " +
                  "SET Consciousness='" + consciousness + "', Disoriented=" + disoriented + ", Airways='" + airways + "', Breathing='" + breathing + "', Breathing_Frequency=" + breathingFrequency + ", Circulation='" + circulation + "', Circulation_Frequency=" + circulationFrequency + 
                  "WHERE ABC_ID=" + abcID + ";";
        }
    }
}
