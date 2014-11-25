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

namespace ETD
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
    public partial class MainWindow
	{
		MainWindowUpdate updater;

		public MainWindow()
		{
			InitializeComponent();
			updater = new MainWindowUpdate(this);
		}

		//---------------------------------------------------------------------------
		//Window changes handling
		//---------------------------------------------------------------------------

		//Resizing of the team display section along with the window
		private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			updater.setTeamsHeight();
		}

		//Maximization and minimization do not register as size changes, so resizing the team display section as well
		private void Window_StateChanged(object sender, EventArgs e)
		{
			updater.setTeamsHeight();
		}

		//---------------------------------------------------------------------------
		//Map related methods
		//---------------------------------------------------------------------------

		//Method to prompt the user for input and to load that image as a map
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
			}

			updater.LoadMap(coloredImage);
        }

		//---------------------------------------------------------------------------
		//Team section related methods
		//---------------------------------------------------------------------------

		//Clicking on the add team button
		private void DisplayCreateTeamForm(object sender, RoutedEventArgs e)
		{
			updater.DisplayCreateTeamForm();
		}

		//Hiding form after submit or cancel
		public void HideCreateTeamForm()
		{
			updater.HideCreateTeamForm();
		}

		//Displaying the team upon form submit
		public void DisplayTeam(Team team)
		{
			updater.HideCreateTeamForm();
			updater.DisplayTeam(team);
		}

		//TO BECOME: Method that gets called when equipment is overlapped with a team
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			//TODO: Move to controller
			/*
			Equipment one = new Equipment("ambulance cart");
			team.addEquipment(one);
			 */

			Equipment ms = new Equipment(equipments.mountedStretcher);
			String teamName = "N";
			updater.AddEquipment(ms, teamName);
		}

		//TODO: Remove, just for debugging-------------------------------------------
		private void SetTime(object sender, RoutedEventArgs e)
		{
            Timer.TestTimer(this);
        }
        private void SetTimer(object sender, RoutedEventArgs e)
        {
            Timer.TestTimer(this);
        }
		public TextBlock getTimer()
		{
			return timer;
		}
		//--------------------------------------------------------------------------
	}
}
