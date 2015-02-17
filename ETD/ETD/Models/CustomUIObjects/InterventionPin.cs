using ETD.Services;
using ETD.ViewsPresenters.MapSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ETD.Models.CustomUIObjects
{
	class InterventionPin : Pin
	{
		static int size = 40;

		public InterventionPin(int interventionNumber, MapSectionPage mapSection) : base(mapSection, size)
		{
			base.setImage(TechnicalServices.getImage("intervention"));
			base.setText(interventionNumber.ToString());
		}
	}
}
