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
using System.IO;
using System.Xml;
using ETD.ViewsPresenters;
using ETD.Models.Objects;
using ETD.Services;
using System.Data.SQLite;
using ETD.Services.Database;

namespace ETD.ViewsPresenters.TeamsSection.TeamForm
{
    /// <summary>
    /// Interaction logic for TeamForm.xaml
    /// </summary>
    
    public partial class TeamFormPage : Page
    {
		TeamsSectionPage caller;
		public int currentNumberOfMembers = 1; //Used to track the number of members on the TeamForm
		private List<Control> textboxLastValidationFailed = null;
		private List<Border> comboboxLastValidationFailed = null;

        public TeamFormPage(TeamsSectionPage caller)
        {
            InitializeComponent();
			this.caller = caller;

			ComboBoxItem createUserItem = new ComboBoxItem();
			createUserItem.Content = "NEW USER";
			createUserItem.FontStyle = FontStyles.Italic;
			createUserItem.FontWeight = FontWeights.Bold;

			ComboBoxItem createUserItem2 = new ComboBoxItem();
			createUserItem2.Content = "NEW USER";
			createUserItem2.FontStyle = FontStyles.Italic;
			createUserItem2.FontWeight = FontWeights.Bold;

			ComboBoxItem createUserItem3 = new ComboBoxItem();
			createUserItem3.Content = "NEW USER";
			createUserItem3.FontStyle = FontStyles.Italic;
			createUserItem3.FontWeight = FontWeights.Bold;

			ComboBox_TeamMemberName1.Items.Add(createUserItem);
			ComboBox_TeamMemberName2.Items.Add(createUserItem2);
			ComboBox_TeamMemberName3.Items.Add(createUserItem3);

			SQLiteDataReader reader = StaticDBConnection.QueryDatabase("SELECT Name FROM Volunteers");
			while (reader.Read())
			{
				ComboBoxItem cbItem = new ComboBoxItem();
				cbItem.Content = reader["Name"].ToString();
				ComboBoxItem cbItem2 = new ComboBoxItem();
				cbItem2.Content = reader["Name"].ToString();
				ComboBoxItem cbItem3 = new ComboBoxItem();
				cbItem3.Content = reader["Name"].ToString();
				ComboBox_TeamMemberName1.Items.Add(cbItem);
				ComboBox_TeamMemberName2.Items.Add(cbItem2);
				ComboBox_TeamMemberName3.Items.Add(cbItem3);
			}
            reader.Dispose();
            StaticDBConnection.CloseConnection();
        }

		//Click: Add Member
		private void AddMember_Click(object sender, RoutedEventArgs e)
		{
			switch (currentNumberOfMembers)
			{
				case 1:
					member2.Visibility = Visibility.Visible;
					RemoveMember.IsEnabled = true;
					break;
				case 2:
					member3.Visibility = Visibility.Visible;
					AddMember.IsEnabled = false;
					break;
			}
			currentNumberOfMembers++;
        }

		//Click: Remove Member
        private void RemoveMember_Click(object sender, RoutedEventArgs e)
        {
			switch (currentNumberOfMembers)
			{
				case 2:
					member2.Visibility = Visibility.Collapsed;
					RemoveMember.IsEnabled = false;
					break;
				case 3:
					member3.Visibility = Visibility.Collapsed;
					AddMember.IsEnabled = true;
					break;
			}
			currentNumberOfMembers--;
        }

		//Click: Submit
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
			if(FormValidation())
			{
				//Creating team
				DateTime dateNow = DateTime.Now;

				//Create team
                String teamNameStr = teamName.Text;
				if (teamNameStr.Length == 1)
                {
					teamNameStr = teamNameStr.ToUpper();
                }
				Team team = new Team(teamNameStr);

				//Create first member
				String mem1Name = ComboBox_TeamMemberName1.Text;
				DateTime mem1Departure = CheckDepartureTime(new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, int.Parse(departurehh1.Text), int.Parse(departuremm1.Text), dateNow.Second));
				Trainings mem1LvlOfTraining = (Trainings)lvlOfTraining1.SelectedIndex;
				TeamMember mem_1 = new TeamMember(mem1Name, mem1LvlOfTraining, mem1Departure);
				team.AddMember(mem_1);

				//Create second member
				String mem2Name = ComboBox_TeamMemberName2.Text;
				if (mem2Name != "")
				{
					DateTime mem2Departure = CheckDepartureTime(new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, int.Parse(departurehh2.Text), int.Parse(departuremm2.Text), dateNow.Second));
					Trainings mem2LvlOfTraining = (Trainings) lvlOfTraining2.SelectedIndex;
					TeamMember mem2 = new TeamMember(mem2Name, mem2LvlOfTraining, mem2Departure);
					team.AddMember(mem2);
				}

				//Create third member
				String mem3Name = ComboBox_TeamMemberName3.Text;
                if (mem3Name != "")
				{
					DateTime mem3Departure = CheckDepartureTime(new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, int.Parse(departurehh3.Text), int.Parse(departuremm3.Text), dateNow.Second));
					Trainings mem3LvlOfTraining = (Trainings)lvlOfTraining3.SelectedIndex;
					TeamMember mem3 = new TeamMember(mem3Name, mem3LvlOfTraining, mem3Departure);
					team.AddMember(mem3);
				}
               
				//Displaying the team on the main window
				caller.HideCreateTeamForm();
                //caller.UpdateSectors();
			}
			else
			{
				MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_FailedValidation);
			}
        }

		//Click: Cancel
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
			caller.HideCreateTeamForm();
        }

		//Focus: Textboxes - Clearing the fields upon focus if populated by the default text
		private void TextBoxes_GotFocus(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.GotFocus(sender, e);
		}

		//LostFocus: Textboxes - Recovering the fields default text if left empty
		private void TextBoxes_LostFocus(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.LostFocus(sender, e);
		}

		//Validating all fields when form is submitted before team creation
		private bool FormValidation()
		{
			List<Control> textboxFailedValidation = new List<Control>();
			List<Border> comboboxFailedValidation = new List<Border>();

			//Team Name
			if(teamName.Text.Equals("Team Name"))
			{
				textboxFailedValidation.Add(teamName);
			}

			//Make sure that there are no other team with the same name
			if(Team.TeamListContains(teamName.Text))
			{
				textboxFailedValidation.Add(teamName);
			}
			
			//Team Member 1
			//if (teamMember1.Text.Equals("Team Member Name"))
			//{
			//	textboxFailedValidation.Add(teamMember1);
			//}

			if(TimeValidation(departurehh1.Text, true) == false)
			{
				textboxFailedValidation.Add(departurehh1);
			}
			if (TimeValidation(departuremm1.Text, false) == false)
			{
				textboxFailedValidation.Add(departuremm1);
			}

			if (lvlOfTraining1.Text.Equals(""))
			{
				comboboxFailedValidation.Add(lvlOfTraining1Border);
			}

			//Team Member 2
			if (currentNumberOfMembers >= 2)
			{
				//if (teamMember2.Text.Equals("Team Member Name"))
				//{
				//	textboxFailedValidation.Add(teamMember2);
				//}

				if (TimeValidation(departurehh2.Text, true) == false)
				{
					textboxFailedValidation.Add(departurehh2);
				}
				if (TimeValidation(departuremm2.Text, false) == false)
				{
					textboxFailedValidation.Add(departuremm2);
				}

				if (lvlOfTraining2.Text.Equals(""))
				{
					comboboxFailedValidation.Add(lvlOfTraining2Border);
				}
			}

			//Team Member 3
			if (currentNumberOfMembers >= 3)
			{
				//if (teamMember3.Text.Equals("Team Member Name"))
				//{
				//	textboxFailedValidation.Add(teamMember3);
				//}

				if (TimeValidation(departurehh3.Text, true) == false)
				{
					textboxFailedValidation.Add(departurehh3);
				}
				if (TimeValidation(departuremm3.Text, false) == false)
				{
					textboxFailedValidation.Add(departuremm3);
				}

				if (lvlOfTraining3.Text.Equals(""))
				{
					comboboxFailedValidation.Add(lvlOfTraining3Border);
				}
			}

			bool textBoxSuccess = ReportValidationFail(textboxFailedValidation);
			bool comboBoxSuccess = ReportValidationFail(comboboxFailedValidation);
			if (textBoxSuccess == false || comboBoxSuccess == false)
			{
				return false;
			}
			else
			{
				return true;
			}
			
		}

		//Validation of hours and minutes fields, hours is true when the time passed is one of the hours fields
		private bool TimeValidation(String time, bool hours)
		{
			if(hours == true && time.Equals("hh"))
			{
				return false;
			}
			else if(hours == false && time.Equals("mm"))
			{
				return false;
			}
			else
			{
				try
				{
					int num = Int32.Parse(time);
					if(hours == true && (num < 0 || num >= 24))
					{
						return false;
					}
					else if(hours == false && (num < 0 || num >= 60))
					{
						return false;
					}
				}
				catch(Exception e)
				{
					return false;
				}
			}
			return true;
		}

		//Redenning the borders of the controls that failed validation
		public bool ReportValidationFail(List<Control> failedValidation)
		{
			//Resetting border values to default
			if (textboxLastValidationFailed != null)
			{
				foreach (Control ctl in textboxLastValidationFailed)
				{
					ctl.ClearValue(Control.BorderBrushProperty);
				}
			}
			textboxLastValidationFailed = new List<Control>(failedValidation);


			//Giving a red border to all the controls that have failed validation
			if (failedValidation.Count != 0)
			{
				foreach (Control ctl in failedValidation)
				{
					ctl.BorderBrush = new SolidColorBrush(Colors.Red);
				}
				return false;
			}
			else
			{
				return true;
			}
		}

		//Redenning the borders of the controls that failed validation
		public bool ReportValidationFail(List<Border> failedValidation)
		{
			//Resetting border values to default
			if (comboboxLastValidationFailed != null)
			{
				foreach (Border bd in comboboxLastValidationFailed)
				{
					bd.BorderBrush = new SolidColorBrush(Colors.White);
				}
			}
			comboboxLastValidationFailed = new List<Border>(failedValidation);

			//Giving a red border to all the controls that have failed validation
			if (failedValidation.Count != 0)
			{
				foreach (Border bd in failedValidation)
				{
					bd.BorderBrush = new SolidColorBrush(Colors.Red);
				}
				return false;
			}
			else
			{
				return true;
			}
		}

		public DateTime CheckDepartureTime(DateTime departureTime)
		{
			if (departureTime.Hour < DateTime.Now.Hour)
			{
				departureTime = departureTime.AddDays(1);
			}
            else if (departureTime.Hour == DateTime.Now.Hour && departureTime.Minute < DateTime.Now.Minute)
            {
                departureTime = departureTime.AddDays(1);
            }
			return departureTime;
		}

		private void ComboBox_TeamMemberName1_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ComboBox_TeamMemberName1.SelectedIndex == 0)
			{
				ComboBox_TeamMemberName1.Visibility = Visibility.Collapsed;
				teamMember1.Visibility = Visibility.Visible;
				Button_OKTeamMember1.Visibility = Visibility.Visible;
				Button_CancelTeamMember1.Visibility = Visibility.Visible;
			}
			else
			{
				ComboBoxItem memberNameItem = new ComboBoxItem();
				memberNameItem = (ComboBoxItem)ComboBox_TeamMemberName1.SelectedItem;
				SQLiteDataReader reader = StaticDBConnection.QueryDatabase("Select Training_Level FROM [Volunteers] WHERE Name='" + memberNameItem.Content.ToString() + "'");
				reader.Read();

				lvlOfTraining1.SelectedIndex = reader.GetInt32(0);
                reader.Dispose();
                StaticDBConnection.CloseConnection();
			}
		}

		private void ComboBox_TeamMemberName2_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ComboBox_TeamMemberName2.SelectedIndex == 0)
			{
				ComboBox_TeamMemberName2.Visibility = Visibility.Collapsed;
				teamMember2.Visibility = Visibility.Visible;
				Button_OKTeamMember2.Visibility = Visibility.Visible;
				Button_CancelTeamMember2.Visibility = Visibility.Visible;
			}
			else
			{
				ComboBoxItem memberNameItem = new ComboBoxItem();
				memberNameItem = (ComboBoxItem)ComboBox_TeamMemberName2.SelectedItem;
				SQLiteDataReader reader = StaticDBConnection.QueryDatabase("Select Training_Level FROM [Volunteers] WHERE Name='" + memberNameItem.Content.ToString() + "'");
				reader.Read();

				lvlOfTraining2.SelectedIndex = reader.GetInt32(0);
                reader.Dispose();
                StaticDBConnection.CloseConnection();
			}
		}

		private void ComboBox_TeamMemberName3_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ComboBox_TeamMemberName3.SelectedIndex == 0)
			{
				ComboBox_TeamMemberName3.Visibility = Visibility.Collapsed;
				teamMember3.Visibility = Visibility.Visible;
				Button_OKTeamMember3.Visibility = Visibility.Visible;
				Button_CancelTeamMember3.Visibility = Visibility.Visible;
			}
			else
			{
				ComboBoxItem memberNameItem = new ComboBoxItem();
				memberNameItem = (ComboBoxItem)ComboBox_TeamMemberName3.SelectedItem;
				SQLiteDataReader reader = StaticDBConnection.QueryDatabase("Select Training_Level FROM [Volunteers] WHERE Name='" + memberNameItem.Content.ToString() + "'");
				reader.Read();

				lvlOfTraining3.SelectedIndex = reader.GetInt32(0);
                reader.Dispose();
                StaticDBConnection.CloseConnection();
			}
		}

		private void Button_CancelTeamMember1_Click(object sender, RoutedEventArgs e)
		{
			ComboBox_TeamMemberName1.Visibility = Visibility.Visible;
			teamMember1.Visibility = Visibility.Collapsed;
			Button_OKTeamMember1.Visibility = Visibility.Collapsed;
			Button_CancelTeamMember1.Visibility = Visibility.Collapsed;
		}

		private void Button_OKTeamMember1_Click(object sender, RoutedEventArgs e)
		{
			if (teamMember1.Text == "")
			{
				MessageBox.Show("Please enter a user name.");
			}
			else
			{
				//StaticDBConnection.NonQueryDatabase("INSERT INTO [Volunteers] (Name, Training_Level) VALUES ('" + teamMember1.Text + "', 0);");
				teamMember1.Visibility = Visibility.Collapsed;
				Button_OKTeamMember1.Visibility = Visibility.Collapsed;
				Button_CancelTeamMember1.Visibility = Visibility.Collapsed;
				ComboBox_TeamMemberName1.Visibility = Visibility.Visible;

				ComboBoxItem newUser = new ComboBoxItem();
				newUser.Content = teamMember1.Text;
				ComboBox_TeamMemberName1.Items.Add(newUser);
				ComboBox_TeamMemberName1.SelectedItem = newUser;
			}
		}

		private void Button_CancelTeamMember2_Click(object sender, RoutedEventArgs e)
		{
			ComboBox_TeamMemberName2.Visibility = Visibility.Visible;
			teamMember2.Visibility = Visibility.Collapsed;
			Button_OKTeamMember2.Visibility = Visibility.Collapsed;
			Button_CancelTeamMember2.Visibility = Visibility.Collapsed;
		}

		private void Button_OKTeamMember2_Click(object sender, RoutedEventArgs e)
		{
			if (teamMember2.Text == "")
			{
				MessageBox.Show("Please enter a user name.");
			}
			else
			{
				//StaticDBConnection.NonQueryDatabase("INSERT INTO [Volunteers] (Name, Training_Level) VALUES ('" + teamMember2.Text + "', 0);");
				teamMember2.Visibility = Visibility.Collapsed;
				Button_OKTeamMember2.Visibility = Visibility.Collapsed;
				Button_CancelTeamMember2.Visibility = Visibility.Collapsed;
				ComboBox_TeamMemberName2.Visibility = Visibility.Visible;

				ComboBoxItem newUser = new ComboBoxItem();
				newUser.Content = teamMember2.Text;
				ComboBox_TeamMemberName2.Items.Add(newUser);
				ComboBox_TeamMemberName2.SelectedItem = newUser;
			}
		}

		private void Button_CancelTeamMember3_Click(object sender, RoutedEventArgs e)
		{
			ComboBox_TeamMemberName1.Visibility = Visibility.Visible;
			teamMember1.Visibility = Visibility.Collapsed;
			Button_OKTeamMember1.Visibility = Visibility.Collapsed;
			Button_CancelTeamMember1.Visibility = Visibility.Collapsed;
		}

		private void Button_OKTeamMember3_Click(object sender, RoutedEventArgs e)
		{
			if (teamMember3.Text == "")
			{
				MessageBox.Show("Please enter a user name.");
			}
			else
			{
				//StaticDBConnection.NonQueryDatabase("Replace INTO [Volunteers] (Name, Training_Level) VALUES ('" + teamMember3.Text + "', 0);");
				teamMember3.Visibility = Visibility.Collapsed;
				Button_OKTeamMember3.Visibility = Visibility.Collapsed;
				Button_CancelTeamMember3.Visibility = Visibility.Collapsed;
				ComboBox_TeamMemberName3.Visibility = Visibility.Visible;

				ComboBoxItem newUser = new ComboBoxItem();
				newUser.Content = teamMember3.Text;
				ComboBox_TeamMemberName3.Items.Add(newUser);
				ComboBox_TeamMemberName3.SelectedItem = newUser;
			}
		}
    }
}
