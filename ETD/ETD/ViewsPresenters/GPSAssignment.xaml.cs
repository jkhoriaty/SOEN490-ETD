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
        Dictionary<string, GPSLocation> gpsLocationsDictionary = new Dictionary<string, GPSLocation>();
        Dictionary<string, string> volunteerList = new Dictionary<string,string>();
        Dictionary<string, string> inverseVolunteerList = new Dictionary<string, string>();

        public GPSAssignment()
        {
            teamList = Team.getTeamList();
            gpsLocationsDictionary = GPSLocation.getDictionary();
            int row = 0;
            InitializeComponent();

            foreach (Team t in teamList)
            {
                Label teamName = new Label();
                teamName.Content = t.getName();
                Grid.SetRow(teamName, row);
                Grid.SetColumn(teamName, 0);

                teamGrid.Children.Add(teamName);

                ComboBox combo = new ComboBox();
                String forName = row.ToString();
                //combo.Name = forName;
                Grid.SetRow(combo, row);
                Grid.SetColumn(combo, 1);

                List<TeamMember> tempList = new List<TeamMember>();
                tempList = t.getMemberList();

                volunteerList = GPSServices.getUsers();

                foreach (KeyValuePair<string, string> entry in volunteerList)
                {
                    combo.Items.Add(entry.Value);
                }

                teamGrid.Children.Add(combo);
                row++;
            }

        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
           /* foreach (ComboBox ctrl in teamGrid.Children)
            {
                int index = Convert.ToInt32(ctrl.Name);
                string index2 = ctrl.SelectedItem.ToString();
                teamList[index].setGPSLocation(gpsLocationsDictionary[index2]);
            }*/

            int teamIndex = 0;

            foreach (KeyValuePair<string, string> entry in volunteerList)
            {
                inverseVolunteerList.Add(entry.Value, entry.Key);
            }

            foreach (ComboBox ctrl in teamGrid.Children.OfType<ComboBox>())
            {
                ComboBox temp = new ComboBox();
                temp = ctrl;
                if (temp.SelectedItem != null)
                {
                    string memberID = temp.SelectedItem.ToString();

                    MessageBox.Show(memberID);

                    teamList[teamIndex].setGPSLocation(gpsLocationsDictionary[inverseVolunteerList[memberID]]);         
                }   
            }
                this.Close();
        }
    }
}
