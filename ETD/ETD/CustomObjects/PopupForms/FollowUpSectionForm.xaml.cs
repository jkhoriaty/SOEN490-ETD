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
        private FollowUpSectionForm FollowUpPage;
        private Request FollowUpInfo;
        private Dictionary<String, TextBox[]> TimeStampMap = new Dictionary<String, TextBox[]>();
        private Dictionary<String, TextBox> ClientMap = new Dictionary<String, TextBox>();
        private Dictionary<String, TextBox> RecipientMap = new Dictionary<String, TextBox>();
        private Dictionary<String, TextBox> RequestMap = new Dictionary<String, TextBox>();
        private Dictionary<String, TextBox> HandledByMap = new Dictionary<String, TextBox>();
        private Dictionary<String, TextBox[]> SuiviTimeStampMap = new Dictionary<String, TextBox[]>();
        private Dictionary<String, TextBox[]> FaitTimeStampMap = new Dictionary<String, TextBox[]>();
        //private Dictionary<String, String> TimesEquivalentKeyMap = new Dictionary<String, String>();



        public FollowUpSectionForm()
        {
            InitializeComponent();
            //this.FollowUpPage = FollowUpPage;

            setupTimeMap();
            setupClientMap();
            setupRecipientMap();
            setupRequestMap();
            setupHandledByMap();
            setupSuiviTimeStampMap();
            //setupTimesEquivalentKeyMap();
            setupFaitTimeStampMap();

        }

        private void setupTimeMap()
        {
            TimeStampMap.Add("TimestampT1", TextBoxHandler.textboxArray(TimestamphhT1, TimestampmmT1));
            TimeStampMap.Add("TimestampT2", TextBoxHandler.textboxArray(TimestamphhT2, TimestampmmT2));
            TimeStampMap.Add("TimestampT3", TextBoxHandler.textboxArray(TimestamphhT3, TimestampmmT3));
            TimeStampMap.Add("TimestampT4", TextBoxHandler.textboxArray(TimestamphhT4, TimestampmmT4));
            TimeStampMap.Add("TimestampT5", TextBoxHandler.textboxArray(TimestamphhT5, TimestampmmT5));
            TimeStampMap.Add("TimestampT6", TextBoxHandler.textboxArray(TimestamphhT6, TimestampmmT6));
            TimeStampMap.Add("TimestampT7", TextBoxHandler.textboxArray(TimestamphhT7, TimestampmmT7));
            TimeStampMap.Add("TimestampT8", TextBoxHandler.textboxArray(TimestamphhT8, TimestampmmT8));
            TimeStampMap.Add("TimestampT9", TextBoxHandler.textboxArray(TimestamphhT9, TimestampmmT9));
            TimeStampMap.Add("TimestampT10", TextBoxHandler.textboxArray(TimestamphhT10, TimestampmmT10));
        }

   
        private void setupClientMap()
        {
            ClientMap.Add("Client1", Client1);
            ClientMap.Add("Client2", Client2);
            ClientMap.Add("Client3", Client3);
            ClientMap.Add("Client4", Client4);
            ClientMap.Add("Client5", Client5);
            ClientMap.Add("Client6", Client6);
            ClientMap.Add("Client7", Client7);
            ClientMap.Add("Client8", Client8);
            ClientMap.Add("Client9", Client9);
            ClientMap.Add("Client10", Client10);

        }

        private void setupRecipientMap()
        {
            RecipientMap.Add("Recipient1", Recipient1);
            RecipientMap.Add("Recipient2", Recipient2);
            RecipientMap.Add("Recipient3", Recipient3);
            RecipientMap.Add("Recipient4", Recipient4);
            RecipientMap.Add("Recipient5", Recipient5);
            RecipientMap.Add("Recipient6", Recipient6);
            RecipientMap.Add("Recipient7", Recipient7);
            RecipientMap.Add("Recipient8", Recipient8);
            RecipientMap.Add("Recipient9", Recipient9);
            RecipientMap.Add("Recipient10", Recipient10);
        }

        private void setupRequestMap()
        {
            RequestMap.Add("Request1", Request1);
            RequestMap.Add("Request2", Request2);
            RequestMap.Add("Request3", Request3);
            RequestMap.Add("Request4", Request4);
            RequestMap.Add("Request5", Request5);
            RequestMap.Add("Request6", Request6);
            RequestMap.Add("Request7", Request7);
            RequestMap.Add("Request8", Request8);
            RequestMap.Add("Request9", Request9);
            RequestMap.Add("Request10", Request10);
        }


        private void setupHandledByMap()
        {
            HandledByMap.Add("HandledBy1", HandledBy1);
            HandledByMap.Add("HandledBy2", HandledBy2);
            HandledByMap.Add("HandledBy3", HandledBy3);
            HandledByMap.Add("HandledBy4", HandledBy4);
            HandledByMap.Add("HandledBy5", HandledBy5);
            HandledByMap.Add("HandledBy6", HandledBy6);
            HandledByMap.Add("HandledBy7", HandledBy7);
            HandledByMap.Add("HandledBy8", HandledBy8);
            HandledByMap.Add("HandledBy9", HandledBy9);
            HandledByMap.Add("HandledBy10", HandledBy10);
        }

        private void setupSuiviTimeStampMap()
        {
            SuiviTimeStampMap.Add("Timestamps1", TextBoxHandler.textboxArray(Timestamphhs1, Timestampmms1));
            SuiviTimeStampMap.Add("Timestamps2", TextBoxHandler.textboxArray(Timestamphhs2, Timestampmms2));
            SuiviTimeStampMap.Add("Timestamps3", TextBoxHandler.textboxArray(Timestamphhs3, Timestampmms3));
            SuiviTimeStampMap.Add("Timestamps4", TextBoxHandler.textboxArray(Timestamphhs4, Timestampmms4));
            SuiviTimeStampMap.Add("Timestamps5", TextBoxHandler.textboxArray(Timestamphhs5, Timestampmms5));
            SuiviTimeStampMap.Add("Timestamps6", TextBoxHandler.textboxArray(Timestamphhs6, Timestampmms6));
            SuiviTimeStampMap.Add("Timestamps7", TextBoxHandler.textboxArray(Timestamphhs7, Timestampmms7));
            SuiviTimeStampMap.Add("Timestamps8", TextBoxHandler.textboxArray(Timestamphhs8, Timestampmms8));
            SuiviTimeStampMap.Add("Timestamps9", TextBoxHandler.textboxArray(Timestamphhs9, Timestampmms9));
            SuiviTimeStampMap.Add("Timestamps10", TextBoxHandler.textboxArray(Timestamphhs10, Timestampmms10));
        }


        private void setupFaitTimeStampMap()
        {
            FaitTimeStampMap.Add("Timestampf1", TextBoxHandler.textboxArray(Timestamphhf1, Timestampmmf1));
            FaitTimeStampMap.Add("Timestampf2", TextBoxHandler.textboxArray(Timestamphhf2, Timestampmmf2));
            FaitTimeStampMap.Add("Timestampf3", TextBoxHandler.textboxArray(Timestamphhf3, Timestampmmf3));
            FaitTimeStampMap.Add("Timestampf4", TextBoxHandler.textboxArray(Timestamphhf4, Timestampmmf4));
            FaitTimeStampMap.Add("Timestampf5", TextBoxHandler.textboxArray(Timestamphhf5, Timestampmmf5));
            FaitTimeStampMap.Add("Timestampf6", TextBoxHandler.textboxArray(Timestamphhf6, Timestampmmf6));
            FaitTimeStampMap.Add("Timestampf7", TextBoxHandler.textboxArray(Timestamphhf7, Timestampmmf7));
            FaitTimeStampMap.Add("Timestampf8", TextBoxHandler.textboxArray(Timestamphhf8, Timestampmmf8));
            FaitTimeStampMap.Add("Timestampf9", TextBoxHandler.textboxArray(Timestamphhf9, Timestampmmf9));
            FaitTimeStampMap.Add("Timestampf10", TextBoxHandler.textboxArray(Timestamphhf10, Timestampmmf10));
        }

        /*
        private void setupTimesEquivalentKeyMap()
        {
            TimesEquivalentKeyMap.Add("Timestamps1", "Timestampf1");
            TimesEquivalentKeyMap.Add("Timestamps2", "Timestampf2");
            TimesEquivalentKeyMap.Add("Timestamps3", "Timestampf3");
            TimesEquivalentKeyMap.Add("Timestamps4", "Timestampf4");
            TimesEquivalentKeyMap.Add("Timestamps5", "Timestampf5");
            TimesEquivalentKeyMap.Add("Timestamps6", "Timestampf6");
            TimesEquivalentKeyMap.Add("Timestamps7", "Timestampf7");
            TimesEquivalentKeyMap.Add("Timestamps8", "Timestampf8");
            TimesEquivalentKeyMap.Add("Timestamps9", "Timestampf9");
            TimesEquivalentKeyMap.Add("Timestamps10", "Timestampf10");
        }
        */

        private void UpdateFollowUpInformation(int position, TextBox Clientbox, TextBox RequestBox, TextBox HandledByMapBox, 
                                                 TextBox TimestamphhsBox, TextBox TimestampmmsBox, TextBox TimestamphhfBox, TextBox TimestampmmfBox)
        {
            //Suivi timestamp
            int timestampshh = 0;
            int timestampsmm = 0;

            //fait timestamp
            int timestampfhh = 0;
            int timestampfmm = 0;

            if (!TimestamphhsBox.Text.Equals("hh") && !TimestampmmsBox.Text.Equals("mm"))
            {
                timestampshh = int.Parse(TimestamphhsBox.Text);
                timestampsmm = int.Parse(TimestampmmsBox.Text);
            }
            if (!TimestamphhfBox.Text.Equals("hh") && !TimestampmmfBox.Text.Equals("mm"))
            {
                timestampfhh = int.Parse(TimestamphhfBox.Text);
                timestampfmm = int.Parse(TimestampmmfBox.Text);
            }

            DateTime timestampSuivi = DateTime.Now;
            DateTime timestampFait = DateTime.Now;
            timestampSuivi = timestampSuivi.Date + new TimeSpan(timestampshh, timestampsmm, 0);
            timestampFait = timestampFait.Date + new TimeSpan(timestampfhh, timestampfmm, 0);

            FollowUpInfo.SetFollowUpInfo(position, new Request(Clientbox.Text, RequestBox.Text, HandledByMapBox.Text, timestampSuivi, timestampFait));

        }

        public void PersistencyUpdate()
        {
            if (!Client1.Text.Equals(""))
            {
                UpdateFollowUpInformation(1, Client1, Request1, HandledBy1, Timestamphhs1, Timestampmms1, Timestamphhf1, Timestampmmf1);
            }

            if (!Client1.Text.Equals(""))
            {
                UpdateFollowUpInformation(2, Client2, Request2, HandledBy2, Timestamphhs2, Timestampmms2, Timestamphhf2, Timestampmmf2);
            }

            if (!Client1.Text.Equals(""))
            {
                UpdateFollowUpInformation(3, Client3, Request3, HandledBy3, Timestamphhs3, Timestampmms3, Timestamphhf3, Timestampmmf3);
            }

            if (!Client1.Text.Equals(""))
            {
                UpdateFollowUpInformation(4, Client4, Request4, HandledBy4, Timestamphhs4, Timestampmms4, Timestamphhf4, Timestampmmf4);
            }

            if (!Client1.Text.Equals(""))
            {
                UpdateFollowUpInformation(5, Client5, Request5, HandledBy5, Timestamphhs5, Timestampmms5, Timestamphhf5, Timestampmmf5);
            }

            if (!Client1.Text.Equals(""))
            {
                UpdateFollowUpInformation(6, Client6, Request6, HandledBy6, Timestamphhs6, Timestampmms6, Timestamphhf6, Timestampmmf6);
            }

            if (!Client1.Text.Equals(""))
            {
                UpdateFollowUpInformation(7, Client7, Request7, HandledBy7, Timestamphhs7, Timestampmms7, Timestamphhf7, Timestampmmf7);
            }

            if (!Client1.Text.Equals(""))
            {
                UpdateFollowUpInformation(8, Client8, Request8, HandledBy8, Timestamphhs8, Timestampmms8, Timestamphhf8, Timestampmmf8);
            }

            if (!Client1.Text.Equals(""))
            {
                UpdateFollowUpInformation(9, Client9, Request9, HandledBy9, Timestamphhs9, Timestampmms9, Timestamphhf9, Timestampmmf9);
            }

            if (!Client1.Text.Equals(""))
            {
                UpdateFollowUpInformation(10, Client10, Request10, HandledBy10, Timestamphhs10, Timestampmms10, Timestamphhf10, Timestampmmf10);
            }

        }


        private void FollowUpInfo_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxHandler.GotFocus(sender, e);
        }

        private void TextBoxes_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxHandler.GotFocus(sender, e);
        }

        private void TextBoxes_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBoxHandler.LostFocus(sender, e);
        }

        public void TimeStampFait_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            TextBoxHandler.setNow(FaitTimeStampMap[bt.Name][0], FaitTimeStampMap[bt.Name][1]);
        }

        public void TimestampSuivi_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            TextBoxHandler.setNow(SuiviTimeStampMap[bt.Name][0], SuiviTimeStampMap[bt.Name][1]);
        }

        public void TimestampTime_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            TextBoxHandler.setNow(TimeStampMap[bt.Name][0], TimeStampMap[bt.Name][1]);
        }
    }

}
