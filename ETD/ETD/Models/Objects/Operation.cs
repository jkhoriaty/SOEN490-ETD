using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Objects
{
    /// <summary>
    /// Operation Model Object
    /// </summary>

    public class Operation
    {
        private String operationName;
        private String acronym;
        private DateTime shiftStart;
        private DateTime shiftEnd;
        private String dispatcherName;

        public Operation(String operationName, String acronym, DateTime shiftStart, DateTime shiftEnd, String dispatcherName)
		{
            this.operationName = operationName;
            this.acronym = acronym;
            this.shiftStart = shiftStart;
            this.shiftEnd = shiftEnd;
            this.dispatcherName = dispatcherName;
		}
    }
}
