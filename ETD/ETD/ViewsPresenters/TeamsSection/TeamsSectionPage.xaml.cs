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
		private TeamsSectionPageUpdater updater;

		public TeamsSectionPage(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
			updater = new TeamsSectionPageUpdater(this);
		}

		//Clicking on the add team button
		private void DisplayCreateTeamForm(object sender, RoutedEventArgs e)
		{
			Frame frame = new Frame();
			frame.Content = new TeamFormPage(this);

			updater.DisplayCreateTeamForm(frame);
		}

		//Hiding form after submit or cancel
		public void HideCreateTeamForm()
		{
			updater.HideCreateTeamForm();
		}

		//Displaying the team upon form submit
		public void DisplayTeam(Team team)
		{
			updater.HideCreateTeamForm();
			Frame frame = new Frame();
			frame.Content = new TeamInfoPage(this, team);
			updater.DisplayTeamInfo(frame);
			mainWindow.DisplayTeamPin(team);
		}

		//Deleting the team upon right click on the label
		public void RemoveTeam(String teamName)
		{
			updater.RemoveTeamInfo(teamName);
			mainWindow.RemoveTeamPin(teamName);
		}

		public void RemoveTeamEquipment(object sender, RoutedEventArgs e)
		{
			Rectangle rct = (Rectangle)sender;
			updater.RemoveTeamEquipment(rct);
			mainWindow.DisplayEquipmentPin(rct.Name);
		}

		public void AddTeamEquipment(String equip, String teamName)
		{
			updater.AddTeamEquipment(equip, teamName);
		}
	}
}
