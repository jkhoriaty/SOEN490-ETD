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
	}
}
