using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Services.Database.Queries
{
    class AddCallQuery : DBQuery
    {
        public AddCallQuery(int ID)
        {
            sql = "INSERT INTO [Ambulance_Calls] (Call_ID, Intervention_ID) VALUES (null, '" + ID + "')";
        }
    }
}
