using ETD.Models.Grids;
using ETD.Models.Objects;
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
	public class PinEditor
	{
		private MapSectionPage mapSection;
        private AdditionalInfoPage AIPmap;
		private int teamSize = 40;
		private int interventionSize = 40; //WARNING: If different than team size, need to revisit the addition of a team onto an intervention in the CollisionDetection method in the PinHandler file
		private int equipmentSize = 30;
        private int AddtionalInfoSize = 60;

		public PinEditor(MapSectionPage mapSection)
		{
			this.mapSection = mapSection;
		}

        public PinEditor(AdditionalInfoPage AIP)
        {
            this.AIPmap = AIP;
        }

		//Creating a new team pin as a result to the successfull submission of the team form
		public void CreateTeamPin(Team team)
		{
			TeamGrid mainContainer = new TeamGrid(team, mapSection, teamSize);
			mapSection.Map.Children.Add(mainContainer);
            
			//Setting pin in the top-left corner and making sure it does not cover any other item
			mapSection.SetPinPosition(mainContainer, (teamSize / 2), (teamSize / 2));

			//The tag of the pin is modified before being added so that the team does not get added to a intervention
			mainContainer.Tag = "intervention";
			mapSection.DetectCollision(mainContainer, (teamSize / 2), (teamSize / 2));
			mainContainer.Tag = "team";
		}

		public void CreateEquipmentPin(String equipmentName)
		{
			EquipmentGrid mainContainer = new EquipmentGrid(equipmentName, mapSection, equipmentSize);
			mapSection.Map.Children.Add(mainContainer);

			//Setting pin in the top-right corner and making sure it does not cover any other item
			mapSection.SetPinPosition(mainContainer, (mapSection.Map.ActualWidth - (equipmentSize / 2)), (equipmentSize / 2));

			//The tag of the pin is modified before being added so that the equipment does not get added to a member and possibly stuck in a modal MessageBox loop
			mainContainer.Tag = "intervention";
			mapSection.DetectCollision(mainContainer, (mapSection.Map.ActualWidth - (equipmentSize / 2)), (equipmentSize / 2));
			mainContainer.Tag = "equipment";
		}

		public void CreateInterventionPin(int interventionNumber)
		{
			InterventionGrid mainContainer = new InterventionGrid(interventionNumber, mapSection, interventionSize);
			mapSection.Map.Children.Add(mainContainer);

			//Setting pin in the bottom-left corner and making sure it does not cover any other item
			mapSection.SetPinPosition(mainContainer, (interventionSize / 2), (mapSection.Map.ActualHeight - (interventionSize / 2)));
			mapSection.DetectCollision(mainContainer, (interventionSize / 2), (mapSection.Map.ActualHeight - (interventionSize / 2)));
		}

		//Deleting pin using its name
		public void DeletePin(String pinName)
		{
			foreach (Grid grid in mapSection.Map.Children)
			{
				if (grid.Name.Equals(pinName))
				{
					mapSection.Map.Children.Remove(grid);
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


        public void CheckRight(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = sender as ContextMenu;
            TeamGrid fe = cm.PlacementTarget as TeamGrid;

            foreach (MenuItem mi in cm.Items)
            {
                mi.IsChecked = ((Statuses)Enum.Parse(typeof(Statuses), mi.Header.ToString().ToLower()) == fe.team.getStatus());
   
            }

            
        }

		internal void CheckRight(MenuItem mi, TeamGrid fe)
		{
			mi.IsChecked = ((Statuses)Enum.Parse(typeof(Statuses), mi.Header.ToString().ToLower()) == fe.team.getStatus());
		}





        //create additionnal info pin on the AdditionalInfoPage.xaml
        public void CreateAdditionnalInfoPin(String AI,int size)
        {
            //Choose size
            if (size !=0)
            {
                AddtionalInfoSize = size;
            }
            AdditionalInfo AI2 = new AdditionalInfo(AI);
            AdditionalInfoGrid mainContainer = new AdditionalInfoGrid(AI2, AIPmap, AddtionalInfoSize);
            
            //If the additional info is a line, call the drawline function
            if (AI.Equals("line"))
            {
                DrawLines();
            }

            else
            {
                AIPmap.AdditionalMap.Children.Add(mainContainer);
                //Setting pin in the bottom-left corner and making sure it does not cover any other item
                AIPmap.SetPinPosition(mainContainer, (AddtionalInfoSize / 2), (AIPmap.AdditionalMap.ActualHeight - (AddtionalInfoSize / 2)));
                //AIPmap.DetectCollision(mainContainer, (AddtionalInfoSize / 2), (AIPmap.AdditionalMap.ActualHeight - (AddtionalInfoSize / 2)));
            }
          
        }


        //resize icons
        //available choices: small, medium, large
        public void ScalePin(object sender, RoutedEventArgs e)
        {
            
            foreach (AdditionalInfoGrid grid in AIPmap.AdditionalMap.Children)
            {

                ContextMenu m = AIPmap.Resources["AIcontext"] as ContextMenu;

                foreach (MenuItem mi in m.Items)
                {

                        foreach (MenuItem ji in mi.Items)
                        {
                            MenuItem item = sender as MenuItem;

                            // MessageBox.Show(ji.Name);
                            // MessageBox.Show(item.Header.ToString().ToLower());
                            // MessageBox.Show((ji.IsChecked = (ji == item)).ToString());
                              if (ji.Name.Equals("small") && (ji.IsChecked = (ji == item)))
                              {
                                     MessageBox.Show(ji.Name);
                                    AIPmap.AdditionalMap.Children.Remove(grid);
                                 CreateAdditionnalInfoPin(grid.Name, 30);
                                  ji.IsChecked = false;
                                   return;
                              }

                              if (ji.Name.Equals("medium") && (ji.IsChecked = (ji == item)))
                              {
                                  MessageBox.Show(ji.Name);
                                  AIPmap.AdditionalMap.Children.Remove(grid);
                                  CreateAdditionnalInfoPin(grid.Name, 60);
                                  ji.IsChecked = false;
                                  return;
                              }
                              if (ji.Name.Equals("large") && (ji.IsChecked = (ji == item)))
                              {
                                  MessageBox.Show(ji.Name);
                                  AIPmap.AdditionalMap.Children.Remove(grid);
                                  CreateAdditionnalInfoPin(grid.Name, 100);
                                  ji.IsChecked = false;
                                  return;
                              }
                       
                           // Trying to implement it similarly to ChangeStatus but calling the Scaling method does not rescale the shape or place it at the appropriate place..
                           // Will fix later
                           // AdditionalInfo AI2 = new AdditionalInfo(grid.Name);
                           // AdditionalInfoGrid Shape = new AdditionalInfoGrid(AI2, AIPmap, AddtionalInfoSize);
                           // if (Shape != null && (ji.IsChecked = (ji == item)))
                           // {
                           //     Shape.ScalePin(item.Header.ToString().ToLower());
                           // }

                      }

               }
            }
        }

        //Draw lines
        //Accepts 2 arguments:
        //start - retrieved on first mouse click
        //end - retrieved on second mouse click
        public void DrawLines()
        {
            MessageBox.Show(" draw lines");
        }


        //Deleting pin from the additional info page
        public void AIDeletePin(object sender, RoutedEventArgs e)
        {
            foreach (AdditionalInfoGrid grid in AIPmap.AdditionalMap.Children)
            { 
                    AIPmap.AdditionalMap.Children.Remove(grid);
                    return;    
            }
        }

		//Filter itmes and edit the appropriate status
		public void EditMenuItems(object sender, RoutedEventArgs e)
		{
			ContextMenu cm = (ContextMenu)sender;
			foreach (MenuItem mi in cm.Items)
			{
				mi.Visibility = Visibility.Collapsed;
				if (cm.PlacementTarget is TeamGrid && mi.Tag.Equals("team"))
				{
					mi.Visibility = Visibility.Visible;
					CheckRight(mi, (TeamGrid)cm.PlacementTarget);
				}
				else if (cm.PlacementTarget is EquipmentGrid && mi.Tag.Equals("equipment"))
				{
					mi.Visibility = Visibility.Visible;
				}
			}
		}
	}
}
