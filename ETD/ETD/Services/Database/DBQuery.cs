using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Services.Database
{
    class DBQuery
    {
        protected string sql;

        protected DBQuery(){}
        public DBQuery(string query)
        {
            this.sql = "";
        }

        public string GetQuery()
        {
            return sql;
        }

        protected string DateTimeSQLite(DateTime datetime)
        {
            string dateTimeFormat = "{0}-{1}-{2} {3}:{4}:{5}.{6}";
            return string.Format(dateTimeFormat, datetime.Year, datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, datetime.Second, datetime.Millisecond);
        }
    }
}
