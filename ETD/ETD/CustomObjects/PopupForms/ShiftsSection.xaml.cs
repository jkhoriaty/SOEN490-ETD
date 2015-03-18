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

        private ShiftsSection shiftsSection;
        private Shift shift;

        private Dictionary<String, TextBox> teamsMap = new Dictionary<String, TextBox>();//Contains all teams
        private Dictionary<String, TextBox> sectorsMap = new Dictionary<String, TextBox>();//Contains all sectors
        private Dictionary<String, TextBox[]> teamsectorMap = new Dictionary<String, TextBox[]>();//Contains teams which have been assigned a sector to supervise
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

        }

        //Populating the list of sectors
        private void setupSectorsMap()
        {

        }


        //Populating the list of team-sectors
        private void setupTeamSectorMap()
        {

        }

        //Populating the list of shift start time
        private void setupTeamSectorStartTimeMap()
        {

        }

        //Populating the list of shift end time
        private void setupTeamSectorEndTimeMap()
        {

        }

        //set the shift start time
        private void StartTimestampTeamSector_Click(object sender, RoutedEventArgs e)
        {

        }

        //set the shift end time
        private void EndTimestampTeamSector_Click(object sender, RoutedEventArgs e)
        {

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
                        button.Background = new SolidColorBrush(Colors.Yellow);
                        button.Foreground = new SolidColorBrush(Colors.Black);
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

        }

      
    }
}
