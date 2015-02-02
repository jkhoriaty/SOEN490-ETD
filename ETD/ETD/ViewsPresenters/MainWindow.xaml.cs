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
using ETD.Services;
using System.Threading;

namespace ETD.ViewsPresenters
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
    public partial class MainWindow
	{
		private TeamsSectionPage teamsSection;
		private MapSectionPage mapSection;
		private InterventionSectionPage interventionsSection;
        private AdditionalInfoPage AIPmapSection;
        private int AddtionalInfoSize;

		private double previousWidth;
		private double previousHeight;

		public MainWindow()
		{
			InitializeComponent();
			teamsSection = new TeamsSectionPage(this);
			mapSection = new MapSectionPage(this);
			interventionsSection = new InterventionSectionPage(this);
            AIPmapSection = new AdditionalInfoPage(this);

			previousWidth = MapSection.ActualWidth;
			previousHeight = MapSection.ActualHeight;

			//Populating the Teams section
			Frame teamsFrame = new Frame();
			teamsFrame.Content = teamsSection;
			TeamsSection.Child = teamsFrame;

			//Populating the Map section
			Frame mapFrame = new Frame();
			mapFrame.Content = mapSection;
			MapSection.Child = mapFrame;

			//Populating the Interventions section
			Frame interventionsFrame = new Frame();
			interventionsFrame.Content = interventionsSection;
			InterventionsSection.Child = interventionsFrame;


            //Populating the AI section
            Frame AIFrame = new Frame();
            AIFrame.Content = AIPmapSection;
            AIPSection.Child = AIFrame;
		}

		//Window size or state changed - Adjusting the team section height
		public void setSectionsHeight(object sender, EventArgs e)
		{
			teamsSection.setTeamsSectionHeight(TeamsSection);
			interventionsSection.setInterventionsSectionWidth(InterventionsSection);

			mapSection.movePins((MapSection.ActualWidth / previousWidth), (MapSection.ActualHeight / previousHeight));

			previousWidth = MapSection.ActualWidth;
			previousHeight = MapSection.ActualHeight;
		}

		//Click: Load Map
		private void LoadMap(object sender, RoutedEventArgs e)
		{
			//Initiating and displaying of the dialog
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image Files (*.bmp, *.jpg, *.png, *.gif)|*.bmp;*.jpg; *.png; *.gif";
			openFileDialog.FilterIndex = 1;

			//Getting the selected image
			BitmapImage coloredImage = null;
			if (openFileDialog.ShowDialog() == true)
			{
				System.IO.FileInfo File = new System.IO.FileInfo(openFileDialog.FileName);
				coloredImage = new BitmapImage(new Uri(openFileDialog.FileName));

				mapSection.SetMap(coloredImage);
			}
		}

		//Called after successful submission and processing of the team form
		public void CreateTeamPin(Team team)
		{
			mapSection.CreateTeamPin(team);
		}

		//Click: Create any type equipement
		private void CreateEquipmentPin(object sender, RoutedEventArgs e)
		{
			ComboBoxItem selectedItem = (ComboBoxItem)EquipmentAdd.SelectedItem;
			if (selectedItem != null)
			{
				mapSection.CreateEquipmentPin("" + selectedItem.Name);
			}
			else
			{
				MessageBox.Show("You need to select an equipment to add!");
			}
		}

		//Recreating equipment after removal from the team
		public void CreateEquipmentPin(String equipmentName)
		{
			mapSection.CreateEquipmentPin(equipmentName);
		}

		//Creating intervention pin
		public void CreateInterventionPin(int interventionNumber)
		{
			mapSection.CreateInterventionPin(interventionNumber);
		}

		//Deleting pin using its name (e.g. when a team is deleted)
		public void DeletePin(String pinName)
		{
			mapSection.DeletePin(pinName);
		}


		//Add equipment to team
		public void AddTeamEquipment(Equipment equip, String teamName)
		{
			teamsSection.AddTeamEquipment(equip, teamName);
		}

		private void ChangeDeadlines(object sender, RoutedEventArgs e)
		{
			bool success = true;
			int interventionDeadline = 0;
			int movingDeadline = 0;
			try
			{
				interventionDeadline = Int32.Parse(InterventionDeadline.Text);
				movingDeadline = Int32.Parse(MovingDeadline.Text);
				if (interventionDeadline < 0 || movingDeadline < 0)
				{
					success = false;
				}
			}
			catch (Exception ex)
			{
				success = false;
			}

			if(success == false)
			{
				MessageBox.Show("The intervention deadlines should be numbers!");
			}
			else
			{
				InterventionSectionPage.setInterventionDeadline(interventionDeadline);
				InterventionSectionPage.setMovingDeadline(movingDeadline);
				MessageBox.Show("The deadlines have been changed.");
			}
		}
        public void CreateAdditionnalInfoPin(object sender, RoutedEventArgs e)
        {
            
            ComboBoxItem selectedItem = (ComboBoxItem)AI.SelectedItem;
            if (selectedItem != null)
            {
                AIPmapSection.CreateAdditionnalInfoPin("" + selectedItem.Name, AddtionalInfoSize);
            }
            else
            {
                MessageBox.Show("Please select a shape to add.");
            }
        }

        public void CreateAdditionnalInfoPin(String AI,int size)
        {
            AIPmapSection.CreateAdditionnalInfoPin(AI,size);
        }

        public void AIDeletePin(object sender, RoutedEventArgs e)
        {
            AIPmapSection.AIDeletePin(sender, e);
        }

		private void ModeChange(object sender, SelectionChangedEventArgs e)
		{

		}

		internal void AddResource(String teamName, String interventionName)
		{
			interventionsSection.AddResource(teamName, interventionName);
		}

		internal void ReportArrival(String teamName, String interventionName)
		{
			interventionsSection.ReportArrival(teamName, interventionName);
		}

        internal void ReportArrived(string interventionName, int rowNumber)
        {
            mapSection.ReportArrived(interventionName, rowNumber);
        }

        internal void CreateIntervention()
        {
            interventionsSection.CreateIntervention();
        }

		private void TestConnectionToServer(object sender, RoutedEventArgs e)
		{
			Task<String[]> PingTask = new Task<string[]>(NetworkServices.PingServer);
			PingTask.ContinueWith(ping => PingAnalyzeAnswer(ping.Result));
			PingTask.Start();
			TestConnectionButton.Background = new SolidColorBrush(Colors.Yellow);
			TestConnectionButton.Foreground = new SolidColorBrush(Colors.Black);
		}

		private void PingAnalyzeAnswer(String[] reply)
		{
			if(reply == null)
			{
				NotifyConnectionFail();
			}
			else
			{
				NotifyConnectionSuccess();
			}
		}

		private void NotifyConnectionFail()
		{
			Dispatcher.Invoke(() => 
			{
				TestConnectionButton.Background = new SolidColorBrush(Colors.Red);
				TestConnectionButton.Foreground = new SolidColorBrush(Colors.White);
			});
		}

		private void NotifyConnectionSuccess()
		{
			Dispatcher.Invoke(() => 
			{
				TestConnectionButton.Background = new SolidColorBrush(Colors.Green);
				TestConnectionButton.Foreground = new SolidColorBrush(Colors.White);

			});
		}
	}
}
