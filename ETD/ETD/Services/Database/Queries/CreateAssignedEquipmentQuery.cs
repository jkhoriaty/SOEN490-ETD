using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Database.Queries
{
    class CreateAssignedEquipmentQuery : DBQuery
    {
        public CreateAssignedEquipmentQuery(int availableID, int teamID)
        {
            sql = "INSERT INTO [Assigned_Equipment] (Available_ID, Team_ID) VALUES (" + availableID + ", " + teamID + ")";
        }
    }
}
