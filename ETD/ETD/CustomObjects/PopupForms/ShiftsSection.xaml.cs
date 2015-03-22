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
    public partial class ShiftsSection : Page, Observer
    {

        ShiftsSection shiftsSectionForm;
        Shift currentShift;
        ShiftLine shiftline;
        int teamNumber = 1;
        int rowPosition = 2;
        int columnPosition = 1;

        int sectorRowPosition = 2;

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

        private void PopulateShiftForm()
        {

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

               // teamMap.Add("Team" + (teamNumber).ToString(), shiftline.getTeamNameTextBox());

                //populate time

                Shifts_grid.Children.Add(shiftline.getStartTimeBorder());
                Grid.SetColumn(shiftline.getStartTimeBorder(), columnPosition);
                Grid.SetRow(shiftline.getStartTimeBorder(), 1);

                shiftStartTimeMap.Add("Start Time " + (rowPosition - 1).ToString(), TextBoxHandler.textboxArray(shiftline.getStartTimeHHTextBox(), shiftline.getStartTimeMMTextBox()));
    
        }

        public void NextShiftTime(object sender, RoutedEventArgs e)
        {

                rowPosition++;
                columnPosition++;
                teamNumber++;
                ShiftLine newShift = new ShiftLine(currentShift);
                newShift.getStartTimeButton().Click += NextShiftTime;
      
                ColumnDefinition colDefinition = new ColumnDefinition();
                colDefinition.Width =  new GridLength(172);

                Shifts_grid.ColumnDefinitions.Add(colDefinition);

                Shifts_grid.Children.Add(newShift.getStartTimeBorder());
                Grid.SetColumn(newShift.getStartTimeBorder(), columnPosition);
                Grid.SetRow(newShift.getStartTimeBorder(), 1);

                shiftStartTimeMap.Add("Start Time " + (rowPosition - 1).ToString(), TextBoxHandler.textboxArray(newShift.getStartTimeHHTextBox(), newShift.getStartTimeMMTextBox()));

                int originalshiftpos = 2;
                for (int i = 0; i < sectorMap.Count(); i++)
                {
                    ShiftLine newShift2 = new ShiftLine(currentShift);

                    //team
                    Shifts_grid.Children.Add(newShift2.getTeamNameTextBox());
                    Grid.SetColumn(newShift2.getTeamNameTextBox(), columnPosition);
                    Grid.SetRow(newShift2.getTeamNameTextBox(), originalshiftpos);

                   // teamMap.Add("Team" + (teamNumber).ToString() + "s" + (columnPosition - 1).ToString(), newShift2.getTeamNameTextBox());
                    originalshiftpos++;
                }
        }

        public void NewSector(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                
                    sectorRowPosition++;
                    ShiftLine newShift = new ShiftLine(currentShift);
                    newShift.getSectorNameTextBox().KeyDown += NewSector;

                    RowDefinition sectorRowDefinition = new RowDefinition();
                    sectorRowDefinition.Height = new GridLength(50);
                    Shifts_grid.RowDefinitions.Add(sectorRowDefinition);

                    Shifts_grid.Children.Add(newShift.getSectorNameTextBox());
                    Grid.SetRow(newShift.getSectorNameTextBox(), sectorRowPosition);

                    sectorMap.Add("Sector" + (sectorRowPosition - 1).ToString(), newShift.getSectorNameTextBox());

                    //team
                    Shifts_grid.Children.Add(newShift.getTeamNameTextBox());
           
                        Grid.SetColumn(newShift.getTeamNameTextBox(), columnPosition);
                        Grid.SetRow(newShift.getTeamNameTextBox(), sectorRowPosition);
                    


                    //teamMap.Add("Team" + (teamNumber - 1).ToString() + "s" + (sectorRowPosition - 1).ToString(), newShift.getTeamNameTextBox());

                    for (int i = 0; i < sectorMap.Count(); i++)
                    {
                        ShiftLine newShift3 = new ShiftLine(currentShift);

                 
                        Shifts_grid.Children.Add(newShift3.getTeamNameTextBox());
                        Grid.SetColumn(newShift3.getTeamNameTextBox(), i+1);
                        Grid.SetRow(newShift3.getTeamNameTextBox(), sectorRowPosition);

                        // teamMap.Add("Team" + (teamNumber).ToString() + "s" + (columnPosition - 1).ToString(), newShift2.getTeamNameTextBox());
 
                    }

            }
        }

        public void Update()
        {
           // buildshiftcomponent();
        }

        public void getShiftDuration(object sender, System.EventArgs e)
        {

        }

    }
}
