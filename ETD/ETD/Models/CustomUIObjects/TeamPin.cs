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
	class TeamPin : Pin
	{
		static int size = 40;

		public TeamPin(Team team, MapSectionPage mapSection) : base(mapSection, size)
		{
			base.setImage(TechnicalServices.getImage(team, team.getStatus()));
			base.setText(team.getName());
		}
	}
}
