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

namespace ETD.ViewsPresenters.InterventionsSection.InterventionForm.EndInterventionForm
{
	/// <summary>
	/// Interaction logic for EndInterventionFormPage.xaml
	/// </summary>
	public partial class EndInterventionFormPage : Page
	{
		private InterventionFormPage interventionForm;
		private Intervention intervention;
		private bool selectionChanged = false;

		//Constructor
		public EndInterventionFormPage(InterventionFormPage interventionForm, Intervention intervention)
		{
			InitializeComponent();
			this.interventionForm = interventionForm;
			this.intervention = intervention;
            FillForm(intervention);
		}

		//Setup initial form
        private void FillForm(Intervention intervention)
        {
            AdditionalInformation.Text = intervention.getConclusionAdditionalInfo();
         
            if (intervention.getConclusion() != null)
            {
                int index = 0;
                foreach (ComboBoxItem cb in ConclusionBox.Items)
                {
                    index++;
                    if (cb.Content.ToString() == intervention.getConclusion())
                    {
                        break;
                    }
                }
                ConclusionBox.SelectedIndex = index-1;
            }
            
            if (intervention.getConclusionTime().Hour != 0 && intervention.getConclusionTime().Minute != 0)
            {              
                TextBoxHandler.setTime(Endhh, Endmm, intervention.getConclusionTime().Hour, intervention.getConclusionTime().Minute);
            }
            if (intervention.getConclusion() == "911 called")
            {
                if (intervention.getCall911Time().Hour != 0 && intervention.getCall911Time().Minute != 0)
                {                  
                    TextBoxHandler.setTime(Call911hh, Call911mm, intervention.getCall911Time().Hour, intervention.getCall911Time().Minute);
                }
                if (intervention.getFirstResponderArrivalTime().Hour != 0 && intervention.getFirstResponderArrivalTime().Minute != 0)
                {
                    TextBoxHandler.setTime(FirstResponderArrivalhh, FirstResponderArrivalmm, intervention.getFirstResponderArrivalTime().Hour, intervention.getFirstResponderArrivalTime().Minute);
                }
                              
                if (intervention.getAmbulanceArrivalTime().Hour != 0 && intervention.getAmbulanceArrivalTime().Minute != 0)
                {            
                    TextBoxHandler.setTime(AmbulanceArrivalhh, AmbulanceArrivalmm, intervention.getAmbulanceArrivalTime().Hour, intervention.getAmbulanceArrivalTime().Minute);
                }

                AmbulanceCompany.Text = intervention.getAmbulanceCompany();
                AmbulanceVehicle.Text = intervention.getAmbulanceVehicle();
                FirstResponderCompany.Text = intervention.getFirstResponderCompany();
                FirstResponderVehicle.Text = intervention.getFirstResponderVehicle();
                MeetingPoint.Text = intervention.getMeetingPoint();     
            }
        }

		//Called When we get focus on text boxes
		private void TextBoxes_GotFocus(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.GotFocus(sender, e);
		}

		//Called When we lost focus on text boxes
		private void TextBoxes_LostFocus(object sender, RoutedEventArgs e)
		{
			
            try
            {
                DateTime concTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(Endhh.Text), Convert.ToInt32(Endmm.Text), 0);
				intervention.setConclusionTime(concTime);
			}
            catch { }
			intervention.setConclusionAdditionalInfo(AdditionalInformation.Text);
            intervention.setAmbulanceCompany(AmbulanceCompany.Text);
            intervention.setAmbulanceVehicle(AmbulanceVehicle.Text);
            intervention.setFirstResponderCompany(FirstResponderCompany.Text);
            intervention.setFirstResponderVehicle(FirstResponderVehicle.Text);
            intervention.setMeetingPoint(MeetingPoint.Text);
			TextBoxHandler.LostFocus(sender, e);
		}

		//Submitting end of intervention
		private void EndIntervention(object sender, RoutedEventArgs e)
		{
			try
			{
				int hh = int.Parse(Endhh.Text);
				int mm = int.Parse(Endmm.Text);
				DateTime endTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hh, mm, DateTime.Now.Second);
				int offset = (int)DateTime.Now.Subtract(endTime).TotalMinutes;
                
                List<Resource> resources = this.intervention.getResourceList();

				if (offset < 0)
				{
					MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_FutureTime);
				}
				else
				{
					ComboBoxItem conclusion = (ComboBoxItem)ConclusionBox.SelectedItem;

					if (Grid.GetColumnSpan(ComboBoxBorder) == 1 && TextBoxHandler.isDefaultText(AdditionalInformation))
					{
						MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_NoConclusion);
					}
					else
					{
						intervention.Completed();
                        intervention.setConclusionTime(endTime);
                        if (Grid.GetColumnSpan(ComboBoxBorder) == 1 )
                        {
                            intervention.setConclusionAdditionalInfo(AdditionalInformation.Text);
                        }
						interventionForm.CompleteIntervention(offset);
					}
				}
				intervention.ResourceModified();

			}
			catch (FormatException ex)
			{
				MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_InvalidTime);
			}
		}

		//Update fields 
		private void SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			//By default, for all selections, the ambulance form is hidden
			MainGrid.RowDefinitions[1].Height = new GridLength(0);
			MainGrid.RowDefinitions[2].Height = new GridLength(0);
			MainGrid.RowDefinitions[3].Height = new GridLength(0);
			MainGrid.RowDefinitions[4].Height = new GridLength(0);

			ComboBox comboBox = (ComboBox)sender;
			ComboBoxItem item = (ComboBoxItem)comboBox.SelectedItem;
            intervention.setConclusion(item.Content.ToString());
			if (item.Name.Equals("call911") || item.Name.Equals("other") || item.Name.Equals("doctor"))
			{
				Grid.SetColumnSpan(ComboBoxBorder, 1);
				AdditionalInformationBorder.Visibility = Visibility.Visible;
				if (item.Name.Equals("call911"))
				{
					AdditionalInformation.Text = intervention.getConclusionAdditionalInfo();
					//If the ambulance requires an ambulance, show the ambulance form
					MainGrid.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);
					MainGrid.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
					MainGrid.RowDefinitions[3].Height = new GridLength(1, GridUnitType.Star);
					MainGrid.RowDefinitions[4].Height = new GridLength(1, GridUnitType.Star);
				}
				else if (item.Name.Equals("other"))
				{
                    AdditionalInformation.Text = ETD.Properties.Resources.TextBox_AdditionalInformation_Conclusion;
				}
				else if (item.Name.Equals("doctor"))
				{
                    AdditionalInformation.Text = ETD.Properties.Resources.TextBox_AdditionalInformation_Hospital;
				}
				TextBoxHandler.ResetHandling(AdditionalInformation);
			}
			else
			{
				AdditionalInformationBorder.Visibility = Visibility.Collapsed;
				Grid.SetColumnSpan(ComboBoxBorder, 2);
			}
			selectionChanged = true;
		}
		//Set conclusion time of an intervention
		private void End_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Endhh, Endmm);
            try
            {
                int hh = int.Parse(Endhh.Text);
                int mm = int.Parse(Endmm.Text);
                DateTime concTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hh, mm, DateTime.Now.Second);
                intervention.setConclusionTime(concTime);
				intervention.ResourceModified();
            }
            catch { }
		}

		//Set 911 call time
		private void Call_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Call911hh, Call911mm);
            
            try
            {
                int hh = int.Parse(Call911hh.Text);
                int mm = int.Parse(Call911mm.Text);
                DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hh, mm, DateTime.Now.Second);
                intervention.setCall911Time(startTime);
				intervention.ResourceModified();
                int offset = (int)DateTime.Now.Subtract(startTime).TotalSeconds;
                if (offset < 0)
                {
                    MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_FutureTime);
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_InvalidTime);
            }
            
		}

		//Set first responder arrival time
		private void FirstResponder_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(FirstResponderArrivalhh, FirstResponderArrivalmm);

            try
            {
				if(intervention.getCall911Time() != DateTime.MinValue)
				{ 
					int hh = int.Parse(FirstResponderArrivalhh.Text);
					int mm = int.Parse(FirstResponderArrivalmm.Text);
					DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hh, mm, DateTime.Now.Second);
					intervention.setFirstResponderArrivalTime(startTime);
					int offset = (int)DateTime.Now.Subtract(startTime).TotalMinutes;
					if (offset < 0)
					{
						MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_FutureTime);
					}
					intervention.ResourceModified();
				}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_InvalidTime);
            }
            
		}

		//Set ambulance arrival time
		private void Ambulance_Click(object sender, RoutedEventArgs e)
		{
            TextBoxHandler.setNow(AmbulanceArrivalhh, AmbulanceArrivalmm);              
            try
            {
				if(intervention.getCall911Time() != DateTime.MinValue)
				{ 
					int hh = int.Parse(AmbulanceArrivalhh.Text);
					int mm = int.Parse(AmbulanceArrivalmm.Text);
					DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hh, mm, DateTime.Now.Second);
					intervention.setAmbulanceArrivalTime(startTime);
					int offset = (int)DateTime.Now.Subtract(startTime).TotalMinutes;
					if (offset < 0)
					{
						MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_FutureTime);
					}
					intervention.ResourceModified();
				}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_InvalidTime);
            }
            
		}
	}
}
