using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Media;

namespace ETD
{
    class TeamFormUpdate : IObserver
    {
        private TeamForm caller;
        public int currentNumberOfMembers = 1; //Used to track the number of members on the TeamForm
		private Dictionary<String, String> textboxContent = new Dictionary<string,string>();
		private List<Control> textboxLastValidationFailed = null;
		private List<Border> comboboxLastValidationFailed = null;

		public TeamFormUpdate(TeamForm caller)
		{
			this.caller = caller;
            LanguageSelector.attach(this);
		}

        public void addMember()
        {
            switch (currentNumberOfMembers)
            {
                case 1:
					caller.member2.Visibility = Visibility.Visible;
                    caller.RemoveMember.IsEnabled = true;
                    break;
                case 2:
					caller.member3.Visibility = Visibility.Visible;
					caller.AddMember.IsEnabled = false;
                    break;
            }
			currentNumberOfMembers++;
        }

        public void removeMember()
        {
			switch (currentNumberOfMembers)
			{
				case 2:
					caller.member2.Visibility = Visibility.Collapsed;
					caller.RemoveMember.IsEnabled = false;
					break;
				case 3:
					caller.member3.Visibility = Visibility.Collapsed;
					caller.AddMember.IsEnabled = true;
					break;
			}
			currentNumberOfMembers--;
        }

        public void submit()
        {
            MessageBox.Show("Submit Pressed");
        }

        public void cancel()
        {
            MessageBox.Show("Cancel Pressed");
        }

		//Clearing TextBoxes 
		public void clearText(TextBox tb)
		{
			if (!textboxContent.ContainsKey(tb.Name))
			{
				textboxContent.Add(tb.Name, tb.Text);
			}
			tb.Text = "";
		}

		//Restoring TextBoxes text if left empty
		public void restoreText(TextBox tb)
		{
			tb.Text = textboxContent[tb.Name];
			textboxContent.Remove(tb.Name);
		}

		//Redenning the borders of the controls that failed validation
		public bool reportValidationFail(List<Control> failedValidation)
		{
			//Resetting border values to default
			if(textboxLastValidationFailed != null)
			{
				foreach(Control ctl in textboxLastValidationFailed)
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
		public bool reportValidationFail(List<Border> failedValidation)
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

        // Updates all text fields when a language change is observed
        public void update()
        {
            //Example
            //control.text = LanguageSelector.getString(control.name)
        }
	}
}





/********************* OLD CODE FROM MAINWINDOW: MAY BE USEFUL ***********/
		/*	System.Windows.Controls.StackPanel mainStackPanel = new System.Windows.Controls.StackPanel();
                Border mainBorder = createBorder();
                TextBox teamName = createTeamNameBox("teamName");
                TextBox teamMember = createTeamMemberBox("teamMember1");
					

					

					

					//System.Windows.Controls.Border line2 = createLine();
					
					System.Windows.Controls.Grid timeGrid = new System.Windows.Controls.Grid();

						System.Windows.Controls.Label timeText = new System.Windows.Controls.Label();
						timeText.Content = "Time of departure:";
						timeText.HorizontalAlignment = HorizontalAlignment.Left;


                        System.Windows.Controls.TextBox dephh = new System.Windows.Controls.TextBox();
                        dephh.Name = "departurehh";
                        dephh.Text = "hh";
                        dephh.Width = 30;
                        Thickness margin = dephh.Margin;
                        margin.Left = 30;
                        dephh.Margin = margin;

                        System.Windows.Controls.TextBox depmm = new System.Windows.Controls.TextBox();
                        depmm.Name = "departuremm";
                        depmm.Text = "mm";
                        depmm.Width = 30;
                        Thickness marginmm = depmm.Margin;
                        marginmm.Left = 100;
                        depmm.Margin = marginmm;


                        ComboBox lvlOfTraining = new ComboBox();
                        lvlOfTraining.Name = "levelOfTraining";
                        ComboBoxItem gFAid = new ComboBoxItem();
                        gFAid.Content = "General First Aid";
                        ComboBoxItem fResp = new ComboBoxItem();
                        fResp.Content = "First Responder";
                        ComboBoxItem med = new ComboBoxItem();
                        med.Content = "Medicine";
                        lvlOfTraining.Items.Add(gFAid);
                        lvlOfTraining.Items.Add(fResp);
                        lvlOfTraining.Items.Add(med);
                        

			//
			// Linking items together to form appropriate hierarchy
			//
			TeamList.Children.Add(mainBorder);
				mainBorder.Child = mainStackPanel;
					mainStackPanel.Children.Add(teamName);
					mainStackPanel.Children.Add(createLine());
					mainStackPanel.Children.Add(teamMember);
					mainStackPanel.Children.Add(createLine());
					mainStackPanel.Children.Add(timeGrid);
						timeGrid.Children.Add(timeText);
                        timeGrid.Children.Add(dephh);
                        timeGrid.Children.Add(depmm);
                        mainStackPanel.Children.Add(createLine());
                    mainStackPanel.Children.Add(lvlOfTraining);
             */


/*
   public Border createLine()
   {
       System.Windows.Controls.Border line = new System.Windows.Controls.Border();
       line.BorderBrush = new SolidColorBrush(Colors.Black); ;
       line.BorderThickness = new System.Windows.Thickness(1);
       Thickness topMargin = line.Margin;
       topMargin.Top = 2;
       topMargin.Bottom = 2;
       line.Margin = topMargin;
       return line;
   }
   public Border createBorder()
   {
       System.Windows.Controls.Border mainBorder = new System.Windows.Controls.Border();
       mainBorder.Name = "newTeam";
       mainBorder.BorderBrush = new SolidColorBrush(Colors.Black); ;
       mainBorder.BorderThickness = new System.Windows.Thickness(1);
       mainBorder.CornerRadius = new System.Windows.CornerRadius(5);
       Thickness topMargin = mainBorder.Margin;
       topMargin.Top = 5;
       mainBorder.Margin = topMargin;
       return mainBorder;
   }
   public TextBox createTeamNameBox(String name)
   {
       TextBox teamName = new TextBox();
       teamName.Name = name;
       teamName.Text = "Team Name";
       teamName.FontWeight = FontWeights.Bold;
       teamName.FontSize = 20;
       return teamName;
   }
   public TextBox createTeamMemberBox(String name)
   {
       //TODO: Move to add team member and have this method call the add teamMember method
       System.Windows.Controls.TextBox teamMember = new System.Windows.Controls.TextBox();
       teamMember.Name = name;
       teamMember.Text = "Team Member Name";
       teamMember.FontSize = 18;
       return teamMember;
   }

*/