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
		TeamInfoPageUpdater updater;

		public TeamInfoPage(TeamsSectionPage teamsSection, Team team)
		{
			InitializeComponent();
			this.teamsSection = teamsSection;
			updater = new TeamInfoPageUpdater(this);
			populateInfo(team);
		}

		private void populateInfo(Team team)
		{
			String phoneticLetter = "";
			if (team.getName().Length == 1)
			{
				phoneticLetter = Services.getPhoneticLetter(team.getName());
			}
			else
			{
				phoneticLetter = team.getName();
			}
			updater.setTeamInfo(team.getName(), phoneticLetter, Services.getImage(team.getHighestLevelOfTraining()));

			TeamMember member = null;
			int i = 0;
			while ((member = team.getMember(i++)) != null)
			{
				updater.setMemberInfo(i, member.getName(), Services.getImage(member.getTrainingLevel()));
			}
		}

		private void DeleteTeam(object sender, MouseButtonEventArgs e)
		{
			Label label = (Label)sender;
			teamsSection.RemoveTeam(label.Name);
		}
	}
}
