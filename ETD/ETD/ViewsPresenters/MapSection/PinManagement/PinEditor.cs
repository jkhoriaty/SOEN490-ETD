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
using System.Drawing;
using System.Windows.Media.Imaging;

namespace ETD.ViewsPresenters.MapSection.PinManagement
{
	public class PinEditor
	{
		private MapSectionPage mapSection;
        private AdditionalInfoPage AIPmap;
		private int teamSize = 40;
		private int interventionSize = 40; //WARNING: If different than team size, need to revisit the addition of a team onto an intervention in the CollisionDetection method in the PinHandler file
		private int equipmentSize = 30;
        private int AddtionalInfoSize = 50;
        private bool ContainsLine = false;
        private bool isdrawing = false;
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
            team.setTeamGrid(mainContainer);
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
					if(grid.Tag.Equals("intervention"))
					{
						//Remove the border of the intervention as well
						foreach (Grid g in mapSection.Map.Children)
						{
							if (g.Name.Equals(pinName))
							{
								mapSection.Map.Children.Remove(g);
								return;
							}
						}
					}
					return;
				}
			}
		}

		//Different signature to handle the removal of the exact equipment and not one with a similar name
		public void DeletePin(Grid pin)
		{
			mapSection.Map.Children.Remove(pin);
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
						if(item.Header.ToString().Equals("Intervening"))
						{
							mapSection.ReportArrival(team);
						}
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
        public void CreateAdditionnalInfoPin(String AI, int size)
        {

            AdditionalInfo AI2 = new AdditionalInfo(AI);
            AdditionalInfoGrid mainContainer = new AdditionalInfoGrid(AI2, AIPmap, AddtionalInfoSize);


            if (AI.Equals("line"))
            {
                //if button released, mouse not drawing
                //if button pressed, save point
                //if button relesed, save end point and draw line
                isdrawing = true;

                AIPmap.AdditionalMap.MouseLeftButtonDown += new MouseButtonEventHandler(AIPmap.DrawingStart);
                AIPmap.AdditionalMap.MouseLeftButtonUp += new MouseButtonEventHandler(AIPmap.DrawingStop);
                AIPmap.AdditionalMap.MouseMove += new MouseEventHandler(AIPmap.Move);
                AIPmap.AdditionalMap.MouseUp += new MouseButtonEventHandler(AIPmap.DrawingMove);
                AIPmap.AdditionalMap.MouseWheel += new MouseWheelEventHandler(AIPmap.ChangeColor);

                double lwidth = AIPmap.AdditionalMap.ActualWidth;
                double lheight = AIPmap.AdditionalMap.ActualHeight;
                AdditionalInfoGridLines line = new AdditionalInfoGridLines(AI2, AIPmap, lwidth, lheight);

                //line obj doesnt exist
                if (!ContainsLine)
                {
                    AIPmap.AdditionalMap.Children.Add(line);
                    ContainsLine = true;
                }


                AIPmap.AdditionalMap.Children.Add(mainContainer);

                //Setting pin in the bottom-left corner and making sure it does not cover any other item
                AIPmap.SetPinPosition(mainContainer, (AddtionalInfoSize / 2), (AIPmap.AdditionalMap.ActualHeight - (AddtionalInfoSize / 2)));
                //AIPmap.DetectCollision(mainContainer, (AddtionalInfoSize / 2), (AIPmap.AdditionalMap.ActualHeight - (AddtionalInfoSize / 2)));
            }
        }

     




        //Draw lines
        //Accepts 2 arguments:
        //start - retrieved on first mouse click
        //end - retrieved on second mouse click  

 

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
		public void EditMenuItems(ContextMenu cm)
		{
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
                else if (!(cm.PlacementTarget is TeamGrid || cm.PlacementTarget is EquipmentGrid) && mi.Tag.Equals("map"))
                {
                    mi.Visibility = Visibility.Visible;
                    CheckRight(mi);
                }
			}
		}

		private void CheckRight(MenuItem mi, TeamGrid fe)
		{
			mi.IsChecked = ((Statuses)Enum.Parse(typeof(Statuses), mi.Header.ToString().ToLower()) == fe.team.getStatus());
		}

        private void CheckRight(MenuItem mi)
        {
            mi.IsChecked = (mi.Header.Equals(mapSection.zoomLevel));
        }
	}
}
