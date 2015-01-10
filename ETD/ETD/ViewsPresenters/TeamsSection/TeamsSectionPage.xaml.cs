using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ETD.ViewsPresenters.TeamsSection.TeamForm;
using ETD.ViewsPresenters.TeamsSection.TeamInfo;
using ETD.Models;

namespace ETD.ViewsPresenters.TeamsSection
{
	/// <summary>
	/// Interaction logic for TeamsSectionPage.xaml
	/// </summary>
	public partial class TeamsSectionPage : Page
	{
		private MainWindow mainWindow;
		private static Dictionary<String, StackPanel> teamEquipmentStacks = new Dictionary<String, StackPanel>();

		public TeamsSectionPage(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
		}

		//Adjusting the team section height
		public void setTeamsSectionHeight(Border TeamsSection)
		{
			//11 to account for padding of 2 on top and 2 on the bottom in addition to 7 in margin to the CreateTeamButtonBorder
			Scroller.MaxHeight = TeamsSection.ActualHeight - TeamsSectionLabel.ActualHeight - CreateTeamButtonBorder.ActualHeight - 11;
		}

		//Clicking on the add team button
		private void DisplayCreateTeamForm(object sender, RoutedEventArgs e)
		{
			Frame frame = new Frame();
			frame.Content = new TeamFormPage(this);
			TeamList.Children.Add(frame);
			CreateTeamButton.IsEnabled = false;
		}

		//Hiding form after submit or cancel
		public void HideCreateTeamForm()
		{
			TeamList.Children.RemoveAt(TeamList.Children.Count - 1);
			CreateTeamButton.IsEnabled = true;
		}

		//Displaying the team upon form submit
		public void DisplayTeamInfo(Team team)
		{
			HideCreateTeamForm();
			Frame frame = new Frame();
			frame.Content = new TeamInfoPage(this, team);
			TeamList.Children.Add(frame);
			mainWindow.CreateTeamPin(team);
		}

		//Registering the team equipment StackPanel to be able to add equipment to each team
		public void registerStackPanel(String teamName, StackPanel equipmentStack)
		{
			teamEquipmentStacks.Add(teamName, equipmentStack);
		}

        /* TO BE COMPLETED
        //Add timer to notify 15mins prior to the end of shift
        public void setTimer(TeamMember member, ToolTip departTime) 
        {
            DateTime departure = member.getDeparture();
            TimeSpan timeLeft = departure - DateTime.Now;
            TimeSpan fifteen = TimeSpan.FromMinutes(15);
            if (timeLeft < TimeSpan.Zero)
            {
                //departTime.SetToolTip(this,"Departure Time");
            }
            else if (timeLeft < fifteen)
            {
               // departTime.ToolTip = timeLeft;
            }
            
        }
        */

		//Deleting the team upon right click on the label
		public void RemoveTeam(String teamName)
		{
			foreach (Frame item in TeamList.Children)
			{
				Page team = (Page)item.Content;
				if (team.Name.Equals(teamName))
				{
					teamEquipmentStacks.Remove(teamName);
					TeamList.Children.Remove(item);
					break;
				}
			}
			Team.teams.Remove(teamName);
			mainWindow.DeletePin(teamName);
		}

		//Adding equipment to specified team equipment stack
		public void AddTeamEquipment(Equipment equip, String teamName)
		{
			//Limit of 3 pieces of equipment per team
			if (teamEquipmentStacks[teamName].Children.Count <= 3)
			{
				Rectangle imageRectangle = new Rectangle();
				imageRectangle.Name = equip.ToString();
				imageRectangle.Tag = teamName;
				imageRectangle.Width = 27;
				imageRectangle.Height = 27;
				imageRectangle.MouseRightButtonDown += new MouseButtonEventHandler(RemoveTeamEquipment);
                imageRectangle.FlowDirection = FlowDirection.LeftToRight;

				Thickness equipmentMargin = imageRectangle.Margin;
				equipmentMargin.Right = 1;
				imageRectangle.Margin = equipmentMargin;

				//Getting the background image to the rectangle
				ImageBrush equipmentImage = new ImageBrush();
				equipmentImage.ImageSource = Services.getImage(equip.getEquipmentName());
				imageRectangle.Fill = equipmentImage;

				//Getting the appropriate equipment StackPanel
				teamEquipmentStacks[teamName].Children.Add(imageRectangle);

				Team.teams[teamName].addEquipment(equip);
			}
			else
			{
				MessageBox.Show("You cannot add more than 3 pieces of equipment to a team. The equipment is going to be readded to the map.");
				mainWindow.CreateEquipmentPin(equip.ToString());
			}
		}

		//Right clicking on an equipment in a team description removew the equipment from the stack and adds it back to the map
		public void RemoveTeamEquipment(object sender, RoutedEventArgs e)
		{
			Rectangle equipment = (Rectangle)sender;
            //Type equipType = Type.GetType(equipment.Name.ToString());
            Equipment equip = new Equipment((Equipments)Enum.Parse(typeof(Equipments), equipment.Name.ToString()));
			StackPanel equipmentStackPanel = (StackPanel)equipment.Parent;
			Team.teams["" + equipment.Tag].removeEquipment(equip);
			equipmentStackPanel.Children.Remove(equipment);
			mainWindow.CreateEquipmentPin(equipment.Name);

		}
	}
}
