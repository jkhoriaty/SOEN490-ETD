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
using ETD.Models.Objects;
using ETD.Services;
using ETD.CustomObjects.CustomUIObjects;
using ETD.CustomObjects.PopupForms;

namespace ETD.CustomObjects.CustomUIObjects
{
    class RequestLine
    {
        private FollowUpSectionForm followupPage;

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

        Request request;
        RequestLine requestline;
        int rowNumber = 1;
        private String init = "-";

        private Dictionary<String, TextBox[]> timestampMap = new Dictionary<String, TextBox[]>();//Contains all time stamps
        private Dictionary<String, TextBox> clientMap = new Dictionary<String, TextBox>();//Contains all clients
        private Dictionary<String, TextBox> recipientMap = new Dictionary<String, TextBox>();//Contains all Recipients
        private Dictionary<String, TextBox> requestMap = new Dictionary<String, TextBox>();//Contains all request
        private Dictionary<String, TextBox> handledbyMap = new Dictionary<String, TextBox>();//Contains all handledby
        private Dictionary<String, TextBox[]> followupTimestampMap = new Dictionary<String, TextBox[]>();//Contains all follow up time stamps
        private Dictionary<String, TextBox[]> completionTimestampMap = new Dictionary<String, TextBox[]>();//Contains all completion time stamps
        private static List<RequestLine> requestLineList = new List<RequestLine>();

        public void doRequestLine(FollowUpSectionForm followupsection)
        {
            followupPage = followupsection;
            populateRequestForm();

        }

        public void populateRequestForm()
        {
            BuildLine();
            PopulateLine();

            //Set up default shift properties
            request = new Request(init, init, init, init, init, init, init, init, init, init);
            requestline = new RequestLine();


            RowDefinition sectorRowDefinition = new RowDefinition();
            sectorRowDefinition.Height = new GridLength(40);
            followupPage.getRequestGrid().RowDefinitions.Add(sectorRowDefinition);

            //populate time
            followupPage.getRequestGrid().Children.Add(this.getTimeBorder());
            Grid.SetColumn(this.getTimeBorder(), 0);
            Grid.SetRow(this.getTimeBorder(), rowNumber);


            timestampMap.Add("Time" + rowNumber, TextBoxHandler.textboxArray(this.getTimeHHTextBox(), this.getTimeMMTextBox()));

            //populate client
            followupPage.getRequestGrid().Children.Add(this.getClientNameTextBox());
            Grid.SetColumn(this.getClientNameBorder(), 1);
            Grid.SetRow(this.getClientNameBorder(), rowNumber);
            Grid.SetColumn(this.getClientNameTextBox(), 1);
            Grid.SetRow(this.getClientNameTextBox(), rowNumber);

            clientMap.Add("Client" + rowNumber, this.getClientNameTextBox());

            //populate recipient
            followupPage.getRequestGrid().Children.Add(this.getRecipientNameTextBox());
            Grid.SetColumn(this.getRecipientNameBorder(), 2);
            Grid.SetRow(this.getRecipientNameBorder(), rowNumber);
            Grid.SetColumn(this.getRecipientNameTextBox(), 2);
            Grid.SetRow(this.getRecipientNameTextBox(), rowNumber);

            recipientMap.Add("Recipient" + rowNumber, this.getRecipientNameTextBox());

            //populate request
            followupPage.getRequestGrid().Children.Add(this.getRequestNameTextBox());
            Grid.SetColumn(this.getRequestNameBorder(), 3);
            Grid.SetRow(this.getRequestNameBorder(), rowNumber);
            Grid.SetColumn(this.getRequestNameTextBox(), 3);
            Grid.SetRow(this.getRequestNameTextBox(), rowNumber);

            requestMap.Add("Request" + rowNumber, this.getRequestNameTextBox());

            //populate handled by
            followupPage.getRequestGrid().Children.Add(this.getHandledByNameTextBox());
            Grid.SetColumn(this.getHandledByNameBorder(), 4);
            Grid.SetRow(this.getHandledByNameBorder(), rowNumber);
            Grid.SetColumn(this.getHandledByNameTextBox(), 4);
            Grid.SetRow(this.getHandledByNameTextBox(), rowNumber);

            handledbyMap.Add("HandledBy" + rowNumber, this.getHandledByNameTextBox());

            //populate follow up
            followupPage.getRequestGrid().Children.Add(this.getFollowUpBorder());
            Grid.SetColumn(this.getFollowUpBorder(), 5);
            Grid.SetRow(this.getFollowUpBorder(), rowNumber);


            followupTimestampMap.Add("FollowUpTimestamp" + rowNumber, TextBoxHandler.textboxArray(this.getFollowUpHHTextBox(), this.getFollowUpMMTextBox()));

            //populate completion
            followupPage.getRequestGrid().Children.Add(this.getCompletionBorder());
            Grid.SetColumn(this.getCompletionBorder(), 6);
            Grid.SetRow(this.getCompletionBorder(), rowNumber);

            completionTimestampMap.Add("CompletionTimestamp" + rowNumber, TextBoxHandler.textboxArray(this.getCompletionHHTextBox(), this.getCompletionMMTextBox()));

            requestLineList.Add(this);
        }


        //Create a new request row
        public void NewRequest(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                rowNumber++;
                populateRequestForm();
            }
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
            timeButton.Click += TimestampTime_Click;
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
            clientBorder.BorderThickness = new Thickness(1, 0, 1, 1);

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
            clientTextBox.KeyDown += NewRequest;

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
            recipientTextBox.KeyDown += NewRequest;
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
            requestTextBox.KeyDown += NewRequest;

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
            handledByTextBox.KeyDown += NewRequest;

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
            followUpButton.Click += TimestampFollowUp_Click;
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
            completionButton.Click += TimeStampCompletion_Click;
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
            clientTextBox.Text = "-";

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

            //If the textbox is for the resource name, update the resource
            if ((TextBox)sender == requestTextBox)
            {
                UpdateRequest();
            }
        }

        private void UpdateRequest()
        {
            request.setClient(clientTextBox.Text);
            request.setHandledBy(handledByTextBox.Text);
            request.setRecipient(recipientTextBox.Text);
            request.setRequest(requestTextBox.Text);

            if (!timeHHTextBox.Text.Equals("hh") && !timeMMTextBox.Text.Equals("mm"))
            {
                try
                {
                    request.setTimeHH(timeHHTextBox.Text);
                    request.setTimeMM(timeMMTextBox.Text);
                }
                catch (Exception e)
                {
                    return;
                }
            }

            if (!followUpHHTextBox.Text.Equals("hh") && !followUpMMTextBox.Text.Equals("mm"))
            {
                try
                {
                    request.setFollowUpHH(followUpHHTextBox.Text);
                    request.setFollowUpMM(followUpMMTextBox.Text);
                }
                catch (Exception e)
                {
                    return;
                }
            }

            if (!completionHHTextBox.Text.Equals("hh") && !completionMMTextBox.Text.Equals("mm"))
            {
                try
                {
                    request.setCompletionHH(completionHHTextBox.Text);
                    request.setCompletionMM(completionMMTextBox.Text);
                }
                catch (Exception e)
                {
                    return;
                }
            }

        }

        //Sets the current hours and minutes in the passed TextBoxes
        public void TimeStampCompletion_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            TextBoxHandler.setNow(completionTimestampMap[bt.Name + rowNumber][0], completionTimestampMap[bt.Name + rowNumber][1]);
            request.setCompletionHH(completionTimestampMap[bt.Name + rowNumber][0].Text.ToString());
            request.setCompletionMM(completionTimestampMap[bt.Name + rowNumber][1].Text.ToString());
        }

        //Sets the current hours and minutes in the passed TextBoxes
        public void TimestampFollowUp_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            TextBoxHandler.setNow(followupTimestampMap[bt.Name + rowNumber][0], followupTimestampMap[bt.Name + rowNumber][1]);
            request.setFollowUpHH(followupTimestampMap[bt.Name + rowNumber][0].Text.ToString());
            request.setFollowUpMM(followupTimestampMap[bt.Name + rowNumber][1].Text.ToString());
        }

        //Sets the current hours and minutes in the passed TextBoxes
        public void TimestampTime_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            TextBoxHandler.setNow(timestampMap[bt.Name + rowNumber][0], timestampMap[bt.Name + rowNumber][1]);
            request.setTimeHH(timestampMap[bt.Name + rowNumber][0].Text.ToString());
            request.setTimeMM(timestampMap[bt.Name + rowNumber][1].Text.ToString());
        }

        //return list of requests
        public static List<RequestLine> getRequestLineList()
        {
            return requestLineList;
        }
    }
}
