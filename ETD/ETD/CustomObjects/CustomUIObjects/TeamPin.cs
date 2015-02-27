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
using System.Windows.Input;

namespace ETD.CustomObjects.CustomUIObjects
{
	class TeamPin : Pin
	{
		internal static int size = 40;

		private Team team;
		private InterventionPin interventionPin;

		public TeamPin(Team team, MapSectionPage mapSection) : base(team, mapSection, size)
		{
			base.setImage(TechnicalServices.getImage(team, team.getStatus()));
			base.setText(team.getName());

			this.team = team; //Providing a link to the team that this pin represents
		}

		internal Team getTeam()
		{
			return team;
		}

		public override void DragStop(Canvas Canvas_map, MouseButtonEventArgs e)
		{
			base.DragStop(Canvas_map, e);

			if (interventionPin != null && interventionPin.getInterventionContainer() != null)
			{
				if(SufficientOverlap(interventionPin.getInterventionContainer())) //Considered accidental drag-and-drop
				{
					interventionPin.getInterventionContainer().PlaceAll();
				}
				else
				{
					interventionPin.getIntervention().RemoveTeam(team);
					team.setStatus("unavailable");
					interventionPin = null;
				}
			}
			else
			{
				team.setStatus("moving");
			}
		}

		internal override bool HandleSpecialCollisions(Pin fixedPin)
		{
			//SpecialCollision: Team is dropped on intervention, add team to intervention
			if(fixedPin.IsOfType("InterventionPin") && SufficientOverlap(fixedPin))
			{
				interventionPin = (InterventionPin)fixedPin;
				interventionPin.getIntervention().AddTeam(team);
				interventionPin.getInterventionContainer();
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
