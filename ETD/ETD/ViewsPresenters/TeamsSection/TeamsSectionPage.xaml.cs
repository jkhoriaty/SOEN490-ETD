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
		private static double teamSizeDifference = 0;
		private static Dictionary<String, StackPanel> teamEquipmentStacks = new Dictionary<String, StackPanel>();

		public TeamsSectionPage(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
		}

		//Adjusting the team section height
		public void setTeamsSectionHeight(Border TeamSection)
		{
			if (teamSizeDifference == 0)
			{
				teamSizeDifference = TeamSection.ActualHeight - TeamList.ActualHeight;
			}
			Scroller.MaxHeight = TeamSection.Height - teamSizeDifference;
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

		//Deleting the team upon right click on the label
		public void RemoveTeam(String teamName)
		{
			foreach (Frame item in TeamList.Children)
			{
				Page team = (Page)item.Content;
				if (team.Name.Equals(teamName))
				{
					teamEquipmentStacks.Remove(teamName);
					TeamList.Children.Remove(team);
					break;
				}
			}
			mainWindow.DeletePin(teamName);
		}

		//Adding equipment to specified team equipment stack
		public void AddTeamEquipment(String equip, String teamName)
		{
			Rectangle imageRectangle = new Rectangle();
			imageRectangle.Name = equip;
			imageRectangle.Tag = teamName;
			imageRectangle.Width = 27;
			imageRectangle.Height = 27;
			imageRectangle.MouseRightButtonDown += new MouseButtonEventHandler(RemoveTeamEquipment);

			Thickness equipmentMargin = imageRectangle.Margin;
			equipmentMargin.Right = 2;
			equipmentMargin.Left = 2;
			imageRectangle.Margin = equipmentMargin;

			//Getting the background image to the rectangle
			ImageBrush equipmentImage = new ImageBrush();
			equipmentImage.ImageSource = Services.getImage((equipments)Enum.Parse(typeof(equipments), equip));
			imageRectangle.Fill = equipmentImage;

			//Getting the appropriate equipment StackPanel
			teamEquipmentStacks[teamName].Children.Add(imageRectangle);
		}

		//Right clicking on an equipment in a team description removew the equipment from the stack and adds it back to the map
		public void RemoveTeamEquipment(object sender, RoutedEventArgs e)
		{
			Rectangle equipment = (Rectangle)sender;
			StackPanel equipmentStackPanel = (StackPanel)equipment.Parent;
			equipmentStackPanel.Children.Remove(equipment);
			mainWindow.CreateEquipmentPin(equipment.Name);
		}
	}
}
