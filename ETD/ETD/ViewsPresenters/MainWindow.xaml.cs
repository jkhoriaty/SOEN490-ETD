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
using ETD.Models;

namespace ETD.ViewsPresenters
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
    public partial class MainWindow
	{
		private TeamsSectionPage teamsSection;
		private MapSectionPage mapSection;

		public MainWindow()
		{
			InitializeComponent();

			teamsSection = new TeamsSectionPage(this);
			mapSection = new MapSectionPage(this);

			//Populating the Team section
			Frame teamsFrame = new Frame();
			teamsFrame.Content = teamsSection;
			TeamSection.Child = teamsFrame;

			//Populating the Map section
			Frame mapFrame = new Frame();
			mapFrame.Content = mapSection;
			MapSection.Child = mapFrame;
		}

		//Window size or state changed - Adjusting the team section height
		public void setTeamsSectionHeight(object sender, EventArgs e)
		{
			TeamSection.Height = MapSection.ActualHeight + 20;
			teamsSection.setTeamsSectionHeight(TeamSection);
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
			Button bt = (Button)sender;
			mapSection.CreateEquipmentPin(bt.Name);
		}

		//Recreating equipment after removal from the team
		public void CreateEquipmentPin(String equipmentName)
		{
			mapSection.CreateEquipmentPin(equipmentName);
		}

		//Deleting pin using its name (e.g. when a team is deleted)
		public void DeletePin(String pinName)
		{
			mapSection.DeletePin(pinName);
		}

		//Add equipment to team
		public void AddTeamEquipment(String equip, String teamName)
		{
			teamsSection.AddTeamEquipment(equip, teamName);
		}
	}
}
