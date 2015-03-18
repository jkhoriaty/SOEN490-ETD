using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Database.Queries
{
    class CreateResourceQuery : DBQuery
    {
        public CreateResourceQuery(int interventionID, int teamID)
        {
            sql = "INSERT INTO [Resources] (Intervention_ID, Team_ID) VALUES (" + interventionID + ", " + teamID + ")";
        }
        public CreateResourceQuery(int interventionID, String resourceName, int teamID, bool intervening, DateTime moving, DateTime arrival)
        {
            sql = "INSERT INTO [Resources] (Intervention_ID, Name, Team_ID, Intervening, Moving, Arrival) VALUES (" + interventionID + ", '" + resourceName + "', " + teamID + ", " + intervening + "', '" + DateTimeSQLite(moving) + "', '" + DateTimeSQLite(arrival) + ")";
        }
    }
}
