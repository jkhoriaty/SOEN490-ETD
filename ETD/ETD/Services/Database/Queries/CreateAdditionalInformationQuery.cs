using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Database.Queries
{
    class CreateAdditionalInformationQuery : DBQuery
    {
        public CreateAdditionalInformationQuery(int interventionID, String information, DateTime timestamp)
        {
            sql = "INSERT INTO [AdditionalInformations] (Intervention_ID, Information, Timestamp) VALUES (" + interventionID + ", '" + information + "', '" + DateTimeSQLite(timestamp) + "')";
        }
    }
}
