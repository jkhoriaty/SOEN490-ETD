﻿using Microsoft.Win32;
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
        MainWindow caller;

        public GPSAssignment(MainWindow caller)
        {
            this.caller = caller;
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

                combo.Items.Add(" ");

                teamGrid.Children.Add(combo);

                
                if (t.getGPSLocation() != null)
                {
                    int tempIndex = 0;

                    foreach (KeyValuePair<string, GPSLocation> entry in gpsLocationsDictionary)
                    {
                        if (entry.Value.id == t.getGPSLocation().id)
                        {
                            foreach (KeyValuePair<string, string> ID in volunteerList)
                            {
                                if (entry.Key == ID.Key)
                                {
                                    combo.SelectedIndex = tempIndex;
                                }

                                tempIndex++;
                            }
                        }
                    }
                }
                row++;
            }

        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            int teamIndex = 0;
            List<string> noDuplicates = new List<string>();
            bool duplicates = false;

            foreach (KeyValuePair<string, string> entry in volunteerList)
            {
                if (!inverseVolunteerList.ContainsKey(entry.Value))
                {
                    inverseVolunteerList.Add(entry.Value, entry.Key);
                }
            }
            

            foreach (ComboBox ctrl in teamGrid.Children.OfType<ComboBox>())
            {
				if (ctrl.SelectedItem != null)
				{
					if(ctrl.SelectedItem.ToString() != " ")
					{
						string memberID = ctrl.SelectedItem.ToString();

						if (!noDuplicates.Contains(memberID))
						{
							noDuplicates.Add(memberID);
						}
						else
							duplicates = true;

						teamList[teamIndex].setGPSLocation(gpsLocationsDictionary[inverseVolunteerList[memberID]]);
					}

					else if (ctrl.SelectedItem.ToString() == " ")
					{
						teamList[teamIndex].setGPSLocation(null);
					}
				}

                teamIndex++;
            }
            if (duplicates == false)
            {
                this.Close();
                caller.CloseGPSWindow();
            }
            else
            {
                MessageBox.Show("Each team member can be assigned to at most one team.");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            caller.CloseGPSWindow();
        }
    }
}
