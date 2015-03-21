using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Database.Queries
{
    class GetABCQuery : DBQuery
    {
        public GetABCQuery()
        {
            sql = "SELECT * FROM [ABCs];";
        }

        public GetABCQuery(int abcID)
        {
            sql = "SELECT * FROM [ABCs] " +
                  "WHERE ABC_ID=" + abcID + ";";
        }
    }
}
