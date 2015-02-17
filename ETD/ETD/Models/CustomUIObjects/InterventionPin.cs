using ETD.Models.Objects;
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
		private static int size = 40;

		private Intervention intervention;

		public InterventionPin(Intervention intervention, MapSectionPage mapSection) : base(intervention, mapSection, size)
		{
			base.setImage(TechnicalServices.getImage("intervention"));
			base.setText(intervention.getInterventionNumber().ToString());

			this.intervention = intervention; //Providing a link to the team that this pin represents
		}
	}
}
