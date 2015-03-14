using Microsoft.Win32;
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
using ETD.ViewsPresenters.TeamsSection;
using ETD.ViewsPresenters.MapSection;
using ETD.ViewsPresenters.InterventionsSection;
using ETD.Models.Objects;
using System.Windows.Threading;
using System.Drawing;
using ETD.ViewsPresenters.ScheduleSection;
using ETD.Services;
using System.Threading;
using System.Windows.Controls.Primitives;
using ETD.CustomObjects.PopupForms;
using ETD.CustomObjects.CustomUIObjects;
using ETD.Models.ArchitecturalObjects;
using System.Globalization;
using System.Diagnostics;
using ETD.Services.Interfaces;

namespace ETD.ViewsPresenters
{
    /// <summary>
    /// Interaction logic for GPSAssignment.xaml
    /// </summary>
    public partial class GPSAssignment : Window
    {

        List<Team> teamList = new List<Team>();//Contains a list of teams

        public GPSAssignment()
        {
            InitializeComponent();

            teamList = Team.getTeamList();

            foreach (Team team in teamList)
            {
                List<TeamMember> temp = new List<TeamMember>();
                temp = team.getMemberList();
                teamCB.Items.Add(team.getName());

                foreach (TeamMember member in temp)
                {
                    memberCB.Items.Add(member.getName());
                }
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
                
                /*The code to link the chosen GPS signal with the team
                 * member selected above goes here.
                 * TODO: Finish this tonight. ~Greg*/

                this.Close();
        }
    }
}
