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
		MainWindowUpdate updater;
		String AbsolutePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

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

		//Hiding form after submit or cancel
		public void hideTeamForm()
		{
			updater.HideCreateTeamForm();
		}

        private void SetTimer(object sender, RoutedEventArgs e)
        {
            updater.TimeTest_MenuItem_Click();
        }

		//Displaying the team
		public void DisplayTeam(Team team)
		{
			updater.HideCreateTeamForm();
			updater.DisplayTeam(team);
		}

		//Getting picture from the path
		public BitmapImage getImage(String relativePath)
		{
			BitmapImage img = new BitmapImage(new Uri(AbsolutePath + relativePath));
			return img;
		}

		//Getting the full name of the letter
		public String getLetter(String letter)
		{
			return PhoneticAlphabet.getLetter(letter);
		}
	}
}
