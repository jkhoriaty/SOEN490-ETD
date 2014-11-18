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

		//Clicking on the add team button
		private void CreateTeam(object sender, RoutedEventArgs e)
		{
			updater.DisplayCreateTeamForm();
		}
		private void SetTime(object sender, RoutedEventArgs e)
		{
            Timer.TestTimer(this);
        }

        private void LoadMap(object sender, RoutedEventArgs e)
        {
            MainWindowUpdate.LoadMap(this);
        }

		//Hiding form after submit or cancel
		public void hideTeamForm()
		{
			updater.HideCreateTeamForm();
		}

        private void SetTimer(object sender, RoutedEventArgs e)
        {
            Timer.TestTimer(this);
        }

		public TextBlock getTimer()
		{
			return timer;
		}


		/* Uncomment for the view to pull the Team through the controller
		 * private Team getTeam(int index)
		 * {
		 * Globals.getTeam(index);
		}*/
	}
}
