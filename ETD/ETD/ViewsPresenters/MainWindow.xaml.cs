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


namespace ETD.ViewsPresenters
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
    public partial class MainWindow
	{
        //Page variables 
		private TeamsSectionPage teamsSection;
		private MapSectionPage mapSection;
		private InterventionSectionPage interventionsSection;
        private AdditionalInfoPage mapModificationSection;

        //Forms used by the popup method
        private FollowUpSectionForm followupSection;
        private ShiftsSection shiftSection;
        private FormPopup followupSectionFormPopupContainer;
        private FormPopup shiftSectionPopupContainer;

        //Variables used when resizing the window
		private double previousWidth;
		private double previousHeight;

		DispatcherTimer dispatcherTimer = new DispatcherTimer();
		private Dictionary<String, String> registeredVolunteers = new Dictionary<String, String>();

		public MainWindow()
		{
            //hook up DataChanged event to get notification to make culture-related changes in code
            CultureResources.ResourceProvider.DataChanged += new EventHandler(ResourceProvider_DataChanged);

            //initialise with default culture
            Debug.WriteLine(string.Format("Set culture to default [{0}]:", Properties.Settings.Default.DefaultCulture));
            CultureResources.ChangeCulture(Properties.Settings.Default.DefaultCulture.NativeName);

			InitializeComponent();

            //only attach SelectionChanged event here to avoid the culture being updated twice
            this.ComboBox_Languages.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_Languages_SelectionChanged);
            ComboBox_Languages.SelectedItem = Properties.Settings.Default.DefaultCulture;

            FormPopup.RegisterMainWindow(this);//Register main window as the master window, used for displaying popups
            followupSection = new FollowUpSectionForm();
            shiftSection = new ShiftsSection();
            mapModificationSection = new AdditionalInfoPage(this);
			teamsSection = new TeamsSectionPage(this);
			mapSection = new MapSectionPage(this);
			interventionsSection = new InterventionSectionPage(this);

			previousWidth = MapSection.ActualWidth;
			previousHeight = MapSection.ActualHeight;

            //Populating the Map modification section
            Frame AIFrame = new Frame();
            AIFrame.Content = mapModificationSection;
            AIPSection.Child = AIFrame;
            

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

            //Start the transmission of the dispatcher's gps position
			dispatcherTimer.Tick += new EventHandler(RefreshGPSPositions);
			dispatcherTimer.Interval += new TimeSpan(0, 0, 5);
			dispatcherTimer.Start();
		}

		//Ping server to test connection and update registed volunteers - Executes every 5 seconds
		public void RefreshGPSPositions(object sender, EventArgs e)
		{
			//UpdateRegistered();
		}

        //window closed
        public void WindowClosed(object sender, System.EventArgs e)
        {
            TechnicalServices.saveMap(mapModificationSection);
        }
         
		//Window size or state changed - Adjusting the team section height
		public void setSectionsHeight(object sender, EventArgs e)
		{
			teamsSection.setTeamsSectionHeight(TeamsSection);
			interventionsSection.setInterventionsSectionWidth(InterventionsSection);

			Pin.MoveAllPins((MapSection.ActualWidth / previousWidth), (MapSection.ActualHeight / previousHeight));

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

                mapModificationSection.setMap(coloredImage);
			}
		}

		//Click: Create any type equipement
		private void CreateEquipmentPin(object sender, RoutedEventArgs e)
		{
			ComboBoxItem selectedItem = (ComboBoxItem)EquipmentAdd.SelectedItem;
			if (selectedItem != null)
			{
				new Equipment("" + selectedItem.Name);
			}
			else
			{
				MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_NoEquipment);
			}
		}


        //Change intervention deadlines
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
				MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_DeadlineNumbers);
			}
			else
			{
				InterventionSectionPage.setInterventionDeadline(interventionDeadline);
				InterventionSectionPage.setMovingDeadline(movingDeadline);
				MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_DeadlineChanged);
			}
		}

        //Create additional shapes on the map
        public void CreateMapModificationPin(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = sender as ComboBoxItem;
            ComboBox parent = item.Parent as ComboBox;
            foreach (ComboBoxItem mi in parent.Items)
            {
                if (mi!=null && mi.IsSelected )
                {
                    mapModificationSection.CreateMapModificationPin("" + mi.Name);
                }
            }  
        }

     
        //Switch between Regular mode and Edit mode
        private void ModeChange(object sender, RoutedEventArgs e)
		{
			ComboBoxItem item = sender as ComboBoxItem;
			ComboBox parent = modeCB;
            foreach (ComboBoxItem mi in parent.Items)
            {
                if (mi.Content.Equals(ETD.Properties.Resources.ComboBoxItem_RegularMode) && (mi.IsSelected))//lock the map modification section
                {
                    AI.Visibility = Visibility.Collapsed;
                    mapModificationSection.IsEnabled = false;
                    mapSection.IsEnabled = true;
                }
                else if (mi.Content.Equals(ETD.Properties.Resources.ComboBoxItem_EditMode) && (mi.IsSelected))//lock the map section
                {
                    AI.Visibility = Visibility.Visible;
                    mapModificationSection.IsEnabled = true;
                    mapSection.IsEnabled = false;
                }
            }  
		}

        //Recovering the fields default text if left empty
		public void PopupLostFocus(object sender, EventArgs e)
		{
			Popup popup = (Popup)sender;
			popup.IsOpen = false;
		}

        //Display GPS position
		private void ShowGPSLocations(object sender, RoutedEventArgs e)
		{
			//UpdateRegistered().Wait();
			Dispatcher.Invoke(() =>
			{
				
			});
			newRegisteredCTR.Content = "0";
		}

		//Pinging server for registered volunteers and interpret the return
		private Task UpdateRegistered()
		{
			Task<String[]> UpdateRegisteredTask = new Task<string[]>(NetworkServices.UpdateRegisted);
			UpdateRegisteredTask.ContinueWith(task => UpdateRegisteredResultAnalysis(task.Result));
			UpdateRegisteredTask.Start();
			return UpdateRegisteredTask;
		}

        //Displays follow up section page
        private void ShowFollowUpSection(object sender, RoutedEventArgs e)
        {
            followupSectionFormPopupContainer = new FormPopup(followupSection);
        }

        //Displays Shifts section page
        private void ShowShiftsSection(object sender, RoutedEventArgs e)
        {
            shiftSectionPopupContainer = new FormPopup(shiftSection);
        }

		//Interpret the servers return
		private void UpdateRegisteredResultAnalysis(String[] reply)
		{
			if(reply == null)
			{
				NotifyConnectionFail();
			}
			else if(reply.Length > 0)
			{
				NotifyConnectionSuccess();
				int newCtr = 0;
				for (int i = 1; i < (reply.Length - 1); i++)
				{
					String[] volunteerInfo = reply[i].Split('|');
					if (!registeredVolunteers.ContainsKey(volunteerInfo[0]))
					{
						registeredVolunteers.Add(volunteerInfo[0], volunteerInfo[1]);
						newCtr++;
					}
					else
					{
						registeredVolunteers[volunteerInfo[0]] = volunteerInfo[1];
					}
				}
				Dispatcher.Invoke(() => newRegisteredCTR.Content = "" + newCtr);
			}
		}

		//UI work to notify of connection success
		private void NotifyConnectionSuccess()
		{
			Dispatcher.Invoke(() => 
			{
				GPSLocationsTextBlock.Background = new SolidColorBrush(Colors.Green);
				GPSLocationsTextBlock.Foreground = new SolidColorBrush(Colors.White);
				GPSLocationsTextBlock.IsEnabled = true;
			});
		}

		//UI work to notify of connection failure
		private void NotifyConnectionFail()
		{
			Dispatcher.Invoke(() => 
			{
				GPSLocationsTextBlock.Background = new SolidColorBrush(Colors.Red);
				GPSLocationsTextBlock.Foreground = new SolidColorBrush(Colors.White);
				GPSLocationsTextBlock.IsEnabled = false;
			});
		}

        ////Code written by: Andrew Wood
        //Retrieved from: http://www.codeproject.com/Articles/22967/WPF-Runtime-Localization
        //Used under  Code Project Open License (CPOL) license.
        void ResourceProvider_DataChanged(object sender, EventArgs e)
        {
            //Debug.WriteLine(string.Format("ObjectDataProvider.DataChanged event. fetching culturename property for new culture [{0}]", Properties.Resources.LabelCultureName));
        }

        ////Code written by: Andrew Wood
        //Retrieved from: http://www.codeproject.com/Articles/22967/WPF-Runtime-Localization
        //Used under  Code Project Open License (CPOL) license.
        private void ComboBox_Languages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String selected_NativeName = ComboBox_Languages.SelectedItem as String;
            //CultureInfo selected_culture = ComboBox_Languages.SelectedItem as CultureInfo;

            //if not current language
            //could check here whether the culture we want to change to is available in order to provide feedback / action
            if (Properties.Resources.Culture != null && !Properties.Resources.Culture.NativeName.Equals(selected_NativeName))
            {
                Debug.WriteLine(string.Format("Change Current Culture to [{0}]", selected_NativeName));

                //save language in settings
                //Properties.Settings.Default.CultureDefault = selected_culture;
                //Properties.Settings.Default.Save();

                //change resources to new culture
                CultureResources.ChangeCulture(selected_NativeName);

                //could apply a theme tied to this culture if desired
            }
        }
    }

}
