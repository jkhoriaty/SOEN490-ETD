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
    class RequestLine
    {
        private Request request;

        //time(hh/mm)
        private Border timeBorder;
        private StackPanel timeStackPanel;
        private TextBox timeHHTextBox;
        private Button timeButton;
        private TextBox timeMMTextBox;

        //client
        private Border clientBorder;
        private StackPanel clientStackPanel;
        private TextBox clientTextBox;

        //recipient
        private Border recipientBorder;
        private StackPanel recipientStackPanel;
        private TextBox recipientTextBox;

        //request
        private Border requestBorder;
        private StackPanel requestStackPanel;
        private TextBox requestTextBox;

        //handled by
        private Border handledByBorder;
        private StackPanel handledByStackPanel;
        private TextBox handledByTextBox;

        //follow up time stamp (hh/mm)
        private Border followUpBorder;
        private StackPanel followUpStackPanel;
        private TextBox followUpHHTextBox;
        private Button followUpButton;
        private TextBox followUpMMTextBox;

        //completion time stamp (hh/mm)
        private Border completionBorder;
        private StackPanel completionStackPanel;
        private TextBox completionHHTextBox;
        private Button completionButton;
        private TextBox completionMMTextBox;


        public RequestLine(Request requestLine)
        {
            this.request = request;
            BuildLine();
            PopulateLine();
        }


        private void BuildLine()
        {

            //time
            timeBorder = new Border();
            timeBorder.BorderBrush = new SolidColorBrush(Colors.Black);
            timeBorder.BorderThickness = new Thickness(0, 0, 1, 1);

            timeStackPanel = new StackPanel();
            timeStackPanel.Orientation = Orientation.Horizontal;
            timeStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            timeStackPanel.VerticalAlignment = VerticalAlignment.Center;
            timeStackPanel.Height = 40;
            timeBorder.Child = timeStackPanel;

            //HH
            timeHHTextBox = new TextBox();
            timeHHTextBox.Width = 43;
            timeHHTextBox.Height = 40;
            timeHHTextBox.GotFocus += TextBoxes_GotFocus;
            timeHHTextBox.LostFocus += TextBoxes_LostFocus;
            timeHHTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            timeHHTextBox.VerticalContentAlignment = VerticalAlignment.Center;
            timeStackPanel.Children.Add(timeHHTextBox);

            //button
            timeButton = new Button();
            timeButton.Width = 29;
            timeButton.Height = 40;
            timeButton.Content = ":";
            timeButton.HorizontalContentAlignment = HorizontalAlignment.Center;
            timeButton.VerticalContentAlignment = VerticalAlignment.Center;
            timeButton.Name = "Time";

            timeStackPanel.Children.Add(timeButton);

            //MM
            timeMMTextBox = new TextBox();
            timeMMTextBox.Width = 43;
            timeMMTextBox.Height = 40;
            timeMMTextBox.GotFocus += TextBoxes_GotFocus;
            timeMMTextBox.LostFocus += TextBoxes_LostFocus;
            timeMMTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            timeMMTextBox.VerticalContentAlignment = VerticalAlignment.Center;
            timeStackPanel.Children.Add(timeMMTextBox);



            //client
            clientBorder = new Border();
            clientBorder.BorderBrush = new SolidColorBrush(Colors.Black);
            clientBorder.BorderThickness = new Thickness(1, 0, 1,1);

            clientStackPanel = new StackPanel();
            clientStackPanel.Orientation = Orientation.Horizontal;
            clientStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            clientStackPanel.VerticalAlignment = VerticalAlignment.Center;
            clientBorder.Child = clientStackPanel;

            clientTextBox = new TextBox();
            clientTextBox.GotFocus += TextBoxes_GotFocus;
            clientTextBox.LostFocus += TextBoxes_LostFocus;
            clientTextBox.Width = 115;
            clientTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            clientTextBox.VerticalContentAlignment = VerticalAlignment.Center;
            clientTextBox.BorderBrush = new SolidColorBrush(Colors.Black);
            clientTextBox.BorderThickness = new Thickness(0, 0, 0, 1);


            //recipient
            recipientBorder = new Border();
            recipientBorder.BorderBrush = new SolidColorBrush(Colors.Black);
            recipientBorder.BorderThickness = new Thickness(1, 0, 1, 1);

            recipientStackPanel = new StackPanel();
            recipientStackPanel.Orientation = Orientation.Horizontal;
            recipientStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            recipientStackPanel.VerticalAlignment = VerticalAlignment.Center;
            recipientBorder.Child = recipientStackPanel;

            recipientTextBox = new TextBox();
            recipientTextBox.GotFocus += TextBoxes_GotFocus;
            recipientTextBox.LostFocus += TextBoxes_LostFocus;
            recipientTextBox.Width = 115;
            recipientTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            recipientTextBox.VerticalContentAlignment = VerticalAlignment.Center;
            recipientTextBox.BorderBrush = new SolidColorBrush(Colors.Black);
            recipientTextBox.BorderThickness = new Thickness(1, 0, 1, 1);

            //request
            requestBorder = new Border();
            requestBorder.BorderBrush = new SolidColorBrush(Colors.Black);
            requestBorder.BorderThickness = new Thickness(1, 0, 1, 1);

            requestStackPanel = new StackPanel();
            requestStackPanel.Orientation = Orientation.Horizontal;
            requestStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            requestStackPanel.VerticalAlignment = VerticalAlignment.Center;
            requestBorder.Child = requestStackPanel;

            requestTextBox = new TextBox();
            requestTextBox.GotFocus += TextBoxes_GotFocus;
            requestTextBox.LostFocus += TextBoxes_LostFocus;
            requestTextBox.Width = 490;
            requestTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            requestTextBox.VerticalContentAlignment = VerticalAlignment.Center;
            requestTextBox.BorderBrush = new SolidColorBrush(Colors.Black);
            requestTextBox.BorderThickness = new Thickness(0, 0, 1, 1);

            //handled by
            handledByBorder = new Border();
            handledByBorder.BorderBrush = new SolidColorBrush(Colors.Black);
            handledByBorder.BorderThickness = new Thickness(1, 0, 1, 1);

            handledByStackPanel = new StackPanel();
            handledByStackPanel.Orientation = Orientation.Horizontal;
            handledByStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            handledByStackPanel.VerticalAlignment = VerticalAlignment.Center;
            handledByBorder.Child = handledByStackPanel;

            handledByTextBox = new TextBox();
            handledByTextBox.GotFocus += TextBoxes_GotFocus;
            handledByTextBox.LostFocus += TextBoxes_LostFocus;
            handledByTextBox.Width = 115;
            handledByTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            handledByTextBox.VerticalContentAlignment = VerticalAlignment.Center;
            handledByTextBox.BorderBrush = new SolidColorBrush(Colors.Black);
            handledByTextBox.BorderThickness = new Thickness(0, 0, 1, 1);


            //follow up

            followUpBorder = new Border();
            followUpBorder.BorderBrush = new SolidColorBrush(Colors.Black);
            followUpBorder.BorderThickness = new Thickness(0, 0, 1, 1);

            followUpStackPanel = new StackPanel();
            followUpStackPanel.Orientation = Orientation.Horizontal;
            followUpStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            followUpStackPanel.VerticalAlignment = VerticalAlignment.Center;
            followUpStackPanel.Height = 40;
            followUpBorder.Child = followUpStackPanel;

            //HH
            followUpHHTextBox = new TextBox();
            followUpHHTextBox.Width = 43;
            followUpHHTextBox.Height = 40;
            followUpHHTextBox.GotFocus += TextBoxes_GotFocus;
            followUpHHTextBox.LostFocus += TextBoxes_LostFocus;
            followUpHHTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            followUpHHTextBox.VerticalContentAlignment = VerticalAlignment.Center;
            followUpStackPanel.Children.Add(followUpHHTextBox);

            //button
            followUpButton = new Button();
            followUpButton.Width = 29;
            followUpButton.Height = 40;
            followUpButton.Content = ":";
            followUpButton.HorizontalContentAlignment = HorizontalAlignment.Center;
            followUpButton.VerticalContentAlignment = VerticalAlignment.Center;
            followUpButton.Name = "FollowUpTimestamp";

            followUpStackPanel.Children.Add(followUpButton);

            //MM
            followUpMMTextBox = new TextBox();
            followUpMMTextBox.Width = 43;
            followUpMMTextBox.Height = 40;
            followUpMMTextBox.GotFocus += TextBoxes_GotFocus;
            followUpMMTextBox.LostFocus += TextBoxes_LostFocus;
            followUpMMTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            followUpMMTextBox.VerticalContentAlignment = VerticalAlignment.Center;
            followUpStackPanel.Children.Add(followUpMMTextBox);


            //completion

            completionBorder = new Border();
            completionBorder.BorderBrush = new SolidColorBrush(Colors.Black);
            completionBorder.BorderThickness = new Thickness(0, 0, 1, 1);

            completionStackPanel = new StackPanel();
            completionStackPanel.Orientation = Orientation.Horizontal;
            completionStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            completionStackPanel.VerticalAlignment = VerticalAlignment.Center;
            completionStackPanel.Height = 40;
            completionBorder.Child = completionStackPanel;

            //HH
            completionHHTextBox = new TextBox();
            completionHHTextBox.Width = 43;
            completionHHTextBox.Height = 40;
            completionHHTextBox.GotFocus += TextBoxes_GotFocus;
            completionHHTextBox.LostFocus += TextBoxes_LostFocus;
            completionHHTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            completionHHTextBox.VerticalContentAlignment = VerticalAlignment.Center;
            completionStackPanel.Children.Add(completionHHTextBox);

            //button
            completionButton = new Button();
            completionButton.Width = 29;
            completionButton.Height = 40;
            completionButton.Content = ":";
            completionButton.HorizontalContentAlignment = HorizontalAlignment.Center;
            completionButton.VerticalContentAlignment = VerticalAlignment.Center;
            completionButton.Name = "CompletionTimestamp";

            completionStackPanel.Children.Add(completionButton);

            //MM
            completionMMTextBox = new TextBox();
            completionMMTextBox.Width = 43;
            completionMMTextBox.Height = 40;
            completionMMTextBox.GotFocus += TextBoxes_GotFocus;
            completionMMTextBox.LostFocus += TextBoxes_LostFocus;
            completionMMTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            completionMMTextBox.VerticalContentAlignment = VerticalAlignment.Center;
            completionStackPanel.Children.Add(completionMMTextBox);

        }

        //Accessors

        //client
        public Border getClientNameBorder()
        {
            return clientBorder;
        }

        public TextBox getClientNameTextBox()
        {
            return clientTextBox;
        }

        //recipient
        public Border getRecipientNameBorder()
        {
            return recipientBorder;
        }

        public TextBox getRecipientNameTextBox()
        {
            return recipientTextBox;
        }

        //request
        public Border getRequestNameBorder()
        {
            return requestBorder;
        }
        public TextBox getRequestNameTextBox()
        {
            return requestTextBox;
        }

        //handled by
        public Border getHandledByNameBorder()
        {
            return handledByBorder;
        }
        public TextBox getHandledByNameTextBox()
        {
            return handledByTextBox;
        }

        //time
        public Border getTimeBorder()
        {
            return timeBorder;
        }

        public TextBox getTimeHHTextBox()
        {
            return timeHHTextBox;
        }

        public TextBox getTimeMMTextBox()
        {
            return timeMMTextBox;
        }

        public Button getTimeButton()
        {
            return timeButton;
        }

        //follow up
        public Border getFollowUpBorder()
        {
            return followUpBorder;
        }

        public TextBox getFollowUpHHTextBox()
        {
            return followUpHHTextBox;
        }

        public TextBox getFollowUpMMTextBox()
        {
            return followUpMMTextBox;
        }

        public Button getFollowUpTimeButton()
        {
            return followUpButton;
        }

        //completion time
        public Border getCompletionBorder()
        {
            return completionBorder;
        }

        public TextBox getCompletionHHTextBox()
        {
            return completionHHTextBox;
        }

        public TextBox getCompletionMMTextBox()
        {
            return completionMMTextBox;
        }

        public Button getCompletionTimeButton()
        {
            return completionButton;
        }

        private void PopulateLine()
        {
            //time
            timeHHTextBox.Text = "hh";
            timeMMTextBox.Text = "mm";

            //client
            clientTextBox.Text = "-" ;

            //recipient
            recipientTextBox.Text = "-";

            //request
            requestTextBox.Text = "-";

            //handled by
            handledByTextBox.Text = "-";

            //follow up time stamp (hh/mm)
            followUpHHTextBox.Text = "hh";
            followUpMMTextBox.Text = "mm";

            //completion time stamp (hh/mm)
            completionHHTextBox.Text = "hh";
            completionMMTextBox.Text = "mm";
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

    }
}
