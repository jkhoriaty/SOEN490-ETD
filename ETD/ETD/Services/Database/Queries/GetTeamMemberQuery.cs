﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Database.Queries
{
    class GetTeamMemberQuery : DBQuery
    {
        public GetTeamMemberQuery()
        {
            sql = "SELECT * FROM [Team_Members];";
        }
    }
}
