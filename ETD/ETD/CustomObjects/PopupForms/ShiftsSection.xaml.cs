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
using ETD.CustomObjects.CustomUIObjects;
using ETD.Models.ArchitecturalObjects;


namespace ETD.CustomObjects.PopupForms
{
    /// <summary>
    /// Interaction logic for ShiftsSection.xaml
    /// </summary>
    public partial class ShiftsSection : Page
    {
		//Variables used to predict the next shift time and team rotation
        Shift currentShift;
        ShiftLine shiftline;
        int teamNumber = 1;
        int rowPosition = 2;
        int columnPosition = 1;
        int sectorPosition = 2;

        int shiftDuration = 30;//Default shift duration
        private Dictionary<String, TextBox> teamMap = new Dictionary<String, TextBox>();//Contains all teams
        private Dictionary<String, TextBox> sectorMap = new Dictionary<String, TextBox>();//Contains all sectors
        private Dictionary<String, TextBox[]> durationTimeMap = new Dictionary<String, TextBox[]>();//Contains shift duration
        private Dictionary<String, TextBox[]> shiftStartTimeMap = new Dictionary<String, TextBox[]>();//Contains shift start time duration
 
        public ShiftsSection()
        {
            InitializeComponent();  
            PopulateShiftForm();
        }

		//Initialize the shift form
        private void PopulateShiftForm()
        {
            //Set up default shift properties
            currentShift = new Shift("-", "-", shiftDuration);
            shiftline = new ShiftLine(currentShift);

            shiftline.getStartTimeButton().Click += NextShiftTime;
            shiftline.getSectorNameTextBox().KeyDown += NewSector;

            RowDefinition sectorRowDefinition = new RowDefinition();
            sectorRowDefinition.Height = new GridLength(50);
            Shifts_grid.RowDefinitions.Add(sectorRowDefinition);

            //populate sector
            Shifts_grid.Children.Add(shiftline.getSectorNameTextBox());
            Grid.SetColumn(shiftline.getSectorNameTextBox(), 0);
            Grid.SetRow(shiftline.getSectorNameTextBox(), rowPosition);

            Grid.SetColumn(shiftline.getSectorNameBorder(), 0);
            Grid.SetRow(shiftline.getSectorNameBorder(), rowPosition);
            sectorMap.Add("Sector" + (rowPosition - 1).ToString(), shiftline.getSectorNameTextBox());

            //populate team
            Shifts_grid.Children.Add(shiftline.getTeamNameTextBox());
            Grid.SetColumn(shiftline.getTeamNameTextBox(), columnPosition);
            Grid.SetRow(shiftline.getTeamNameTextBox(), rowPosition);

            teamMap.Add("Team" + (teamNumber).ToString() + "Sector" + (sectorPosition - 1), shiftline.getTeamNameTextBox());

            //populate time
            Shifts_grid.Children.Add(shiftline.getStartTimeBorder());
            Grid.SetColumn(shiftline.getStartTimeBorder(), columnPosition);
            Grid.SetRow(shiftline.getStartTimeBorder(), 1);

            shiftStartTimeMap.Add("Start Time " + (columnPosition).ToString(), TextBoxHandler.textboxArray(shiftline.getStartTimeHHTextBox(), shiftline.getStartTimeMMTextBox()));

        }

		//Predict the next shift time and team rotation
        public void NextShiftTime(object sender, RoutedEventArgs e)
        {
            rowPosition++;
            columnPosition++;
            teamNumber++;
            int sectorRowPosition = 2;
            shiftDuration = Convert.ToInt32(ShiftDuration.Text.ToString());

            //Create a new shift
            ShiftLine newShift = new ShiftLine(currentShift);
            newShift.getStartTimeButton().Click += NextShiftTime;

            //Set up new shift properties
            ColumnDefinition colDefinition = new ColumnDefinition();
            colDefinition.Width =  new GridLength(172);
            Shifts_grid.ColumnDefinitions.Add(colDefinition);

            Shifts_grid.Children.Add(newShift.getStartTimeBorder());
            Grid.SetColumn(newShift.getStartTimeBorder(), columnPosition);
            Grid.SetRow(newShift.getStartTimeBorder(), 1);

            shiftStartTimeMap.Add("Start Time " + (columnPosition).ToString(), TextBoxHandler.textboxArray(newShift.getStartTimeHHTextBox(), newShift.getStartTimeMMTextBox()));

            //Predict next shift
            String newShiftHH = shiftStartTimeMap["Start Time " + (columnPosition -1).ToString()][0].Text.ToString();
            String newShiftMM = shiftStartTimeMap["Start Time " + (columnPosition -1).ToString()][1].Text.ToString();

			bool isallDigitsNewShiftsHH = newShiftHH.All(char.IsDigit);
			bool isallDigitsNewShiftsMM = newShiftMM.All(char.IsDigit);

            if (!newShiftHH.Equals("hh") && !newShiftMM.Equals("mm"))
			{
				if (isallDigitsNewShiftsHH && isallDigitsNewShiftsMM)//If the time input is a number, predict next shift time
				{
					if ((Convert.ToInt32(newShiftMM) + shiftDuration) >= 60)//If the next shift time is higher than 60,Loop back to 0min plus the difference between the minutes fromt he old and new shift
					{
						int newMM = (Convert.ToInt32(newShiftMM) + shiftDuration) - 60;
						newShift.getStartTimeMMTextBox().Text = Convert.ToString(newMM);

						if (Convert.ToInt32(newShiftHH) == 12)//Loop back to 1am
						{
							newShift.getStartTimeHHTextBox().Text = "1";
						}
						else
						{
							newShift.getStartTimeHHTextBox().Text = Convert.ToString(Convert.ToInt32(newShiftHH) + 1);
						}

					}
					else//Shift time is equal to the input time plus the shift duration
					{
						newShift.getStartTimeMMTextBox().Text = Convert.ToString(Convert.ToInt32(newShiftMM) + shiftDuration);
						newShift.getStartTimeHHTextBox().Text = Convert.ToString(Convert.ToInt32(newShiftHH));
					}  
				}      
            }

            /*Team rotation
			The next team to be assigned a sector(eg: sector 1) is the one that was responsible for the other sector (eg : sector 2)*/
            int originalshiftpos = 2;
            int sector = 2;
           
            if (sectorMap.Count() == 1)//if there is only one sector and one team, no team rotation
            {
                ShiftLine newShift2 = new ShiftLine(currentShift);
                Shifts_grid.Children.Add(newShift2.getTeamNameTextBox());
                Grid.SetColumn(newShift2.getTeamNameTextBox(), columnPosition);
                Grid.SetRow(newShift2.getTeamNameTextBox(), originalshiftpos);

                teamMap.Add("Team" + (teamNumber).ToString() + "Sector" + (sectorRowPosition - 1).ToString(), newShift2.getTeamNameTextBox());
            }

            for (int i = 0; i < sectorMap.Count() && sectorMap.Count()>1 ; i++)//if there is more than one sector, predict the next team that will be replacing the team currently assigned in that sector
            {
                ShiftLine newShift2 = new ShiftLine(currentShift);
                String rotatedTeam;

                Shifts_grid.Children.Add(newShift2.getTeamNameTextBox());
                Grid.SetColumn(newShift2.getTeamNameTextBox(), columnPosition);
                Grid.SetRow(newShift2.getTeamNameTextBox(), originalshiftpos);

                teamMap.Add("Team" + (teamNumber).ToString() + "Sector" + (sectorRowPosition - 1).ToString(), newShift2.getTeamNameTextBox());

                if ((sectorMap.Count()) == (sectorRowPosition - 1))//If the number of sectors is the same as the its row position, the next team assigned to the sector is the one that was responsible for the first sector
                {
                    rotatedTeam = teamMap["Team" + (teamNumber - 1) + "Sector" + 1.ToString()].Text.ToString();
                    teamMap["Team" + (teamNumber).ToString() + "Sector" + (sectorRowPosition - 1).ToString()].Text = rotatedTeam;
                }
                else//The next team assigned to the sector is the one that was responsible for the one under it. eg: team A on sector 1 will be responsible for sector 2 and team B sector 2 will be responsible for sector 1
                {
                    rotatedTeam = teamMap["Team" + (teamNumber - 1) + "Sector" + (sector).ToString()].Text.ToString();
                    teamMap["Team" + (teamNumber).ToString() + "Sector" + (sectorRowPosition - 1).ToString()].Text = rotatedTeam;     
                }

                sector++;
                sectorRowPosition++;
                originalshiftpos++;
            }  
        }

		//Create a new sector when the user clicks the enter key
        public void NewSector(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {      
                //Set up a new sector field
                sectorPosition++;
                teamNumber = 1;
                ShiftLine newShift = new ShiftLine(currentShift);
                newShift.getSectorNameTextBox().KeyDown += NewSector;

                RowDefinition sectorRowDefinition = new RowDefinition();
                sectorRowDefinition.Height = new GridLength(50);
                Shifts_grid.RowDefinitions.Add(sectorRowDefinition);

                Shifts_grid.Children.Add(newShift.getSectorNameTextBox());
                Grid.SetRow(newShift.getSectorNameTextBox(), sectorPosition);

                sectorMap.Add("Sector" + (sectorPosition - 1).ToString(), newShift.getSectorNameTextBox());

                //Add a new row of teams
                Shifts_grid.Children.Add(newShift.getTeamNameTextBox());
                Grid.SetRow(newShift.getTeamNameTextBox(), sectorPosition);
                Grid.SetColumn(newShift.getTeamNameTextBox(), 1);

                teamMap.Add("Team" + (teamNumber).ToString() + "Sector" + (sectorPosition - 1).ToString(), newShift.getTeamNameTextBox());

                for (int i = 0; i < columnPosition-1; i++)
                {
                    teamNumber++;
                    ShiftLine newShift3 = new ShiftLine(currentShift);
                    Shifts_grid.Children.Add(newShift3.getTeamNameTextBox());
                    Grid.SetColumn(newShift3.getTeamNameTextBox(), i+2);
                    Grid.SetRow(newShift3.getTeamNameTextBox(), sectorPosition);

                    teamMap.Add("Team" + (teamNumber).ToString() + "Sector" + (sectorPosition - 1).ToString(), newShift3.getTeamNameTextBox());
                }
            }
        }

        //Focus on the follow up or completion time stamp text box
        private void TextBoxes_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxHandler.GotFocus(sender, e);
        }

        //Recovering the fields default text if left empty
        private void TextBoxes_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBoxHandler.LostFocus(sender, e);
        }
    }
}
