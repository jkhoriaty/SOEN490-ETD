using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Database.Queries
{
    class CreateInterventionQuery : DBQuery
    {
        public CreateInterventionQuery(int operationID, int interventionNumber, DateTime timeOfCall)
        {
            sql = "INSERT INTO [Interventions] (Operation_ID, Intervention_Number, Time_Of_Call) VALUES (" + operationID + ", " + interventionNumber + ", '" + DateTimeSQLite(timeOfCall) + "')";
        }
    }
}
