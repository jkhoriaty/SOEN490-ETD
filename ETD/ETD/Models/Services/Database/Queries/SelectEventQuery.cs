using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Services.Database.Queries
{
    class SelectEventQuery : DBQuery
    {
        public SelectEventQuery()
        {
            sql = "SELECT * FROM [Events]";
        }

        public SelectEventQuery(Dictionary<string,string> Where)
        {
            sql = "SELECT * FROM [Events] WHERE ";
            foreach(KeyValuePair<string, string> clause in Where)
            {
                sql += clause.Key + "='" + clause.Value + "' ,";
            }
            sql.Remove(sql.Length - 2);
        }
    }
}
