using ETD.Models.Objects;
using ETD.Services;
using ETD.ViewsPresenters.MapSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ETD.Models.CustomUIObjects
{
	class TeamPin : Pin
	{
		private static int size = 40;

		private Team team;

		public TeamPin(Team team, MapSectionPage mapSection) : base(team, mapSection, size)
		{
			base.setImage(TechnicalServices.getImage(team, team.getStatus()));
			base.setText(team.getName());

			this.team = team; //Providing a link to the team that this pin represents
		}
	}
}
