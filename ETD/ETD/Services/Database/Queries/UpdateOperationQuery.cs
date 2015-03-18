using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Database.Queries
{
    class UpdateOperationQuery : DBQuery
    {
        public UpdateOperationQuery(int operationID,String operationName, String acronym, DateTime shiftStart, DateTime shiftEnd, String dispatcherName)
        {
            sql = "UPDATE [Operations] " +
                  "SET Name='" + operationName + "', Acronym='" + acronym+ "', Shift_Start='" + shiftStart.ToString() + "', Shift_End='" + shiftEnd.ToString() + "', Dispatcher='" + dispatcherName + "' " +
                  "WHERE Operation_ID=" + operationID + ";";
        }
    }
}
