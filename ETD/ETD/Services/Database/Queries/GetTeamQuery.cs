using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Database.Queries
{
    class GetTeamQuery : DBQuery
    {
        public GetTeamQuery()
        {
            sql = "SELECT * FROM [Teams];";
        }
    }
}
