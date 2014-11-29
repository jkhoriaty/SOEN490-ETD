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

using ETD.Models;

namespace ETD.ViewsPresenters.TeamsSection.TeamInfo
{
	/// <summary>
	/// Interaction logic for TeamInfoPage.xaml
	/// </summary>
	public partial class TeamInfoPage : Page
	{
		TeamsSectionPage teamsSection;

		public TeamInfoPage(TeamsSectionPage teamsSection, Team team)
		{
			InitializeComponent();
			this.teamsSection = teamsSection;
			populateInfo(team);
		}

		//Filling up the page with the information on the team
		private void populateInfo(Team team)
		{
			page.Name = team.getName();
			teamName.Name = team.getName();
			if (team.getName().Length == 1)
			{
				teamName.Content = Services.getPhoneticLetter(team.getName());
			}
			else
			{
				teamName.Content = team.getName();
			}

			ImageBrush img = new ImageBrush();
			img.ImageSource = Services.getImage(team.getHighestLevelOfTraining());
			teamTraining.Fill = img;

			TeamMember member = null;
			int position = 0;
			while ((member = team.getMember(position++)) != null)
			{
				Grid memberLine = (Grid)informations.Children[position];
				memberLine.Visibility = System.Windows.Visibility.Visible;

				Label memberName = (Label)memberLine.Children[0];
				memberName.Content = member.getName();

				Rectangle memberTraining = (Rectangle)memberLine.Children[1];
				ImageBrush img2 = new ImageBrush();
				img2.ImageSource = Services.getImage(member.getTrainingLevel());
				memberTraining.Fill = img2;
			}

			teamsSection.registerStackPanel(team.getName(), equipmentStackPanel);
		}

		//Right click on the team to remove it from the team list
		private void DeleteTeam(object sender, MouseButtonEventArgs e)
		{
			Label label = (Label)sender;
			teamsSection.RemoveTeam(label.Name);
		}
	}
}
