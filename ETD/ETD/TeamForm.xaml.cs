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
    /// Interaction logic for TeamForm.xaml
    /// </summary>
    public partial class TeamForm : Page
    {
		MainWindow caller;
        TeamFormUpdate updater;

        public TeamForm(MainWindow caller)
        {
            InitializeComponent();
			this.caller = caller;
            updater = new TeamFormUpdate(this);
        }

        private void AddMember_Click(object sender, RoutedEventArgs e)
        {
            updater.addMember();
        }

        private void RemoveMember_Click(object sender, RoutedEventArgs e)
        {
            updater.removeMember();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
			if(formValidation())
			{
				MessageBox.Show("Validation Successful");
                Team team = updater.createTeam();
                //Use this team to link it to map
			}
			else
			{
				MessageBox.Show("Validation has failed. Please change the boxes with a red border.");
			}
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
			caller.hideTeamForm();
        }

		private void TextBoxes_GotFocus(object sender, RoutedEventArgs e)
		{
			TextBox tb = (TextBox) sender;
			updater.clearText(tb);
		}

		private void TextBoxes_LostFocus(object sender, RoutedEventArgs e)
		{
			TextBox tb = (TextBox)sender;
			if (tb.Text.Equals(""))
			{
				updater.restoreText(tb);
			}
		}

		private bool formValidation()
		{
			List<Control> textboxFailedValidation = new List<Control>();
			List<Border> comboboxFailedValidation = new List<Border>();

			//Team Name
			if(teamName.Text.Equals("Team Name"))
			{
				textboxFailedValidation.Add(teamName);
			}
			
			//Team Member 1
			if (teamMember1.Text.Equals("Team Member Name"))
			{
				textboxFailedValidation.Add(teamMember1);
			}

			if(timeValidation(departurehh1.Text, true) == false)
			{
				textboxFailedValidation.Add(departurehh1);
			}
			if (timeValidation(departuremm1.Text, false) == false)
			{
				textboxFailedValidation.Add(departuremm1);
			}

			if (lvlOfTraining1.Text.Equals(""))
			{
				comboboxFailedValidation.Add(lvlOfTraining1Border);
			}

			//Team Member 2
			if (updater.currentNumberOfMembers >= 2)
			{
				if (teamMember2.Text.Equals("Team Member Name"))
				{
					textboxFailedValidation.Add(teamMember2);
				}

				if (timeValidation(departurehh2.Text, true) == false)
				{
					textboxFailedValidation.Add(departurehh2);
				}
				if (timeValidation(departuremm2.Text, false) == false)
				{
					textboxFailedValidation.Add(departuremm2);
				}

				if (lvlOfTraining2.Text.Equals(""))
				{
					comboboxFailedValidation.Add(lvlOfTraining2Border);
				}
			}

			//Team Member 3
			if (updater.currentNumberOfMembers >= 3)
			{
				if (teamMember3.Text.Equals("Team Member Name"))
				{
					textboxFailedValidation.Add(teamMember3);
				}

				if (timeValidation(departurehh3.Text, true) == false)
				{
					textboxFailedValidation.Add(departurehh3);
				}
				if (timeValidation(departuremm3.Text, false) == false)
				{
					textboxFailedValidation.Add(departuremm3);
				}

				if (lvlOfTraining3.Text.Equals(""))
				{
					comboboxFailedValidation.Add(lvlOfTraining3Border);
				}
			}

			bool success = true;
			success = updater.reportValidationFail(textboxFailedValidation);
			success = updater.reportValidationFail(comboboxFailedValidation);
			return success;
		}

		private bool timeValidation(String time, bool hours)
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
    }
}
