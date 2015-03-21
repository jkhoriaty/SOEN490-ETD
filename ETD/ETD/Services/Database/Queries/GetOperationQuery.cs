using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Database.Queries
{
    class GetOperationQuery : DBQuery
    {
        public GetOperationQuery()
        {
            sql = "SELECT * FROM [Operations];";
        }

        public GetOperationQuery(int operationID)
        {
            sql = "SELECT * FROM [Operations] " +
                  "WHERE Operation_ID=" + operationID + ";";
        }
    }
}
