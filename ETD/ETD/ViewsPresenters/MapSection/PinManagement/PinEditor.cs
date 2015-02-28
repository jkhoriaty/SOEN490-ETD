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

        public void ChangeStatus(object sender, RoutedEventArgs e)
        {/*
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
            }*/
        }



        public void CheckRight(object sender, RoutedEventArgs e)
        {/*
            ContextMenu cm = sender as ContextMenu;
            TeamGrid fe = cm.PlacementTarget as TeamGrid;

            foreach (MenuItem mi in cm.Items)
            {
                mi.IsChecked = ((Statuses)Enum.Parse(typeof(Statuses), mi.Header.ToString().ToLower()) == fe.team.getStatus());
   
            }
			*/
        }

		/*internal void CheckRight(MenuItem mi, TeamGrid fe)
		{
			mi.IsChecked = ((Statuses)Enum.Parse(typeof(Statuses), mi.Header.ToString().ToLower()) == fe.team.getStatus());
		}*/

		//Filter itmes and edit the appropriate status
		public void EditMenuItems(ContextMenu cm)
		{/*
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
			}*/
		}

        private void CheckRight(MenuItem mi)
        {
            mi.IsChecked = (mi.Header.Equals(mapSection.zoomLevel));
        }
	}
}
