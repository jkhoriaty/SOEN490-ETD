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
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		//Resizing of the team display section along with the window
		private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			MainWindowUpdate.setTeamsHeight(this);
		}

		//Maximization and minimization do not register as size changes, so resizing the team display section as well
		private void Window_StateChanged(object sender, EventArgs e)
		{
			MainWindowUpdate.setTeamsHeight(this);
		}

		//Clicking on the add team button
		private void CreateTeam(object sender, RoutedEventArgs e)
		{
			MainWindowUpdate.DisplayTeam(this);
		}
        private void SetTimer(object sender, RoutedEventArgs e)
        {
            Timer.TestTimer(this);
        }
		//
		// Getters for the view to get the reference to needed controls
		//
		public ScrollViewer getScroller()
		{
			return Scroller;
		}

		public StackPanel getTeamList()
		{
			return TeamList;
		}

		public Border getTeamSection()
		{
			return TeamSection;
		}

        public TextBlock getTimer()
        {
            return timer;
        }

	}
}
