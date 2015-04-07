using ETD.Models.Objects;
using ETD.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Web;

namespace ETD.CustomObjects.CustomUIObjects
{
    class ShiftLine
    {
		//Variables used to create a new shift
        private Shift shift;
        private Border sectorBorder;
        private StackPanel sectorStackPanel;
        private TextBox sectorTextBox;

        private Border teamBorder;
        private StackPanel teamStackPanel;
        private TextBox teamNameTextBox;

        private Border startTimeBorder;
        private StackPanel startTimeStackPanel;
        private TextBox startTimeHHTextBox;
        private Button startTimeButton;
        private TextBox startTimeMMTextBox;

 
        public ShiftLine(Shift shift)
        {
            this.shift = shift;
            BuildLine();
            PopulateLine();
        }

		//Create a new shift line 
        private void BuildLine()
        {
            //sector
            sectorBorder = new Border();
            sectorBorder.BorderBrush = new SolidColorBrush(Colors.Black);
            sectorBorder.BorderThickness = new Thickness(1, 0, 1, 1);

            sectorStackPanel = new StackPanel();
            sectorStackPanel.Orientation = Orientation.Horizontal;
            sectorStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            sectorStackPanel.VerticalAlignment = VerticalAlignment.Center;
            sectorBorder.Child = sectorStackPanel;

            sectorTextBox = new TextBox();
            sectorTextBox.Width = 155;
            sectorTextBox.GotFocus += TextBoxes_GotFocus;
            sectorTextBox.LostFocus += TextBoxes_LostFocus;
            sectorTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            sectorTextBox.VerticalContentAlignment = VerticalAlignment.Center;
            sectorTextBox.BorderBrush = new SolidColorBrush(Colors.Black);
            sectorTextBox.BorderThickness = new Thickness(1, 0, 1, 1);
           

            //team
            teamBorder = new Border();
            teamBorder.BorderBrush = new SolidColorBrush(Colors.Black);
            teamBorder.BorderThickness = new Thickness(0, 0, 1, 1);

            teamStackPanel = new StackPanel();
            teamStackPanel.Orientation = Orientation.Horizontal;
            teamStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            teamStackPanel.VerticalAlignment = VerticalAlignment.Center;
            teamBorder.Child = teamStackPanel;

            teamNameTextBox = new TextBox();
            teamNameTextBox.GotFocus += TextBoxes_GotFocus;
            teamNameTextBox.LostFocus += TextBoxes_LostFocus;
            teamNameTextBox.Width = 172;
            teamNameTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            teamNameTextBox.VerticalContentAlignment = VerticalAlignment.Center;
            teamNameTextBox.BorderBrush = new SolidColorBrush(Colors.Black);
            teamNameTextBox.BorderThickness = new Thickness(0, 0, 1, 1);
          
            //startTime
            startTimeBorder = new Border();
            startTimeBorder.BorderBrush = new SolidColorBrush(Colors.Black);
            startTimeBorder.BorderThickness = new Thickness(0, 1, 1, 1);

            startTimeStackPanel = new StackPanel();
            startTimeStackPanel.Orientation = Orientation.Horizontal;
            startTimeStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            startTimeStackPanel.VerticalAlignment = VerticalAlignment.Center;
            startTimeBorder.Child = startTimeStackPanel;

            //HH
            startTimeHHTextBox = new TextBox();
            startTimeHHTextBox.Width = 60;
            startTimeHHTextBox.Height = 50;
            startTimeHHTextBox.GotFocus += TextBoxes_GotFocus;
            startTimeHHTextBox.LostFocus += TextBoxes_LostFocus;
            startTimeHHTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            startTimeHHTextBox.VerticalContentAlignment = VerticalAlignment.Center;
            startTimeStackPanel.Children.Add(startTimeHHTextBox);

            startTimeButton = new Button();
            startTimeButton.Width = 52;
            startTimeButton.Height = 50;
            startTimeButton.Content = ":";
            startTimeButton.HorizontalContentAlignment = HorizontalAlignment.Center;
            startTimeButton.VerticalContentAlignment = VerticalAlignment.Center;
            startTimeButton.Name = "StartTime";
            
            startTimeStackPanel.Children.Add(startTimeButton);

            //MM
            startTimeMMTextBox = new TextBox();
            startTimeMMTextBox.Width = 60;
            startTimeMMTextBox.Height = 50;
            startTimeMMTextBox.GotFocus += TextBoxes_GotFocus;
            startTimeMMTextBox.LostFocus += TextBoxes_LostFocus;
            startTimeMMTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            startTimeMMTextBox.VerticalContentAlignment = VerticalAlignment.Center;
            startTimeStackPanel.Children.Add(startTimeMMTextBox);
        }

		//Set up default values of a shift
        private void PopulateLine()
        {
            teamNameTextBox.Text = "-";
            sectorTextBox.Text = "-";
            startTimeHHTextBox.Text = "hh";
            startTimeMMTextBox.Text = "mm";
        }


        //Called when the textboxes gain focus
        private void TextBoxes_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxHandler.GotFocus(sender, e);
        }

        //Called when the textboxes lose focus
        private void TextBoxes_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBoxHandler.LostFocus(sender, e);

        }

		//Returns the sector's border
        public Border getSectorNameBorder()
        {
            return sectorBorder;
        }

		//Returns the sector's textbox
        public TextBox getSectorNameTextBox()
        {
            return sectorTextBox;
        }

		//Returns the team's textbox
        public TextBox getTeamNameTextBox()
        {
            return teamNameTextBox;
        }

		//Returns the start time border for the hours
        public Border getStartTimeBorder()
        {
            return startTimeBorder;
        }

		//Returns the start time border for the minutes
        public TextBox getStartTimeHHTextBox()
        {
            return startTimeHHTextBox;
        }

        public TextBox getStartTimeMMTextBox()
        {
            return startTimeMMTextBox;
        }

        public Button getStartTimeButton()
        {
            return startTimeButton;
        }
    }
}
