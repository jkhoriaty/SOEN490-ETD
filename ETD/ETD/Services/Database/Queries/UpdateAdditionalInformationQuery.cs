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
            sql = "UPDATE [AdditionalInformations] " +
                  "SET Information=" + information + ", Timestamp='" + timestamp.ToString("MM/dd/yyyy HH:mm:ss") +
                  "WHERE Additional_Info_ID=" + additionalInfoID + ";";
        }
    }
}
