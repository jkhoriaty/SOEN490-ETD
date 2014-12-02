using ETD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace ETD.ViewsPresenters.MapSection.PinManagement
{
	class PinEditor
	{
		private MapSectionPage caller;
		private double teamSize = 40;
		private double equipmentSize = 30;

		public PinEditor(MapSectionPage caller)
		{
			this.caller = caller;
		}

		//Creating a new team pin as a result to the successfull submission of the team form
		public void CreateTeamPin(Team team)
		{
			TeamGrid mainContainer = new TeamGrid(team,caller);
			caller.Map.Children.Add(mainContainer);
            
			//Setting pin in the top-left corner and making sure it does not cover any other item
			caller.SetPinPosition(mainContainer, (teamSize / 2), (teamSize / 2)); //Setting it top corner
			caller.DetectCollision(mainContainer, (teamSize / 2), (teamSize / 2));
		}

		public void CreateEquipmentPin(String equipmentName)
		{
			EquipmentGrid mainContainer = new EquipmentGrid(equipmentName, caller);
			caller.Map.Children.Add(mainContainer);

			//Setting pin in the top-left corner and making sure it does not cover any other item
			caller.SetPinPosition(mainContainer, (equipmentSize / 2), (equipmentSize / 2));
			caller.DetectCollision(mainContainer, (equipmentSize / 2), (equipmentSize / 2));
		}

		//Deleting pin using its name
		public void DeletePin(String pinName)
		{
			foreach (Grid grid in caller.Map.Children)
			{
				if (grid.Name.Equals(pinName))
				{
					caller.Map.Children.Remove(grid);
					return;
				}
			}
		}

        public void ChangeStatus(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            if (item != null)
            {
                ContextMenu parent = item.Parent as ContextMenu;
                if (parent != null)
                {
                    foreach(MenuItem mi in parent.Items)
                    {
                        mi.IsChecked = (mi == item);
                    }
                    TeamGrid team = parent.PlacementTarget as TeamGrid;
                    if (team != null)
                    {
                        team.ChangeStatus(item.Header.ToString().ToLower());
                    }
                }
            }
            

        }
	}
}
