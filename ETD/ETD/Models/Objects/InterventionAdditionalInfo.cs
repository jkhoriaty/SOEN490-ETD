using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Objects
{
    public class InterventionAdditionalInfo
    {
        private String information;
        private DateTime timestamp;

        public InterventionAdditionalInfo(String information, DateTime timestamp)
        {
            this.information = information;
            this.timestamp = timestamp;
        }
    }
}