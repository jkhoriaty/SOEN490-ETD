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
            sql = "INSERT INTO [Additional_Informations] (Intervention_ID, Information, Timestamp) VALUES (" + interventionID + ", '" + information.Replace("'", "''") + "', '" + DateTimeSQLite(timestamp) + "')";
        }
    }
}
