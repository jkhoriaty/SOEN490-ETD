using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models
{

    public enum AdditionalInfos { camp, circle, line, ramp, rectangle, square, stairs, text };

    public class AdditionalInfo
    {
        public String AdditionalinfoName;
        public int AISize;

        public AdditionalInfo(String name)
       {
           AdditionalinfoName = name;

       }

        public String getAdditionalinfoName()
       {
           return AdditionalinfoName;
       }

        public int getAISize()
        {
            return AISize;
        }

        public void setAISize(int  s)
        {
            this.AISize = s;
        }
    }
}
