using ETD.Models.ArchitecturalObjects;
using ETD.Services.Database;
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

    //Equipment types
	public enum Equipments {ambulanceCart, sittingCart, epipen, transportStretcher, mountedStretcher, wheelchair};


    [Serializable()]
    public class Equipment : Observable
    {
        //Database reflection variables
        private int equipmentID;
        private int operationID;

		private static List<Equipment> equipmentList = new List<Equipment>();//Contains a list of equipments

		private Equipments equipmentType;
		private bool assigned;//Used when checking if the equipment is assigned to any team

        //Creates an equipment and notifies the list of observers
		public Equipment(String name)
		{
			this.equipmentType = (Equipments)Enum.Parse(typeof(Equipments), name);
			equipmentList.Add(this);
			ClassModifiedNotification(typeof(Equipment));
            if (Operation.currentOperation != null)
            {
                this.operationID = Operation.currentOperation.getID();
            }
            this.equipmentID = StaticDBConnection.NonQueryDatabaseWithID("INSERT INTO [Equipments] (Operation_ID, Type_ID) VALUES (" + operationID + ", " + 1+(int)equipmentType + ")");
		}

        //Deletes an equipment and notifies the list of observers
		public static void DeleteEquipment(Equipment equipment)
		{
			equipmentList.Remove(equipment);
			ClassModifiedNotification(typeof(Equipment)); 
		}

        //Checks if the equipment is assigned to a team
		public bool IsAssigned()
		{
			return assigned;
		}

		//Accessors
        public int getID()
        {
            return equipmentID;
        }

        public int getParentID()
        {
            return operationID;
        }
        //Returns the equipment type
		public Equipments getEquipmentType()
		{
			return equipmentType;
		}

        //Return the list of equipments
		public static List<Equipment> getEquipmentList()
		{
			return equipmentList;
		}

        //Mutators

        //Assign the the equipment to a team
        public void setAssigned(bool assigned)
        {
            this.assigned = assigned;
            ClassModifiedNotification(typeof(Equipment));
        }

    }
}
