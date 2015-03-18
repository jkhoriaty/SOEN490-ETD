using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Database.Queries
{
    class CreateAvailableEquipmentQuery : DBQuery
    {
        public CreateAvailableEquipmentQuery(int operationID, int typeID)
        {
            sql = "INSERT INTO [Available_Equipments] (Operation_ID, Type_ID) VALUES (" + operationID+ ", " + typeID + ")";
        }
    }
}
