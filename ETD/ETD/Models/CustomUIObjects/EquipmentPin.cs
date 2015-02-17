using ETD.Models.Objects;
using ETD.Services;
using ETD.ViewsPresenters.MapSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.CustomUIObjects
{
	class EquipmentPin : Pin
	{
		static int size = 30;

		public EquipmentPin(String equipmentName, MapSectionPage mapSection) : base(mapSection, size)
		{
			Equipments equipment = (Equipments)Enum.Parse(typeof(Equipments), equipmentName);
			base.setImage(TechnicalServices.getImage(equipment));
		}
	}
}
