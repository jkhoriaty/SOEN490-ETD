using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Objects
{
    /// <summary>
    /// Intervention Additional Information Model Object
    /// </summary>

    public class InterventionAdditionalInfo
    {
        //Database reflection variables
        private int additionalInfoID;
        private int interventionID;

        private String information;
        private DateTime timestamp;

        //Creates an additional information 
        public InterventionAdditionalInfo(String information, DateTime timestamp)
        {
            this.information = information;
            this.timestamp = timestamp;
        }

        //Accessors
        public int getID()
        {
            return additionalInfoID;
        }

        public int getParentID()
        {
            return interventionID;
        }

        //Returns the additional information for the intervention
        public String getInformation()
        {
            return this.information;
        }

        //Returns the additional information's time of creation
        public DateTime getTimestamp()
        {
            return this.timestamp;
        }
    }
}