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

namespace ETD.CustomObjects.PopupForms
{
    /// <summary>
    /// Interaction logic for FollowUpSectionForm.xaml
    /// </summary>
    public partial class FollowUpSectionForm : Page
    {
        private FollowUpSectionForm followupPage;
        private Request followupInfo;
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

            // Adding textbox information to the appropriate list 
            setupTimestampMap();
            setupClientMap();
            setupRecipientMap();
            setupRequestMap();
            setupHandledByMap();
            setupFollowupTimeStampMap();
            setupCompletionTimeStampMap();

        }

        //Populating the list of time stamps
        private void setupTimestampMap()
        {
            timestampMap.Add("TimestampT1", TextBoxHandler.textboxArray(TimestamphhT1, TimestampmmT1));
            timestampMap.Add("TimestampT2", TextBoxHandler.textboxArray(TimestamphhT2, TimestampmmT2));
            timestampMap.Add("TimestampT3", TextBoxHandler.textboxArray(TimestamphhT3, TimestampmmT3));
            timestampMap.Add("TimestampT4", TextBoxHandler.textboxArray(TimestamphhT4, TimestampmmT4));
            timestampMap.Add("TimestampT5", TextBoxHandler.textboxArray(TimestamphhT5, TimestampmmT5));
            timestampMap.Add("TimestampT6", TextBoxHandler.textboxArray(TimestamphhT6, TimestampmmT6));
            timestampMap.Add("TimestampT7", TextBoxHandler.textboxArray(TimestamphhT7, TimestampmmT7));
            timestampMap.Add("TimestampT8", TextBoxHandler.textboxArray(TimestamphhT8, TimestampmmT8));
            timestampMap.Add("TimestampT9", TextBoxHandler.textboxArray(TimestamphhT9, TimestampmmT9));
            timestampMap.Add("TimestampT10", TextBoxHandler.textboxArray(TimestamphhT10, TimestampmmT10));
        }

        //Populating the list of clients
        private void setupClientMap()
        {
            clientMap.Add("Client1", Client1);
            clientMap.Add("Client2", Client2);
            clientMap.Add("Client3", Client3);
            clientMap.Add("Client4", Client4);
            clientMap.Add("Client5", Client5);
            clientMap.Add("Client6", Client6);
            clientMap.Add("Client7", Client7);
            clientMap.Add("Client8", Client8);
            clientMap.Add("Client9", Client9);
            clientMap.Add("Client10", Client10);

        }

        //Populating the list of recipients
        private void setupRecipientMap()
        {
            recipientMap.Add("Recipient1", Recipient1);
            recipientMap.Add("Recipient2", Recipient2);
            recipientMap.Add("Recipient3", Recipient3);
            recipientMap.Add("Recipient4", Recipient4);
            recipientMap.Add("Recipient5", Recipient5);
            recipientMap.Add("Recipient6", Recipient6);
            recipientMap.Add("Recipient7", Recipient7);
            recipientMap.Add("Recipient8", Recipient8);
            recipientMap.Add("Recipient9", Recipient9);
            recipientMap.Add("Recipient10", Recipient10);
        }

        //Populating the list of requests
        private void setupRequestMap()
        {
            requestMap.Add("Request1", Request1);
            requestMap.Add("Request2", Request2);
            requestMap.Add("Request3", Request3);
            requestMap.Add("Request4", Request4);
            requestMap.Add("Request5", Request5);
            requestMap.Add("Request6", Request6);
            requestMap.Add("Request7", Request7);
            requestMap.Add("Request8", Request8);
            requestMap.Add("Request9", Request9);
            requestMap.Add("Request10", Request10);
        }

        //Populating the list of handled by
        private void setupHandledByMap()
        {
            handledbyMap.Add("HandledBy1", HandledBy1);
            handledbyMap.Add("HandledBy2", HandledBy2);
            handledbyMap.Add("HandledBy3", HandledBy3);
            handledbyMap.Add("HandledBy4", HandledBy4);
            handledbyMap.Add("HandledBy5", HandledBy5);
            handledbyMap.Add("HandledBy6", HandledBy6);
            handledbyMap.Add("HandledBy7", HandledBy7);
            handledbyMap.Add("HandledBy8", HandledBy8);
            handledbyMap.Add("HandledBy9", HandledBy9);
            handledbyMap.Add("HandledBy10", HandledBy10);
        }

        //Populating the list of follow up time stamps
        private void setupFollowupTimeStampMap()
        {
            followupTimestampMap.Add("Timestamps1", TextBoxHandler.textboxArray(Timestamphhs1, Timestampmms1));
            followupTimestampMap.Add("Timestamps2", TextBoxHandler.textboxArray(Timestamphhs2, Timestampmms2));
            followupTimestampMap.Add("Timestamps3", TextBoxHandler.textboxArray(Timestamphhs3, Timestampmms3));
            followupTimestampMap.Add("Timestamps4", TextBoxHandler.textboxArray(Timestamphhs4, Timestampmms4));
            followupTimestampMap.Add("Timestamps5", TextBoxHandler.textboxArray(Timestamphhs5, Timestampmms5));
            followupTimestampMap.Add("Timestamps6", TextBoxHandler.textboxArray(Timestamphhs6, Timestampmms6));
            followupTimestampMap.Add("Timestamps7", TextBoxHandler.textboxArray(Timestamphhs7, Timestampmms7));
            followupTimestampMap.Add("Timestamps8", TextBoxHandler.textboxArray(Timestamphhs8, Timestampmms8));
            followupTimestampMap.Add("Timestamps9", TextBoxHandler.textboxArray(Timestamphhs9, Timestampmms9));
            followupTimestampMap.Add("Timestamps10", TextBoxHandler.textboxArray(Timestamphhs10, Timestampmms10));
        }

        //Populating the list of completion time stamps
        private void setupCompletionTimeStampMap()
        {
            completionTimestampMap.Add("Timestampf1", TextBoxHandler.textboxArray(Timestamphhf1, Timestampmmf1));
            completionTimestampMap.Add("Timestampf2", TextBoxHandler.textboxArray(Timestamphhf2, Timestampmmf2));
            completionTimestampMap.Add("Timestampf3", TextBoxHandler.textboxArray(Timestamphhf3, Timestampmmf3));
            completionTimestampMap.Add("Timestampf4", TextBoxHandler.textboxArray(Timestamphhf4, Timestampmmf4));
            completionTimestampMap.Add("Timestampf5", TextBoxHandler.textboxArray(Timestamphhf5, Timestampmmf5));
            completionTimestampMap.Add("Timestampf6", TextBoxHandler.textboxArray(Timestamphhf6, Timestampmmf6));
            completionTimestampMap.Add("Timestampf7", TextBoxHandler.textboxArray(Timestamphhf7, Timestampmmf7));
            completionTimestampMap.Add("Timestampf8", TextBoxHandler.textboxArray(Timestamphhf8, Timestampmmf8));
            completionTimestampMap.Add("Timestampf9", TextBoxHandler.textboxArray(Timestamphhf9, Timestampmmf9));
            completionTimestampMap.Add("Timestampf10", TextBoxHandler.textboxArray(Timestamphhf10, Timestampmmf10));
        }

        //Updating the time stamp information for the follow up and completion textbox
        private void UpdateFollowUpInformation(int position, TextBox timestamphhtBox, TextBox timestampmmtBox, TextBox clientbox, TextBox requestBox, TextBox recipientBox, TextBox handledbyMapBox, 
                                                 TextBox timestamphhsBox, TextBox timestampmmsBox, TextBox timestamphhfBox, TextBox timestampmmfBox)
        {
            //Set the follow up timestamp to 0
            int timestampshh = 0;
            int timestampsmm = 0;

            //Set the completion timestamp to 0
            int timestampfhh = 0;
            int timestampfmm = 0;

            //Set Time timestamp to 0
            int timestampThh = 0;
            int timestampTmm = 0;

            //Checks if the field is present on the form
            if (!timestamphhsBox.Text.Equals("hh") && !timestampmmsBox.Text.Equals("mm"))
            {
                timestampshh = int.Parse(timestamphhsBox.Text);
                timestampsmm = int.Parse(timestampmmsBox.Text);
            }
            if (!timestamphhfBox.Text.Equals("hh") && !timestampmmfBox.Text.Equals("mm"))
            {
                timestampfhh = int.Parse(timestamphhfBox.Text);
                timestampfmm = int.Parse(timestampmmfBox.Text);
            }

            if (!timestamphhtBox.Text.Equals("hh") && !timestampmmtBox.Text.Equals("mm"))
            {
                timestampThh = int.Parse(timestamphhtBox.Text);
                timestampTmm = int.Parse(timestampmmtBox.Text);
            }

            //Get the current time 
            DateTime timestampFollowup = DateTime.Now;
            DateTime timestampCompletion = DateTime.Now;
            DateTime timestampTime = DateTime.Now;

            //Set the follow up and completion time stamp to the current time
            timestampFollowup = timestampFollowup.Date + new TimeSpan(timestampshh, timestampsmm, 0);
            timestampCompletion = timestampCompletion.Date + new TimeSpan(timestampfhh, timestampfmm, 0);
            followupInfo.setFollowupInfo(position, new Request(timestampTime, clientbox.Text, requestBox.Text, recipientBox.Text, handledbyMapBox.Text, timestampFollowup, timestampCompletion));

        }

        //Focus on either the client, recipient , request, handle by text box
        private void FollowUpInfo_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxHandler.GotFocus(sender, e);
        }

        //Focus on the follow up or completion time stamp text box
        private void TextBoxes_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxHandler.GotFocus(sender, e);
        }

        //Recovering the fields default text if left empty
        private void TextBoxes_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBoxHandler.LostFocus(sender, e);
        }

        //Sets the current hours and minutes in the passed TextBoxes
        public void TimeStampFait_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            TextBoxHandler.setNow(completionTimestampMap[bt.Name][0], completionTimestampMap[bt.Name][1]);
        }

        //Sets the current hours and minutes in the passed TextBoxes
        public void TimestampSuivi_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            TextBoxHandler.setNow(followupTimestampMap[bt.Name][0], followupTimestampMap[bt.Name][1]);
        }

        //Sets the current hours and minutes in the passed TextBoxes
        public void TimestampTime_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            TextBoxHandler.setNow(timestampMap[bt.Name][0], timestampMap[bt.Name][1]);
        }
    }

}
