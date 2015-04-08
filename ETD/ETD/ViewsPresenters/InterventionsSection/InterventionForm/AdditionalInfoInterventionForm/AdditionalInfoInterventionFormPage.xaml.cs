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

namespace ETD.ViewsPresenters.InterventionsSection.InterventionForm.AdditionalInfoInterventionForm
{
	/// <summary>
	/// Interaction logic for AdditionalInfoInterventionFormPage.xaml
	/// </summary>
	public partial class AdditionalInfoInterventionFormPage : Page
	{
		private InterventionFormPage interventionForm;
		private Intervention intervention;
		private Dictionary<String, TextBox> additionnalInformationMap = new Dictionary<String, TextBox>();
		private Dictionary<String, TextBox[]> timestampMap = new Dictionary<String, TextBox[]>();
		private Dictionary<String, String> equivalentKeyMap = new Dictionary<String, String>();
        private bool changed;

		//Constructor
		public AdditionalInfoInterventionFormPage(InterventionFormPage interventionForm, Intervention intervention)
		{
			InitializeComponent();
            changed = false;
			this.interventionForm = interventionForm;
			this.intervention = intervention;

			setupAdditionnalInformationMap();
			setupTimestampMap();
			setupEquivalentKeyMap();
            FillForm();
		}

		//Set up additional info list
		private void setupAdditionnalInformationMap()
		{
			additionnalInformationMap.Add("AdditionalInformation0", AdditionalInformation0);
			additionnalInformationMap.Add("AdditionalInformation1", AdditionalInformation1);
			additionnalInformationMap.Add("AdditionalInformation2", AdditionalInformation2);
			additionnalInformationMap.Add("AdditionalInformation3", AdditionalInformation3);
			additionnalInformationMap.Add("AdditionalInformation4", AdditionalInformation4);
			additionnalInformationMap.Add("AdditionalInformation5", AdditionalInformation5);
			additionnalInformationMap.Add("AdditionalInformation6", AdditionalInformation6);
			additionnalInformationMap.Add("AdditionalInformation7", AdditionalInformation7);
			additionnalInformationMap.Add("AdditionalInformation8", AdditionalInformation8);
			additionnalInformationMap.Add("AdditionalInformation9", AdditionalInformation9);
		}

		//Setup additional info timestamp list
		private void setupTimestampMap()
		{
			timestampMap.Add("Timestamp0", TextBoxHandler.textboxArray(Timestamphh0, Timestampmm0));
			timestampMap.Add("Timestamp1", TextBoxHandler.textboxArray(Timestamphh1, Timestampmm1));
			timestampMap.Add("Timestamp2", TextBoxHandler.textboxArray(Timestamphh2, Timestampmm2));
			timestampMap.Add("Timestamp3", TextBoxHandler.textboxArray(Timestamphh3, Timestampmm3));
			timestampMap.Add("Timestamp4", TextBoxHandler.textboxArray(Timestamphh4, Timestampmm4));
			timestampMap.Add("Timestamp5", TextBoxHandler.textboxArray(Timestamphh5, Timestampmm5));
			timestampMap.Add("Timestamp6", TextBoxHandler.textboxArray(Timestamphh6, Timestampmm6));
			timestampMap.Add("Timestamp7", TextBoxHandler.textboxArray(Timestamphh7, Timestampmm7));
			timestampMap.Add("Timestamp8", TextBoxHandler.textboxArray(Timestamphh8, Timestampmm8));
			timestampMap.Add("Timestamp9", TextBoxHandler.textboxArray(Timestamphh9, Timestampmm9));
		}

		//Setup equivalent key map list
		private void setupEquivalentKeyMap()
		{
			equivalentKeyMap.Add("AdditionalInformation0", "Timestamp0");
			equivalentKeyMap.Add("AdditionalInformation1", "Timestamp1");
			equivalentKeyMap.Add("AdditionalInformation2", "Timestamp2");
			equivalentKeyMap.Add("AdditionalInformation3", "Timestamp3");
			equivalentKeyMap.Add("AdditionalInformation4", "Timestamp4");
			equivalentKeyMap.Add("AdditionalInformation5", "Timestamp5");
			equivalentKeyMap.Add("AdditionalInformation6", "Timestamp6");
			equivalentKeyMap.Add("AdditionalInformation7", "Timestamp7");
			equivalentKeyMap.Add("AdditionalInformation8", "Timestamp8");
			equivalentKeyMap.Add("AdditionalInformation9", "Timestamp9");
		}

		//Setup initial form
        private void FillForm()
        {
            InterventionAdditionalInfo[] interventionAI = intervention.getAllAdditionalInfo();

            for(int i = 0; i < interventionAI.Length; i++)
            {
                if(interventionAI[i] != null)
                {
                    additionnalInformationMap["AdditionalInformation" + i].Text = interventionAI[i].getInformation();
                    DateTime time = interventionAI[i].getTimestamp();
                    timestampMap["Timestamp" + i][0].Text = time.Hour.ToString();
                    timestampMap["Timestamp" + i][1].Text = time.Minute.ToString();
                }
            }
            
        }


		//Update form when fields are modified
		private void UpdateAdditionalInformation(int position, TextBox AdditionalInformation, TextBox TimestamphhBox, TextBox TimestampmmBox)
		{
			int timestamphh = 0;
			int timestampmm = 0;
			if (!TimestamphhBox.Text.Equals("hh") && !TimestampmmBox.Text.Equals("mm"))
			{
				timestamphh = int.Parse(TimestamphhBox.Text);
				timestampmm = int.Parse(TimestampmmBox.Text);
			}
			DateTime timestamp = DateTime.Now;
			timestamp = timestamp.Date + new TimeSpan(timestamphh, timestampmm, 0);

			intervention.setAdditionalInfo(position, new InterventionAdditionalInfo(intervention, AdditionalInformation.Text, timestamp));
		}

		//Called on additional info text box lost focus
		private void AdditionalInformation_GotFocus(object sender, RoutedEventArgs e)
		{
			TextBox tb = (TextBox)sender;
            
			if(tb.Text.Equals(""))
			{
				TextBoxHandler.setNow(timestampMap[equivalentKeyMap[tb.Name]][0], timestampMap[equivalentKeyMap[tb.Name]][1]);
			}
		}

		//Called on text box got focus
		private void TextBoxes_GotFocus(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.GotFocus(sender, e);
		}
		//Called on text box lost focus
		private void TextBoxes_LostFocus(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.LostFocus(sender, e);
		}
		//Set up time to current time
		public void Timestamp_Click(object sender, RoutedEventArgs e)
		{
			Button bt = (Button)sender;
			TextBoxHandler.setNow(timestampMap[bt.Name][0], timestampMap[bt.Name][1]);
		}
		//Called when the additional textbox info was changed
        private void AdditionalInformation_TextChanged(object sender, TextChangedEventArgs e)
        {
            changed = true;
        }
		//Called we lose focus on  additional info textbox
        private void AdditionalInformation_LostFocus(object sender, RoutedEventArgs e)
        {
            if(changed)
            {
                TextBox tb = (TextBox)sender;
                int index = int.Parse(tb.Name.ToCharArray()[tb.Name.Length - 1].ToString());
                UpdateAdditionalInformation(index, tb, timestampMap[equivalentKeyMap[tb.Name]][0], timestampMap[equivalentKeyMap[tb.Name]][1]);
                changed = false;
            }
        }
	}
}
