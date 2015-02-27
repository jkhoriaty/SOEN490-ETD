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
using ETD.Models.ArchitecturalObjects;


namespace ETD.ViewsPresenters.TeamsSection
{
	/// <summary>
	/// Interaction logic for TeamsSectionPage.xaml
	/// </summary>
	public partial class TeamsSectionPage : Page, Observer
	{
		private MainWindow mainWindow;
		private static Dictionary<String, StackPanel> teamEquipmentStacks = new Dictionary<String, StackPanel>();
		private DispatcherTimer dispatcherTimer = new DispatcherTimer();

		private Frame createTeamForm;

		public TeamsSectionPage(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
			dispatcherTimer.Tick += new EventHandler(refresh);
			dispatcherTimer.Interval = new TimeSpan(0, 0, 10); //Update every minute
			dispatcherTimer.Start();

			Team.RegisterObserver(this);
		}

		private void refresh(object sender, EventArgs e)
		{
			foreach(Team team in Team.getTeamList())
			{
				foreach(TeamMember teamMember in team.getMemberList())
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
		private void DisplayCreateTeamForm_Click(object sender, RoutedEventArgs e)
		{
			createTeamForm = new Frame();
			createTeamForm.Content = new TeamFormPage(this);
			StackPanel_teamList.Children.Add(createTeamForm);
			Button_CreateTeam.IsEnabled = false;
		}

		//Hiding form after submit or cancel
		public void HideCreateTeamForm()
		{
			StackPanel_teamList.Children.Remove(createTeamForm);
			Button_CreateTeam.IsEnabled = true;
			createTeamForm = null;
		}

		public void Update()
		{
			StackPanel_teamList.Children.Clear();
			foreach(Team team in Team.getTeamList())
			{
				Frame frame = new Frame();
				frame.Content = new TeamInfoPage(this, team);
				StackPanel_teamList.Children.Add(frame);
			}

			//Restoring the form if a NotifyAll was called when the form was open
			if(createTeamForm != null)
			{
				DisplayCreateTeamForm_Click(new object(), new RoutedEventArgs());
			}
		}

		//Registering the team equipment StackPanel to be able to add equipment to each team
		public void registerStackPanel(String teamName, StackPanel equipmentStack)
		{
			//teamEquipmentStacks.Add(teamName, equipmentStack);
		}

		//Adding equipment to specified team equipment stack
		public void AddTeamEquipment(Equipment equip, String teamName)
		{
			//Limit of 3 pieces of equipment per team
			if (teamEquipmentStacks[teamName].Children.Count <= 3)
			{
				Rectangle imageRectangle = new Rectangle();
				//imageRectangle.Name = equip.getEquipmentName().ToString();
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
				//equipmentImage.ImageSource = TechnicalServices.getImage(equip.getEquipmentName());
				imageRectangle.Fill = equipmentImage;

				//Getting the appropriate equipment StackPanel
				teamEquipmentStacks[teamName].Children.Add(imageRectangle);

				//Team.teamList[teamName].AddEquipment(equip);
			}
			else
			{
				MessageBox.Show("You cannot add more than 3 pieces of equipment to a team. The equipment is going to be readded to the map.");
				//mainWindow.CreateEquipmentPin(equip.ToString());
			}
		}

		//Right clicking on an equipment in a team description removew the equipment from the stack and adds it back to the map
		public void RemoveTeamEquipment(object sender, RoutedEventArgs e)
		{
			Rectangle equipment = (Rectangle)sender;
            //Type equipType = Type.GetType(equipment.Name.ToString());
            //Equipment equip = new Equipment((Equipments)Enum.Parse(typeof(Equipments), equipment.Name.ToString()));
			StackPanel equipmentStackPanel = (StackPanel)equipment.Parent;
			//Team.teamList["" + equipment.Tag].RemoveEquipment(equip);
			equipmentStackPanel.Children.Remove(equipment);          
			//mainWindow.CreateEquipmentPin(equipment.Name);

		}
       
        internal void frameKeyDown(object sender, KeyEventArgs e)
        {
            UIElement frame = e.Source as UIElement;

            if (e.Key == Key.Down)
            {
                frameMoveDown(frame);
                e.Handled = true;
            }

            else if (e.Key == Key.Up)
            {
                frameMoveUp(frame);
                e.Handled = true;
            }
        }

        internal void frameSelection(object sender, MouseEventArgs e)
        {
            UIElement frame = e.Source as UIElement;
			StackPanel_teamList.KeyDown += new KeyEventHandler(frameKeyDown); 
            if (!frame.Focus())
            {
                frame.Focus();
                e.Handled = true;
            }
            e.Handled = true;
        }

        internal void frameMoveDown(UIElement element)
        {
            int elementIndex = 0;
			int count = StackPanel_teamList.Children.Count; //get number of elements in stackpanel
            int index;
			if (StackPanel_teamList.Children.Contains(element))
            {
				index = StackPanel_teamList.Children.IndexOf(element);
                if (index < count-1)
                elementIndex = index+1;
				StackPanel_teamList.Children.Remove(element);
				StackPanel_teamList.Children.Insert(elementIndex, element);
            }
 
        }

        internal void frameMoveUp(UIElement element)
        {
            int elementIndex = 0;
			int count = StackPanel_teamList.Children.Count; //get number of elements in stackpanel
            int index;
			if (StackPanel_teamList.Children.Contains(element))
            {
				index = StackPanel_teamList.Children.IndexOf(element);
                if (index > 0)
                    elementIndex = index - 1;
                else if (index == 0)
                    elementIndex = count - 1;
				StackPanel_teamList.Children.Remove(element);
				StackPanel_teamList.Children.Insert(elementIndex, element);
            }
        }       
	}
}


