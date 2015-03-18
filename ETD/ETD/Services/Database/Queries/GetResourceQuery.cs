using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Database.Queries
{
    class GetResourceQuery : DBQuery
    {
        public GetResourceQuery()
        {
            sql = "SELECT * FROM [Resources];";
        }
    }
}
