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

namespace ETD.ViewsPresenters.FollowUpSection
{
    /// <summary>
    /// Interaction logic for FollowUpSectionForm.xaml
    /// </summary>
    public partial class FollowUpSectionForm : Page
    {
        private FollowUpSectionForm FollowUpPage;
        private Request FollowUpInfo;
        private Dictionary<String, TextBox> ClientMap = new Dictionary<String, TextBox>();
        private Dictionary<String, TextBox> DemandeMap = new Dictionary<String, TextBox>();
        private Dictionary<String, TextBox> PriseEnChargeMap = new Dictionary<String, TextBox>();
        private Dictionary<String, TextBox[]> SuiviTimeStampMap = new Dictionary<String, TextBox[]>();
        private Dictionary<String, TextBox[]> FaitTimeStampMap = new Dictionary<String, TextBox[]>();
        private Dictionary<String, String> TimesEquivalentKeyMap = new Dictionary<String, String>();



        public FollowUpSectionForm(FollowUpSectionForm FollowUpPage)
        {
            InitializeComponent();
            this.FollowUpPage = FollowUpPage;

            setupClientMap();
            setupDemandeMap();
            setupPriseEnChargeMap();

            setupSuiviTimeStampMap();
            setupTimesEquivalentKeyMap();
            setupFaitTimeStampMap();

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

        private void setupDemandeMap()
        {
            DemandeMap.Add("Demande1", Demande1);
            DemandeMap.Add("Demande2", Demande2);
            DemandeMap.Add("Demande3", Demande3);
            DemandeMap.Add("Demande4", Demande4);
            DemandeMap.Add("Demande5", Demande5);
            DemandeMap.Add("Demande6", Demande6);
            DemandeMap.Add("Demande7", Demande7);
            DemandeMap.Add("Demande8", Demande8);
            DemandeMap.Add("Demande9", Demande9);
            DemandeMap.Add("Demande10", Demande10);
        }


        private void setupPriseEnChargeMap()
        {
            PriseEnChargeMap.Add("PriseEnCharge1", PriseEnCharge1);
            PriseEnChargeMap.Add("PriseEnCharge2", PriseEnCharge2);
            PriseEnChargeMap.Add("PriseEnCharge3", PriseEnCharge3);
            PriseEnChargeMap.Add("PriseEnCharge4", PriseEnCharge4);
            PriseEnChargeMap.Add("PriseEnCharge5", PriseEnCharge5);
            PriseEnChargeMap.Add("PriseEnCharge6", PriseEnCharge6);
            PriseEnChargeMap.Add("PriseEnCharge7", PriseEnCharge7);
            PriseEnChargeMap.Add("PriseEnCharge8", PriseEnCharge8);
            PriseEnChargeMap.Add("PriseEnCharge9", PriseEnCharge9);
            PriseEnChargeMap.Add("PriseEnCharge10", PriseEnCharge10);
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

        private void UpdateAdditionalInformation(int position, TextBox Clientbox, TextBox DemandeBox, TextBox PriseEnChargeMapBox, 
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

            FollowUpInfo.SetFollowUpInfo(position, new Request(Clientbox.Text, DemandeBox.Text, PriseEnChargeMapBox.Text, timestampSuivi, timestampFait));

        }

        public void PersistencyUpdate()
        {
            if (!Client1.Text.Equals(""))
            {
                UpdateAdditionalInformation(1, Client1, Demande1, PriseEnCharge1, Timestamphhs1, Timestampmms1, Timestamphhf1, Timestampmmf1);
            }

            if (!Client1.Text.Equals(""))
            {
                UpdateAdditionalInformation(2, Client2, Demande2, PriseEnCharge2, Timestamphhs2, Timestampmms2, Timestamphhf2, Timestampmmf2);
            }

            if (!Client1.Text.Equals(""))
            {
                UpdateAdditionalInformation(3, Client3, Demande3, PriseEnCharge3, Timestamphhs3, Timestampmms3, Timestamphhf3, Timestampmmf3);
            }

            if (!Client1.Text.Equals(""))
            {
                UpdateAdditionalInformation(4, Client4, Demande4, PriseEnCharge4, Timestamphhs4, Timestampmms4, Timestamphhf4, Timestampmmf4);
            }

            if (!Client1.Text.Equals(""))
            {
                UpdateAdditionalInformation(5, Client5, Demande5, PriseEnCharge5, Timestamphhs5, Timestampmms5, Timestamphhf5, Timestampmmf5);
            }

            if (!Client1.Text.Equals(""))
            {
                UpdateAdditionalInformation(6, Client6, Demande6, PriseEnCharge6, Timestamphhs6, Timestampmms6, Timestamphhf6, Timestampmmf6);
            }

            if (!Client1.Text.Equals(""))
            {
                UpdateAdditionalInformation(7, Client7, Demande7, PriseEnCharge7, Timestamphhs7, Timestampmms7, Timestamphhf7, Timestampmmf7);
            }

            if (!Client1.Text.Equals(""))
            {
                UpdateAdditionalInformation(8, Client8, Demande8, PriseEnCharge8, Timestamphhs8, Timestampmms8, Timestamphhf8, Timestampmmf8);
            }

            if (!Client1.Text.Equals(""))
            {
                UpdateAdditionalInformation(9, Client9, Demande9, PriseEnCharge9, Timestamphhs9, Timestampmms9, Timestamphhf9, Timestampmmf9);
            }

            if (!Client1.Text.Equals(""))
            {
                UpdateAdditionalInformation(10, Client10, Demande10, PriseEnCharge10, Timestamphhs10, Timestampmms10, Timestamphhf10, Timestampmmf10);
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

    }

}
