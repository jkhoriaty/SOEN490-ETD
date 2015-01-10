using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Objects
{

    public enum AdditionalInfos { camp, circle, line, ramp, rectangle, square, stairs, text };

    public class AdditionalInfo
    {
        public AdditionalInfo AdditionalinfoName;

        public AdditionalInfo(AdditionalInfo name)
       {
           AdditionalinfoName = name;
       }

        public AdditionalInfo getAdditionalinfo()
       {
           return AdditionalinfoName;
       }

    }
}
