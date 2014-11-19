using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD
{
	public enum equipments {ambulanceCart, sittingCart, epipen, transportStretcher, mountedStretcher, wheelchair};

	public class Equipment
    {

	   public equipments EquipmentName;

       public Equipment(equipments name)
       {
           EquipmentName = name;
       }

       public equipments getEquipmentName()
       {
           return EquipmentName;
       }
    }
}
