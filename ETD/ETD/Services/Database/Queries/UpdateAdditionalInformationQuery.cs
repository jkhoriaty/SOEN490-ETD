using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Database.Queries
{
    class UpdateAdditionalInformationQuery : DBQuery
    {
        public UpdateAdditionalInformationQuery(int additionalInfoID, String information, DateTime timestamp)
        {
            sql = "UPDATE [Additional_Informations] " +
                  "SET Information=" + information + ", Timestamp='" + DateTimeSQLite(timestamp) +
                  "WHERE Additional_Info_ID=" + additionalInfoID + ";";
        }
    }
}
