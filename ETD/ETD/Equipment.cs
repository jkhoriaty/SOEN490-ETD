using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD
{
   public class Equipment
    {
       public String EquipmentName;

       public Equipment(String name)
       {
           EquipmentName = name;
       }

       public String getEquipmentName()
       {
           return EquipmentName;

       }


    }
}
