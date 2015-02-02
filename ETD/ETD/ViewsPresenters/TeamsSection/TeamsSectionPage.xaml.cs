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

using ETD.ViewsPresenters.TeamsSection.TeamForm;
using ETD.ViewsPresenters.TeamsSection.TeamInfo;
using ETD.Models.Objects;
using ETD.Services;
using System.Windows.Threading;

namespace ETD.ViewsPresenters.TeamsSection
{
	/// <summary>
	/// Interaction logic for TeamsSectionPage.xaml
	/// </summary>
	public partial class TeamsSectionPage : Page
	{

		private MainWindow mainWindow;
		private static Dictionary<String, StackPanel> teamEquipmentStacks = new Dictionary<String, StackPanel>();
		private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private StackPanel movingPanel;
        private bool _isDragInProg;

		public TeamsSectionPage(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
            this.AllowDrop = true;
            //this.DragEnter += new DragEventHandler(dragEnter);
            //this.DragLeave += new DragEventHandler(dragDrop);
			dispatcherTimer.Tick += new EventHandler(refresh);
			dispatcherTimer.Interval = new TimeSpan(0, 0, 10); //Update every minute
			dispatcherTimer.Start();
		}

		private void refresh(object sender, EventArgs e)
		{
			foreach(KeyValuePair<String, Team> team in Team.teamsList)
			{
				foreach(TeamMember teamMember in team.Value.getMemberList())
				{

					DateTime now = DateTime.Now.AddMinutes(15);
                    DateTime current = DateTime.Now;
                    if (DateTime.Compare(current, teamMember.getDeparture()) >= 0)
                    {
                        teamMember.getNameGrid().Background = new SolidColorBrush(Colors.Red);
                    }
					else if(DateTime.Compare(now, teamMember.getDeparture()) >= 0)
					{
						teamMember.getNameGrid().Background = new SolidColorBrush(Colors.Yellow);
					}                   
				}
			}
		}

		//Adjusting the team section height
		public void setTeamsSectionHeight(Border TeamsSection)
		{
			//11 to account for padding of 2 on top and 2 on the bottom in addition to 7 in margin to the CreateTeamButtonBorder
			Scroller.MaxHeight = TeamsSection.ActualHeight - TeamsSectionLabel.ActualHeight - CreateTeamButtonBorder.ActualHeight - 11;
		}

		//Clicking on the add team button
		private void DisplayCreateTeamForm(object sender, RoutedEventArgs e)
		{
			Frame frame = new Frame();
			frame.Content = new TeamFormPage(this);
			TeamList.Children.Add(frame);
			CreateTeamButton.IsEnabled = false;
		}

		//Hiding form after submit or cancel
		public void HideCreateTeamForm()
		{
			TeamList.Children.RemoveAt(TeamList.Children.Count - 1);
			CreateTeamButton.IsEnabled = true;
		}

		//Displaying the team upon form submit
		public void DisplayTeamInfo(Team team)
		{
			HideCreateTeamForm();
			Frame frame = new Frame();
			frame.Content = new TeamInfoPage(this, team);
			TeamList.Children.Add(frame);
			mainWindow.CreateTeamPin(team);
		}

		//Registering the team equipment StackPanel to be able to add equipment to each team
		public void registerStackPanel(String teamName, StackPanel equipmentStack)
		{
			teamEquipmentStacks.Add(teamName, equipmentStack);
		}
   
		//Deleting the team upon right click on the label
		public void RemoveTeam(String teamName)
		{
			foreach (Frame item in TeamList.Children)
			{
				Page team = (Page)item.Content;
				if (team.Name.Equals(teamName))
				{
					teamEquipmentStacks.Remove(teamName);
					TeamList.Children.Remove(item);
					break;
				}
			}
			Team.teamsList.Remove(teamName);
			mainWindow.DeletePin(teamName);
            TeamFormPage.removeTeamName(teamName);
		}

		//Adding equipment to specified team equipment stack
		public void AddTeamEquipment(Equipment equip, String teamName)
		{
			//Limit of 3 pieces of equipment per team
			if (teamEquipmentStacks[teamName].Children.Count <= 3)
			{
				Rectangle imageRectangle = new Rectangle();
				imageRectangle.Name = equip.getEquipmentName().ToString();
				imageRectangle.Tag = teamName;
				imageRectangle.Width = 27;
				imageRectangle.Height = 27;
				imageRectangle.MouseRightButtonDown += new MouseButtonEventHandler(RemoveTeamEquipment);
                imageRectangle.FlowDirection = FlowDirection.LeftToRight;

				Thickness equipmentMargin = imageRectangle.Margin;
				equipmentMargin.Right = 1;
				imageRectangle.Margin = equipmentMargin;

				//Getting the background image to the rectangle
				ImageBrush equipmentImage = new ImageBrush();
				equipmentImage.ImageSource = TechnicalServices.getImage(equip.getEquipmentName());
				imageRectangle.Fill = equipmentImage;

				//Getting the appropriate equipment StackPanel
				teamEquipmentStacks[teamName].Children.Add(imageRectangle);

				Team.teamsList[teamName].addEquipment(equip);
			}
			else
			{
				MessageBox.Show("You cannot add more than 3 pieces of equipment to a team. The equipment is going to be readded to the map.");
				mainWindow.CreateEquipmentPin(equip.ToString());
			}
		}

		//Right clicking on an equipment in a team description removew the equipment from the stack and adds it back to the map
		public void RemoveTeamEquipment(object sender, RoutedEventArgs e)
		{
			Rectangle equipment = (Rectangle)sender;
            //Type equipType = Type.GetType(equipment.Name.ToString());
            Equipment equip = new Equipment((Equipments)Enum.Parse(typeof(Equipments), equipment.Name.ToString()));
			StackPanel equipmentStackPanel = (StackPanel)equipment.Parent;
			Team.teamsList["" + equipment.Tag].removeEquipment(equip);
			equipmentStackPanel.Children.Remove(equipment);
			mainWindow.CreateEquipmentPin(equipment.Name);

		}

        internal void dragStart(object sender, DragEventArgs e)
        {
            StackPanel s = (StackPanel)sender;
            _isDragInProg = s.CaptureMouse();
            movingPanel = s;

        }

        internal void dragStop(object sender, MouseButtonEventArgs e)
        {
            StackPanel s = (StackPanel)sender;
            if (s != movingPanel)
            {
                return;
            }
            s.ReleaseMouseCapture();
            _isDragInProg = false;
            //var mousePos = e.GetPosition(this.map)
        }

        internal void dragMove(object sender, MouseEventArgs e)
        {
            if (!_isDragInProg) return;
            StackPanel s = (StackPanel)sender;          
        }

	}
}
