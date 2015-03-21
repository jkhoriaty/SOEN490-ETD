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
using ETD.Services;

namespace ETD.CustomObjects.PopupForms
{
    /// <summary>
    /// Interaction logic for ShiftsSection.xaml
    /// </summary>
    public partial class ShiftsSection : Page
    {
        String sectorStatus = "Unsupervised";//Default sector status
        int shiftDuration = 30;//Default shift duration

        private Dictionary<String, TextBox> teamMap = new Dictionary<String, TextBox>();//Contains all teams
        private Dictionary<String, TextBox> sectorMap = new Dictionary<String, TextBox>();//Contains all sectors
        private Dictionary<String, String> sectorStatusMap = new Dictionary<String, String>();//Contains teams which have been assigned a sector to supervise. Used to check the shift's status
        private Dictionary<String, TextBox[]> teamsectorStartTimeMap = new Dictionary<String, TextBox[]>();//Contains team shift start 
        private Dictionary<String, TextBox[]> teamsectorEndTimeMap = new Dictionary<String, TextBox[]>();//Contains team shift end

        public ShiftsSection()
        {
            InitializeComponent();

            setupTeamMap();
            setupSectorsMap();
            setupTeamSectorMap();
            setupTeamSectorStartTimeMap();
            setupTeamSectorEndTimeMap();

        }

        //Populating the list of teams
        private void setupTeamMap()
        {
            teamMap.Add("Team1", Team1);
            teamMap.Add("Team2", Team2);
            teamMap.Add("Team3", Team3);
            teamMap.Add("Team4", Team4);
            teamMap.Add("Team5", Team5);
            teamMap.Add("Team6", Team6);
        }

        //Populating the list of sectors
        private void setupSectorsMap()
        {
            sectorMap.Add("Sector1", Sector1);
            sectorMap.Add("Sector2", Sector2);
            sectorMap.Add("Sector3", Sector3);
            sectorMap.Add("Sector4", Sector4);
            sectorMap.Add("Sector5", Sector5);
            sectorMap.Add("Sector6", Sector6);
        }


        //Sets the initial sector status to unsupervised
        private void setupTeamSectorMap()
        {
            sectorStatusMap.Add("Sector1", "Unsupervised");
            sectorStatusMap.Add("Sector2", "Unsupervised");
            sectorStatusMap.Add("Sector3", "Unsupervised");
            sectorStatusMap.Add("Sector4", "Unsupervised");
            sectorStatusMap.Add("Sector5", "Unsupervised");
            sectorStatusMap.Add("Sector6", "Unsupervised");
            sectorStatusMap.Add("Sector7", "Unsupervised");
        }

        //Populating the list of shift start time
        private void setupTeamSectorStartTimeMap()
        {
            teamsectorStartTimeMap.Add("TimestampT1L1S", TextBoxHandler.textboxArray(TimestamphhT1L1S, TimestampmmT1L1S));
            teamsectorStartTimeMap.Add("TimestampT1L2S", TextBoxHandler.textboxArray(TimestamphhT1L2S, TimestampmmT1L2S));
            teamsectorStartTimeMap.Add("TimestampT1L3S", TextBoxHandler.textboxArray(TimestamphhT1L3S, TimestampmmT1L3S));
            teamsectorStartTimeMap.Add("TimestampT1L4S", TextBoxHandler.textboxArray(TimestamphhT1L4S, TimestampmmT1L4S));
            teamsectorStartTimeMap.Add("TimestampT1L5S", TextBoxHandler.textboxArray(TimestamphhT1L5S, TimestampmmT1L5S));
            teamsectorStartTimeMap.Add("TimestampT1L6S", TextBoxHandler.textboxArray(TimestamphhT1L6S, TimestampmmT1L6S));
            teamsectorStartTimeMap.Add("TimestampT1L7S", TextBoxHandler.textboxArray(TimestamphhT1L7S, TimestampmmT1L7S));

            teamsectorStartTimeMap.Add("TimestampT2L1S", TextBoxHandler.textboxArray(TimestamphhT2L1S, TimestampmmT2L1S));
            teamsectorStartTimeMap.Add("TimestampT2L2S", TextBoxHandler.textboxArray(TimestamphhT2L2S, TimestampmmT2L2S));
            teamsectorStartTimeMap.Add("TimestampT2L3S", TextBoxHandler.textboxArray(TimestamphhT2L3S, TimestampmmT2L3S));
            teamsectorStartTimeMap.Add("TimestampT2L4S", TextBoxHandler.textboxArray(TimestamphhT2L4S, TimestampmmT2L4S));
            teamsectorStartTimeMap.Add("TimestampT2L5S", TextBoxHandler.textboxArray(TimestamphhT2L5S, TimestampmmT2L5S));
            teamsectorStartTimeMap.Add("TimestampT2L6S", TextBoxHandler.textboxArray(TimestamphhT2L6S, TimestampmmT2L6S));
            teamsectorStartTimeMap.Add("TimestampT2L7S", TextBoxHandler.textboxArray(TimestamphhT2L7S, TimestampmmT2L7S));

            teamsectorStartTimeMap.Add("TimestampT3L1S", TextBoxHandler.textboxArray(TimestamphhT3L1S, TimestampmmT3L1S));
            teamsectorStartTimeMap.Add("TimestampT3L2S", TextBoxHandler.textboxArray(TimestamphhT3L2S, TimestampmmT3L2S));
            teamsectorStartTimeMap.Add("TimestampT3L3S", TextBoxHandler.textboxArray(TimestamphhT3L3S, TimestampmmT3L3S));
            teamsectorStartTimeMap.Add("TimestampT3L4S", TextBoxHandler.textboxArray(TimestamphhT3L4S, TimestampmmT3L4S));
            teamsectorStartTimeMap.Add("TimestampT3L5S", TextBoxHandler.textboxArray(TimestamphhT3L5S, TimestampmmT3L5S));
            teamsectorStartTimeMap.Add("TimestampT3L6S", TextBoxHandler.textboxArray(TimestamphhT3L6S, TimestampmmT3L6S));
            teamsectorStartTimeMap.Add("TimestampT3L7S", TextBoxHandler.textboxArray(TimestamphhT3L7S, TimestampmmT3L7S));

            teamsectorStartTimeMap.Add("TimestampT4L1S", TextBoxHandler.textboxArray(TimestamphhT4L1S, TimestampmmT4L1S));
            teamsectorStartTimeMap.Add("TimestampT4L2S", TextBoxHandler.textboxArray(TimestamphhT4L2S, TimestampmmT4L2S));
            teamsectorStartTimeMap.Add("TimestampT4L3S", TextBoxHandler.textboxArray(TimestamphhT4L3S, TimestampmmT4L3S));
            teamsectorStartTimeMap.Add("TimestampT4L4S", TextBoxHandler.textboxArray(TimestamphhT4L4S, TimestampmmT4L4S));
            teamsectorStartTimeMap.Add("TimestampT4L5S", TextBoxHandler.textboxArray(TimestamphhT4L5S, TimestampmmT4L5S));
            teamsectorStartTimeMap.Add("TimestampT4L6S", TextBoxHandler.textboxArray(TimestamphhT4L6S, TimestampmmT4L6S));
            teamsectorStartTimeMap.Add("TimestampT4L7S", TextBoxHandler.textboxArray(TimestamphhT4L7S, TimestampmmT4L7S));

            teamsectorStartTimeMap.Add("TimestampT5L1S", TextBoxHandler.textboxArray(TimestamphhT5L1S, TimestampmmT5L1S));
            teamsectorStartTimeMap.Add("TimestampT5L2S", TextBoxHandler.textboxArray(TimestamphhT5L2S, TimestampmmT5L2S));
            teamsectorStartTimeMap.Add("TimestampT5L3S", TextBoxHandler.textboxArray(TimestamphhT5L3S, TimestampmmT5L3S));
            teamsectorStartTimeMap.Add("TimestampT5L4S", TextBoxHandler.textboxArray(TimestamphhT5L4S, TimestampmmT5L4S));
            teamsectorStartTimeMap.Add("TimestampT5L5S", TextBoxHandler.textboxArray(TimestamphhT5L5S, TimestampmmT5L5S));
            teamsectorStartTimeMap.Add("TimestampT5L6S", TextBoxHandler.textboxArray(TimestamphhT5L6S, TimestampmmT5L6S));
            teamsectorStartTimeMap.Add("TimestampT5L7S", TextBoxHandler.textboxArray(TimestamphhT5L7S, TimestampmmT5L7S));

            teamsectorStartTimeMap.Add("TimestampT6L1S", TextBoxHandler.textboxArray(TimestamphhT6L1S, TimestampmmT6L1S));
            teamsectorStartTimeMap.Add("TimestampT6L2S", TextBoxHandler.textboxArray(TimestamphhT6L2S, TimestampmmT6L2S));
            teamsectorStartTimeMap.Add("TimestampT6L3S", TextBoxHandler.textboxArray(TimestamphhT6L3S, TimestampmmT6L3S));
            teamsectorStartTimeMap.Add("TimestampT6L4S", TextBoxHandler.textboxArray(TimestamphhT6L4S, TimestampmmT6L4S));
            teamsectorStartTimeMap.Add("TimestampT6L5S", TextBoxHandler.textboxArray(TimestamphhT6L5S, TimestampmmT6L5S));
            teamsectorStartTimeMap.Add("TimestampT6L6S", TextBoxHandler.textboxArray(TimestamphhT6L6S, TimestampmmT6L6S));
            teamsectorStartTimeMap.Add("TimestampT6L7S", TextBoxHandler.textboxArray(TimestamphhT6L7S, TimestampmmT6L7S));
        }

        //Populating the list of shift end time
        private void setupTeamSectorEndTimeMap()
        {
            teamsectorEndTimeMap.Add("TimestampT1L1E", TextBoxHandler.textboxArray(TimestamphhT1L1E, TimestampmmT1L1E));
            teamsectorEndTimeMap.Add("TimestampT1L2E", TextBoxHandler.textboxArray(TimestamphhT1L2E, TimestampmmT1L2E));
            teamsectorEndTimeMap.Add("TimestampT1L3E", TextBoxHandler.textboxArray(TimestamphhT1L3E, TimestampmmT1L3E));
            teamsectorEndTimeMap.Add("TimestampT1L4E", TextBoxHandler.textboxArray(TimestamphhT1L4E, TimestampmmT1L4E));
            teamsectorEndTimeMap.Add("TimestampT1L5E", TextBoxHandler.textboxArray(TimestamphhT1L5E, TimestampmmT1L5E));
            teamsectorEndTimeMap.Add("TimestampT1L6E", TextBoxHandler.textboxArray(TimestamphhT1L6E, TimestampmmT1L6E));
            teamsectorEndTimeMap.Add("TimestampT1L7E", TextBoxHandler.textboxArray(TimestamphhT1L7E, TimestampmmT1L7E));

            teamsectorEndTimeMap.Add("TimestampT2L1E", TextBoxHandler.textboxArray(TimestamphhT2L1E, TimestampmmT2L1E));
            teamsectorEndTimeMap.Add("TimestampT2L2E", TextBoxHandler.textboxArray(TimestamphhT2L2E, TimestampmmT2L2E));
            teamsectorEndTimeMap.Add("TimestampT2L3E", TextBoxHandler.textboxArray(TimestamphhT2L3E, TimestampmmT2L3E));
            teamsectorEndTimeMap.Add("TimestampT2L4E", TextBoxHandler.textboxArray(TimestamphhT2L4E, TimestampmmT2L4E));
            teamsectorEndTimeMap.Add("TimestampT2L5E", TextBoxHandler.textboxArray(TimestamphhT2L5E, TimestampmmT2L5E));
            teamsectorEndTimeMap.Add("TimestampT2L6E", TextBoxHandler.textboxArray(TimestamphhT2L6E, TimestampmmT2L6E));
            teamsectorEndTimeMap.Add("TimestampT2L7E", TextBoxHandler.textboxArray(TimestamphhT2L7E, TimestampmmT2L7E));

            teamsectorEndTimeMap.Add("TimestampT3L1E", TextBoxHandler.textboxArray(TimestamphhT3L1E, TimestampmmT3L1E));
            teamsectorEndTimeMap.Add("TimestampT3L2E", TextBoxHandler.textboxArray(TimestamphhT3L2E, TimestampmmT3L2E));
            teamsectorEndTimeMap.Add("TimestampT3L3E", TextBoxHandler.textboxArray(TimestamphhT3L3E, TimestampmmT3L3E));
            teamsectorEndTimeMap.Add("TimestampT3L4E", TextBoxHandler.textboxArray(TimestamphhT3L4E, TimestampmmT3L4E));
            teamsectorEndTimeMap.Add("TimestampT3L5E", TextBoxHandler.textboxArray(TimestamphhT3L5E, TimestampmmT3L5E));
            teamsectorEndTimeMap.Add("TimestampT3L6E", TextBoxHandler.textboxArray(TimestamphhT3L6E, TimestampmmT3L6E));
            teamsectorEndTimeMap.Add("TimestampT3L7E", TextBoxHandler.textboxArray(TimestamphhT3L7E, TimestampmmT3L7E));

            teamsectorEndTimeMap.Add("TimestampT4L1E", TextBoxHandler.textboxArray(TimestamphhT4L1E, TimestampmmT4L1E));
            teamsectorEndTimeMap.Add("TimestampT4L2E", TextBoxHandler.textboxArray(TimestamphhT4L2E, TimestampmmT4L2E));
            teamsectorEndTimeMap.Add("TimestampT4L3E", TextBoxHandler.textboxArray(TimestamphhT4L3E, TimestampmmT4L3E));
            teamsectorEndTimeMap.Add("TimestampT4L4E", TextBoxHandler.textboxArray(TimestamphhT4L4E, TimestampmmT4L4E));
            teamsectorEndTimeMap.Add("TimestampT4L5E", TextBoxHandler.textboxArray(TimestamphhT4L5E, TimestampmmT4L5E));
            teamsectorEndTimeMap.Add("TimestampT4L6E", TextBoxHandler.textboxArray(TimestamphhT4L6E, TimestampmmT4L6E));
            teamsectorEndTimeMap.Add("TimestampT4L7E", TextBoxHandler.textboxArray(TimestamphhT4L7E, TimestampmmT4L7E));

            teamsectorEndTimeMap.Add("TimestampT5L1E", TextBoxHandler.textboxArray(TimestamphhT5L1E, TimestampmmT5L1E));
            teamsectorEndTimeMap.Add("TimestampT5L2E", TextBoxHandler.textboxArray(TimestamphhT5L2E, TimestampmmT5L2E));
            teamsectorEndTimeMap.Add("TimestampT5L3E", TextBoxHandler.textboxArray(TimestamphhT5L3E, TimestampmmT5L3E));
            teamsectorEndTimeMap.Add("TimestampT5L4E", TextBoxHandler.textboxArray(TimestamphhT5L4E, TimestampmmT5L4E));
            teamsectorEndTimeMap.Add("TimestampT5L5E", TextBoxHandler.textboxArray(TimestamphhT5L5E, TimestampmmT5L5E));
            teamsectorEndTimeMap.Add("TimestampT5L6E", TextBoxHandler.textboxArray(TimestamphhT5L6E, TimestampmmT5L6E));
            teamsectorEndTimeMap.Add("TimestampT5L7E", TextBoxHandler.textboxArray(TimestamphhT5L7E, TimestampmmT5L7E));

            teamsectorEndTimeMap.Add("TimestampT6L1E", TextBoxHandler.textboxArray(TimestamphhT6L1E, TimestampmmT6L1E));
            teamsectorEndTimeMap.Add("TimestampT6L2E", TextBoxHandler.textboxArray(TimestamphhT6L2E, TimestampmmT6L2E));
            teamsectorEndTimeMap.Add("TimestampT6L3E", TextBoxHandler.textboxArray(TimestamphhT6L3E, TimestampmmT6L3E));
            teamsectorEndTimeMap.Add("TimestampT6L4E", TextBoxHandler.textboxArray(TimestamphhT6L4E, TimestampmmT6L4E));
            teamsectorEndTimeMap.Add("TimestampT6L5E", TextBoxHandler.textboxArray(TimestamphhT6L5E, TimestampmmT6L5E));
            teamsectorEndTimeMap.Add("TimestampT6L6E", TextBoxHandler.textboxArray(TimestamphhT6L6E, TimestampmmT6L6E));
            teamsectorEndTimeMap.Add("TimestampT6L7E", TextBoxHandler.textboxArray(TimestamphhT6L7E, TimestampmmT6L7E));
        }

        //set the shift start time
        private void StartTimestampTeamSector_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            TextBoxHandler.setNow(teamsectorStartTimeMap[bt.Name][0], teamsectorStartTimeMap[bt.Name][1]);
        }

        //set the shift end time
        private void EndTimestampTeamSector_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            TextBoxHandler.setNow(teamsectorEndTimeMap[bt.Name][0], teamsectorEndTimeMap[bt.Name][1]);
        }

        //Start shift
        private void StartShift_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            
            //Checks team
            for (int team = 1; team < 7; )
            {
                //Checks team sector
                for (int sector = 1; sector < 8; )
                {
                    //If starttimestamp and endtimestamp not null
                    if (button != null && button.Name.Equals("Team" + team.ToString() + "Sector" + sector.ToString() + "ShiftStart"))
                    {
                        button.Content = "O";
                        sectorStatus = "Supervised";
                        button.Background = new SolidColorBrush(Colors.Yellow);
                        button.Foreground = new SolidColorBrush(Colors.Black);
                        sectorStatusMap["Sector" + sector.ToString()] = sectorStatus;
                        return;
                    }
                    else
                    {
                        sector++;
                    }
                }
                team++;
            } 
        }

        //Focus on either the team or sectors
        private void ShiftsSection_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxHandler.GotFocus(sender, e);
        }

        //Focus on the team-sector time stamp text box
        private void TextBoxes_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxHandler.GotFocus(sender, e);
        }

        //Recovering the team-sector fields default text if left empty
        private void TextBoxes_LostFocus(object sender, RoutedEventArgs e)
        {
           TextBoxHandler.LostFocus(sender, e);
           
        }

        //Display a timer when a shift has been set to started
        private void displayShiftStarted()
        {

        }

        //Alert the user when a shift is about to end
        private void displayShiftEndNotice()
        {
            /*set sector status to unsupervised
             * reset start/end timer to 00
             * */
            sectorStatus = "Unsupervised";
        }

      
    }
}
