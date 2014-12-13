using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models
{
	public enum Equipments {ambulanceCart, sittingCart, epipen, transportStretcher, mountedStretcher, wheelchair};

	public class Equipment
    {

	   public Equipments EquipmentName;

       public Equipment(Equipments name)
       {
           EquipmentName = name;
       }

       public Equipments getEquipmentName()
       {
           return EquipmentName;
       }
    }
}
