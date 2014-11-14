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
    class CreateTeamFormUpdate
    {
        private CreateTeamForm caller;
        private int currentNumberOfMembers = 1; //Used to track the number of members on the CreateTeamForm
        private int rowIndex = 0;

        //Adds block of team member fields to the CreateTeamForm
        public void addMember(CreateTeamForm caller)
        {
            this.caller = caller;
            switch (currentNumberOfMembers)
            {
                case 1:
                    caller.removeMember.Visibility = System.Windows.Visibility.Visible;
                    rowIndex = caller.createTeamGrid.RowDefinitions.Count - 1;
                    caller.Height += 150;

                    addMemberLabel("Team Member (First Aid Kit)");
                    addMemberNameRow("firstAidMemberName");
                    addMemberDepartureRow("fDephh", "fDepmm");
                    addLevelOfTrainingRow("fAidLOT");

                    currentNumberOfMembers++;
                    break;

                case 2:
                    rowIndex = caller.createTeamGrid.RowDefinitions.Count - 1;
                    caller.Height += 150;

                    addMemberLabel("Team Member (Second First Aid Kit)");
                    addMemberNameRow("sfirstAidMemberName");
                    addMemberDepartureRow("sfDephh", "sfDepmm");
                    addLevelOfTrainingRow("sfAidLOT");

                    currentNumberOfMembers++;
                    break;
            }   
        }

        public void removeMember()
        {

        }
        public void createRowDef()
        {
            RowDefinition newRow = new RowDefinition();
            newRow.Height = new GridLength(30, GridUnitType.Pixel);
            caller.createTeamGrid.RowDefinitions.Insert(caller.createTeamGrid.RowDefinitions.Count, newRow);
            rowIndex++;
        }
        
        public void addMemberLabel(String cont) // Adds member label (i.e. "Team Member (First Aid Kit)")
        {
            createRowDef();
            //Label for first aid kit section.
            Label FAKLabel = new Label();
            FAKLabel.Content = cont;
            FAKLabel.FontStyle = FontStyles.Italic;
            FAKLabel.FontWeight = FontWeights.Bold;
            Grid.SetRow(FAKLabel, rowIndex);
            Grid.SetColumn(FAKLabel, 0);
            caller.createTeamGrid.Children.Add(FAKLabel);
        }

        public void addMemberNameRow(String textboxName)
        {
            //Name Row
            createRowDef();

            Label MemberNameLabel = new Label();
            MemberNameLabel.Content = "Name";
            Thickness margin = MemberNameLabel.Margin;
            margin.Left = 30;
            MemberNameLabel.Margin = margin;
            Grid.SetRow(MemberNameLabel, rowIndex);
            Grid.SetColumn(MemberNameLabel, 0);
            caller.createTeamGrid.Children.Add(MemberNameLabel);

            TextBox MemberName = new TextBox();
            MemberName.Name = textboxName;
            MemberName.Height = 26;
            MemberName.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(MemberName, rowIndex);
            Grid.SetColumn(MemberName, 1);
            caller.createTeamGrid.Children.Add(MemberName);

        }

        public void addMemberDepartureRow(String hh, String mm)
        {
            // Departure row
            createRowDef();

            Label DepLabel = new Label();
            DepLabel.Content = "Time of Departure (24-hour format)";
            Thickness margin = DepLabel.Margin;
            margin.Left = 30;
            DepLabel.Margin = margin;
            Grid.SetRow(DepLabel, rowIndex);
            Grid.SetColumn(DepLabel, 0);
            caller.createTeamGrid.Children.Add(DepLabel);

            TextBox Dephh = new TextBox();
            Dephh.Name = hh;
            Dephh.Text = "hh";
            Dephh.Height = 26;
            Dephh.Width = 30;
            Dephh.VerticalAlignment = VerticalAlignment.Center;
            Dephh.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetRow(Dephh, rowIndex);
            Grid.SetColumn(Dephh, 1);
            caller.createTeamGrid.Children.Add(Dephh);

            TextBox Depmm = new TextBox();
            Depmm.Name = mm;
            Depmm.Text = "mm";
            Depmm.Height = 26;
            Depmm.Width = 30;
            Depmm.VerticalAlignment = VerticalAlignment.Center;
            Depmm.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetRow(Depmm, rowIndex);
            Grid.SetColumn(Depmm, 1);
            Thickness mmMargin = Depmm.Margin;
            mmMargin.Left = 35;
            Depmm.Margin = mmMargin;
            caller.createTeamGrid.Children.Add(Depmm);
        }

        public void addLevelOfTrainingRow(String lvlOfTrainingName)
        {
            //First Aid Kit Level of Training
            createRowDef();

            Label LOT = new Label();
            LOT.Content = "Level Of Training";
            Thickness margin = LOT.Margin;
            margin.Left = 30;
            LOT.Margin = margin;
            Grid.SetRow(LOT, rowIndex);
            Grid.SetColumn(LOT, 0);
            caller.createTeamGrid.Children.Add(LOT);

            ComboBox LevelOfTraining = new ComboBox();
            LevelOfTraining.Name = lvlOfTrainingName;
            ComboBoxItem gFAid = new ComboBoxItem();
            gFAid.Content = "General First Aid";
            ComboBoxItem fResp = new ComboBoxItem();
            fResp.Content = "First Responder";
            ComboBoxItem med = new ComboBoxItem();
            med.Content = "Medicine";
            LevelOfTraining.Items.Add(gFAid);
            LevelOfTraining.Items.Add(fResp);
            LevelOfTraining.Items.Add(med);

            Grid.SetRow(LevelOfTraining, rowIndex);
            Grid.SetColumn(LevelOfTraining, 1);
            caller.createTeamGrid.Children.Add(LevelOfTraining);
        }
    }
}
