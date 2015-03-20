using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Database.Queries
{
    class CreateOperationQuery : DBQuery
    {
        public CreateOperationQuery(String operationName, String acronym, DateTime shiftStart, DateTime shiftEnd, String dispatcherName)
        {
            sql = "INSERT INTO [Operations] (Name, Acronym, Shift_Start, Shift_End, Dispatcher) VALUES ('" + operationName + "', '" + acronym + "', '" + DateTimeSQLite(shiftStart) + "', '" + DateTimeSQLite(shiftEnd) + "', '" + dispatcherName + "')";
        }
    }
}
