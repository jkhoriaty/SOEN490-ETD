using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Database.Queries
{
    class CreateTeamQuery : DBQuery
    {
        public CreateTeamQuery(int operationID, string name, int status)
        {
            sql = "INSERT INTO [Teams] (Operation_ID, Name, Status) VALUES (" + operationID + ", '" + name + "', " + status  + ")";
        }
    }
}
