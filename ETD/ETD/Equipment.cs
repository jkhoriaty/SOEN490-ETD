using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD
{
   public class Equipment
    {
       public string EquipmentName;

       public Equipment(String name)
       {
           EquipmentName = name;
       }

       public string getEquipment()
       {
           return EquipmentName;

       }


    }
}
