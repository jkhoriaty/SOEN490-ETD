using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;

namespace ETD
{
    class TeamFormUpdate
    {
        private TeamForm caller;
        private int currentNumberOfMembers = 1; //Used to track the number of members on the TeamForm

        public void addMember(TeamForm caller)
        {
            this.caller = caller;
            switch (currentNumberOfMembers)
            {
                case 1:
                    caller.RemoveMember.IsEnabled = true;
                    break;
                case 2:
                    break;
            }
            MessageBox.Show("Add Member Pressed");
        }

        public void removeMember(TeamForm caller)
        {
            this.caller = caller;
            MessageBox.Show("Remove Member Pressed");
        }

        public void submit(TeamForm caller)
        {
            this.caller = caller;
            MessageBox.Show("Submit Pressed");
        }

        public void cancel(TeamForm caller)
        {
            this.caller = caller;
            MessageBox.Show("Cancel Pressed");
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