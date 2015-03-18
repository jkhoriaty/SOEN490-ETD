using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Database.Queries
{
    class GetAvailableEquipmentQuery : DBQuery
    {
        public GetAvailableEquipmentQuery()
        {
            sql = "SELECT * FROM [Available_Equipment];";
        }
    }
}
