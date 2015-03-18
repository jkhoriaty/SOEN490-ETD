using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Database.Queries
{
    class GetAdditionalInformationQuery : DBQuery
    {
        public GetAdditionalInformationQuery()
        {
            sql = "SELECT * FROM [Additional_Informations];";
        }
    }
}
