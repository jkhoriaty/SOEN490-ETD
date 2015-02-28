using ETD.Models.Objects;
using ETD.Services;
using ETD.ViewsPresenters.MapSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Equipment Model Object
/// </summary>

namespace ETD.CustomObjects.CustomUIObjects
{
	class EquipmentPin : Pin
	{
		internal static int size = 30;

		private Equipment equipment;

		public EquipmentPin(Equipment equipment, MapSectionPage mapSection) : base(equipment, mapSection, size)
		{
			base.setImage(TechnicalServices.getImage(equipment.getEquipmentType()));

			this.equipment = equipment; //Providing a link to the equipment that this pin represents
		}

		internal override bool HandleSpecialCollisions(Pin fixedPin)
		{
			//SpecialCollision: Equipment is dropped on a team with sufficient overlap, add the equipment to the team
			if(fixedPin.IsOfType("TeamPin") && SufficientOverlap(fixedPin))
			{
				TeamPin teamPin = (TeamPin)fixedPin;
				teamPin.getTeam().AddEquipment(equipment);
				equipment.setAssigned(true);
				return true;
			}

			//If there are no special conditions return false
			return false;
		}
	}
}
