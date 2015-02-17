using ETD.Models.ArchitecturalObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Objects
{
	public enum Equipments {ambulanceCart, sittingCart, epipen, transportStretcher, mountedStretcher, wheelchair};

	public class Equipment : Observable
    {
		private static List<Equipment> equipmentList = new List<Equipment>();

		private Equipments equipmentType;

		public Equipment(String name)
		{
			equipmentType = (Equipments)Enum.Parse(typeof(Equipments), name);
			
			equipmentList.Add(this);
			NotifyAll();
		}

		/*
		 * Getters
		 */
		public Equipments getEquipmentType()
		{
			return equipmentType;
		}

		public static List<Equipment> getEquipmentList()
		{
			return equipmentList;
		}
    }
}
