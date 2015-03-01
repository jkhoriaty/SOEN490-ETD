using ETD.Models.ArchitecturalObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Objects
{
    /// <summary>
    /// Equipment Model Object
    /// </summary>

	public enum Equipments {ambulanceCart, sittingCart, epipen, transportStretcher, mountedStretcher, wheelchair};

	public class Equipment : Observable
    {
		private static List<Equipment> equipmentList = new List<Equipment>();

		private Equipments equipmentType;
		private bool assigned;

		public Equipment(String name)
		{
			equipmentType = (Equipments)Enum.Parse(typeof(Equipments), name);
			
			equipmentList.Add(this);
			ClassModifiedNotification(typeof(Equipment));
		}

		public static void DeleteEquipment(Equipment equipment)
		{
			equipmentList.Remove(equipment);
			ClassModifiedNotification(typeof(Equipment)); 
		}

		public void setAssigned(bool assigned)
		{
			this.assigned = assigned;
			ClassModifiedNotification(typeof(Equipment));
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

		public bool IsAssigned()
		{
			return assigned;
		}
    }
}
