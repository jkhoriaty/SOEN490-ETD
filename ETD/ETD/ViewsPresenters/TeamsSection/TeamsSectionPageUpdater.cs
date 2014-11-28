using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using ETD.Models;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace ETD.ViewsPresenters.TeamsSection
{
	class TeamsSectionPageUpdater
	{
		private TeamsSectionPage caller;
		private static double teamSizeDifference = 0;
		private static Dictionary<String, StackPanel> teamEquipmentStacks = new Dictionary<String, StackPanel>();

		public TeamsSectionPageUpdater(TeamsSectionPage caller)
		{
			this.caller = caller;
		}

		public void setTeamsHeight(Border TeamSection)
		{
			if (teamSizeDifference == 0)
			{
				teamSizeDifference = TeamSection.ActualHeight - caller.TeamList.ActualHeight;
			}
			caller.Scroller.MaxHeight = TeamSection.Height - teamSizeDifference;
		}

		//Called to show the form to create a new team
		public void DisplayCreateTeamForm(Frame frame)
		{
			caller.TeamList.Children.Add(frame);
			caller.CreateTeamButton.IsEnabled = false;
		}

		//To be called after submit or cancel of the create team form
		public void HideCreateTeamForm()
		{
			caller.TeamList.Children.RemoveAt(caller.TeamList.Children.Count - 1);
			caller.CreateTeamButton.IsEnabled = true;
		}

		//Called to display a created team information in the Team list
		public void DisplayTeamInfo(Frame frame)
		{
			caller.TeamList.Children.Add(frame);
		}

		//Removing equipment from team carried equipment
		public void RemoveTeamInfo(String teamName)
		{
			foreach (Frame bd in caller.TeamList.Children)
			{
				Page pg = (Page) bd.Content;
				if (pg.Name.Equals(teamName))
				{
					teamEquipmentStacks.Remove(teamName);
					caller.TeamList.Children.Remove(bd);
					return;
				}
			}
		}

		//Adding equipment to team description
		public void AddTeamEquipment(String equip, String teamName)
		{
			//Creating the rectangle in which the equipment is going to reside
			Rectangle equipment = new Rectangle();
			equipment.Name = equip;
			equipment.Tag = teamName;
			equipment.Width = 27;
			equipment.Height = 27;
			Thickness equipmentMargin = equipment.Margin;
			equipmentMargin.Right = 2;
			equipmentMargin.Left = 2;
			equipment.Margin = equipmentMargin;
			equipment.MouseRightButtonDown += new MouseButtonEventHandler(caller.RemoveTeamEquipment);

			//Getting the background image to the rectangle
			ImageBrush equipmentImage = new ImageBrush();
			equipmentImage.ImageSource = Services.getImage((equipments)Enum.Parse(typeof(equipments), equip));
			equipment.Fill = equipmentImage;

			//Getting the appropriate equipment StackPanel
			teamEquipmentStacks[teamName].Children.Add(equipment);
		}

		//Removing equipment from team carried equipment
		public void RemoveTeamEquipment(Rectangle sender)
		{
			StackPanel equipmentStackPanel = (StackPanel)sender.Parent;
			equipmentStackPanel.Children.Remove(sender);
		}
	}
}
