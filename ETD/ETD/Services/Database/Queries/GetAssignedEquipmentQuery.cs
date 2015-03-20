using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Database.Queries
{
    class GetAssignedEquipmentQuery : DBQuery
    {
        public GetAssignedEquipmentQuery()
        {
            sql = "SELECT * FROM [Assigned_Equipment];";
        }
    }
}
