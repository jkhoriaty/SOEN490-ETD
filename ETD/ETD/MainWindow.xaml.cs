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
		private MainWindowUpdate updater;
		private bool _isRectDragInProg;
		private String movingRectangle;

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


		private void team_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Rectangle r = (Rectangle) sender;

			_isRectDragInProg = r.CaptureMouse();
			movingRectangle = r.Name;
		}

		private void team_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			Rectangle r = (Rectangle) sender;

			//Avoid in having method called on object being collided with
			if (!r.Name.Equals(movingRectangle))
			{
				return;
			}

			r.ReleaseMouseCapture();
			_isRectDragInProg = false;

			var mousePos = e.GetPosition(Map);
			double horizontalDropped = mousePos.X;
			double verticalDropped = mousePos.Y;

			//Calling collision detection and resolution for the dropped object
			updater.collisionDetection(r, horizontalDropped, verticalDropped);
		}

		//Method to visually drag the item selected and insuring it doesn't go outside of the map
		private void team_MouseMove(object sender, MouseEventArgs e)
		{
			//If no rectangle are clicked, exit method
			if (!_isRectDragInProg) return;

			Rectangle r = (Rectangle) sender;

			//Handling behaviour where fixed rectangle gets moved when another rectangle is dropped on it
			if (!r.Name.Equals(movingRectangle))
			{
				return;
			}
			
			//Get the position of the mouse relative to the Canvas
			var mousePos = e.GetPosition(Map);

			//Making sure it is not dragged out of bounds
			//Getting shapeRadius from updater to only have one centralized copy of this
			if (mousePos.X > (Map.ActualWidth - updater.shapeRadius) || mousePos.Y > (Map.ActualHeight - updater.shapeRadius) || mousePos.X < updater.shapeRadius || mousePos.Y < updater.shapeRadius)
			{
				return;
			}

			updater.setPosition(r, mousePos.X, mousePos.Y);
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
			updater.DisplayTeamInfo(team);
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
