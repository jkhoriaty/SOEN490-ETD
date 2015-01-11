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
using ETD.Models.Objects;
using ETD.Models.Services;

namespace ETD.ViewsPresenters.TeamsSection.TeamInfo
{
	/// <summary>
	/// Interaction logic for TeamInfoPage.xaml
	/// </summary>
	public partial class TeamInfoPage : Page
	{
		TeamsSectionPage teamsSection;
		Team team;

		public TeamInfoPage(TeamsSectionPage teamsSection, Team team)
		{
			InitializeComponent();
			this.teamsSection = teamsSection;
			this.team = team;
			populateInfo(team);
		}

		//Filling up the page with the information on the team
		private void populateInfo(Team team)
		{
			page.Name = team.getName();
			teamName.Name = team.getName();
			if (team.getName().Length == 1)
			{
				teamName.Text = TechnicalServices.getPhoneticLetter(team.getName());
			}
			else
			{
				teamName.Text = team.getName();
			}

			ImageBrush img = new ImageBrush();
			img.ImageSource = TechnicalServices.getImage(team.getHighestLevelOfTraining());
			teamTraining.Fill = img;

			TeamMember member = null;
			int position = 0;
			while ((member = team.getMember(position++)) != null)
			{
				Grid memberLine = (Grid)informations.Children[position];
				memberLine.Visibility = System.Windows.Visibility.Visible;

				Label memberName = (Label)memberLine.Children[0];
				memberName.Content = member.getName();
				memberName.ToolTip = DepartureTimeToString(member);
    
				Rectangle memberTraining = (Rectangle)memberLine.Children[1];
				ImageBrush img2 = new ImageBrush();
				img2.ImageSource = TechnicalServices.getImage(member.getTrainingLevel());
				memberTraining.Fill = img2;
			}

			teamsSection.registerStackPanel(team.getName(), equipmentStackPanel);
		}

		private String DepartureTimeToString(TeamMember member)
		{
			String departurehh = member.getDeparture().Hour.ToString();
			String departuremm = member.getDeparture().Minute.ToString();
			if(departuremm.Length == 1)
			{
				departuremm = "0" + departuremm;
			}
			return departurehh + ":" + departuremm;
		}

		//Right click on the team to remove it from the team list
		private void DeleteTeam(object sender, MouseButtonEventArgs e)
		{
			TextBlock label = (TextBlock)sender;
			teamsSection.RemoveTeam(label.Name);
		}
	}
}
