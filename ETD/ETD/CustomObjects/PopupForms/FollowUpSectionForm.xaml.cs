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

namespace ETD.CustomObjects.PopupForms
{
    /// <summary>
    /// Interaction logic for FollowUpSectionForm.xaml
    /// </summary>
    public partial class FollowUpSectionForm : Page
    {
        private FollowUpSectionForm followupPage;
        private Request followupInfo;
        Request currentRequest;
        RequestLine requestLine;
        int rowNumber = 1;
        private String init = "-";

        private Dictionary<String, TextBox[]> timestampMap = new Dictionary<String, TextBox[]>();//Contains all time stamps
        private Dictionary<String, TextBox> clientMap = new Dictionary<String, TextBox>();//Contains all clients
        private Dictionary<String, TextBox> recipientMap = new Dictionary<String, TextBox>();//Contains all Recipients
        private Dictionary<String, TextBox> requestMap = new Dictionary<String, TextBox>();//Contains all request
        private Dictionary<String, TextBox> handledbyMap = new Dictionary<String, TextBox>();//Contains all handledby
        private Dictionary<String, TextBox[]> followupTimestampMap = new Dictionary<String, TextBox[]>();//Contains all follow up time stamps
        private Dictionary<String, TextBox[]> completionTimestampMap = new Dictionary<String, TextBox[]>();//Contains all completion time stamps

        //Create a follow up form containing a list of special requests made during the operation
        public FollowUpSectionForm()
        {
            InitializeComponent();
            PopulateRequestForm();
        }

        private void PopulateRequestForm()
        {
            //Set up default shift properties
            currentRequest = new Request(init, init, init, init, init, init, init, init, init, init);
            requestLine = new RequestLine(currentRequest);

            requestLine.getClientNameTextBox().KeyDown += NewRequest;
            requestLine.getRecipientNameTextBox().KeyDown += NewRequest;
            requestLine.getRequestNameTextBox().KeyDown += NewRequest;
            requestLine.getHandledByNameTextBox().KeyDown += NewRequest;
        
            RowDefinition sectorRowDefinition = new RowDefinition();
            sectorRowDefinition.Height = new GridLength(40);
            Request_Grid.RowDefinitions.Add(sectorRowDefinition);

            //populate time
            Request_Grid.Children.Add(requestLine.getTimeBorder());
            Grid.SetColumn(requestLine.getTimeBorder(), 0);
            Grid.SetRow(requestLine.getTimeBorder(), rowNumber);
            requestLine.getTimeButton().Click += TimestampTime_Click;

            timestampMap.Add("Time" + rowNumber, TextBoxHandler.textboxArray(requestLine.getTimeHHTextBox(), requestLine.getTimeMMTextBox()));

            //populate client
            Request_Grid.Children.Add(requestLine.getClientNameTextBox());
            Grid.SetColumn(requestLine.getClientNameBorder(), 1);
            Grid.SetRow(requestLine.getClientNameBorder(), rowNumber);
            Grid.SetColumn(requestLine.getClientNameTextBox(), 1);
            Grid.SetRow(requestLine.getClientNameTextBox(), rowNumber);

            clientMap.Add("Client" + rowNumber, requestLine.getClientNameTextBox());

            //populate recipient
            Request_Grid.Children.Add(requestLine.getRecipientNameTextBox());
            Grid.SetColumn(requestLine.getRecipientNameBorder(), 2);
            Grid.SetRow(requestLine.getRecipientNameBorder(), rowNumber);
            Grid.SetColumn(requestLine.getRecipientNameTextBox(), 2);
            Grid.SetRow(requestLine.getRecipientNameTextBox(), rowNumber);

            recipientMap.Add("Recipient" + rowNumber, requestLine.getRecipientNameTextBox());

            //populate request
            Request_Grid.Children.Add(requestLine.getRequestNameTextBox());
            Grid.SetColumn(requestLine.getRequestNameBorder(), 3);
            Grid.SetRow(requestLine.getRequestNameBorder(), rowNumber);
            Grid.SetColumn(requestLine.getRequestNameTextBox(), 3);
            Grid.SetRow(requestLine.getRequestNameTextBox(), rowNumber);

            requestMap.Add("Request" + rowNumber, requestLine.getRequestNameTextBox());

            //populate handled by
            Request_Grid.Children.Add(requestLine.getHandledByNameTextBox());
            Grid.SetColumn(requestLine.getHandledByNameBorder(), 4);
            Grid.SetRow(requestLine.getHandledByNameBorder(), rowNumber);
            Grid.SetColumn(requestLine.getHandledByNameTextBox(), 4);
            Grid.SetRow(requestLine.getHandledByNameTextBox(), rowNumber);

            handledbyMap.Add("HandledBy" + rowNumber, requestLine.getHandledByNameTextBox());

            //populate follow up
            Request_Grid.Children.Add(requestLine.getFollowUpBorder());
            Grid.SetColumn(requestLine.getFollowUpBorder(), 5);
            Grid.SetRow(requestLine.getFollowUpBorder(), rowNumber);
            requestLine.getFollowUpTimeButton().Click += TimestampFollowUp_Click;

            followupTimestampMap.Add("FollowUpTimestamp" + rowNumber, TextBoxHandler.textboxArray(requestLine.getFollowUpHHTextBox(), requestLine.getFollowUpMMTextBox()));

            //populate completion
            Request_Grid.Children.Add(requestLine.getCompletionBorder());
            Grid.SetColumn(requestLine.getCompletionBorder(), 6);
            Grid.SetRow(requestLine.getCompletionBorder(), rowNumber);
            requestLine.getCompletionTimeButton().Click += TimeStampCompletion_Click;

            completionTimestampMap.Add("CompletionTimestamp" + rowNumber, TextBoxHandler.textboxArray(requestLine.getCompletionHHTextBox(), requestLine.getCompletionMMTextBox()));

        }

        //Create a new request row
        public void NewRequest(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                rowNumber++;
                PopulateRequestForm();
            }
        }

        //Sets the current hours and minutes in the passed TextBoxes
        public void TimeStampCompletion_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            TextBoxHandler.setNow(completionTimestampMap[bt.Name + rowNumber][0], completionTimestampMap[bt.Name + rowNumber][1]);
        }

        //Sets the current hours and minutes in the passed TextBoxes
        public void TimestampFollowUp_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            TextBoxHandler.setNow(followupTimestampMap[bt.Name + rowNumber][0], followupTimestampMap[bt.Name + rowNumber][1]);
        }

        //Sets the current hours and minutes in the passed TextBoxes
        public void TimestampTime_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            TextBoxHandler.setNow(timestampMap[bt.Name + rowNumber][0], timestampMap[bt.Name + rowNumber][1]);
        }
    }

}
