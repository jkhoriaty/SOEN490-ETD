using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Services.Database
{
    class DBQuery
    {
        protected string sql;

        protected DBQuery(){}
        public DBQuery(string query)
        {
            
            //this.sql = query;
            this.sql = "";
        }

        public string GetQuery()
        {
            return sql;
        }
    }
}
