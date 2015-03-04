﻿using System;
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

		public EndInterventionFormPage(InterventionFormPage interventionForm, Intervention intervention)
		{
			InitializeComponent();
			this.interventionForm = interventionForm;
			this.intervention = intervention;
            FillForm(intervention);
		}

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
            if (intervention.getConclusion() == "911")
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


		private void TextBoxes_GotFocus(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.GotFocus(sender, e);
		}

		private void TextBoxes_LostFocus(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.LostFocus(sender, e);
            try
            {
                DateTime concTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(Endhh.Text), Convert.ToInt32(Endmm.Text), 0);
                intervention.setConclusionTime(concTime);
                intervention.setAmbulanceCompany(AmbulanceCompany.Text);
                intervention.setAmbulanceVehicle(AmbulanceVehicle.Text);
                intervention.setFirstResponderCompany(FirstResponderCompany.Text);
                intervention.setFirstResponderVehicle(FirstResponderVehicle.Text);
                intervention.setMeetingPoint(MeetingPoint.Text);
            }
            catch { }
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
                            
                /*for (int i = 0; i < TeamsSection.TeamForm.TeamFormPage.activeTeamsList.Count; i++)
                {
                    try
                    {
                        //Team.teamList[TeamsSection.TeamForm.TeamFormPage.activeTeamsList[i]].getTeamGrid().ChangeStatus("unavailable");
                    }
                    catch (ArgumentNullException)
                    {

                    }
                   
                }*/

				if (offset < 0)
				{
					MessageBox.Show("The time inserted is in the future!");
				}
				else
				{
					ComboBoxItem conclusion = (ComboBoxItem)ConclusionBox.SelectedItem;

					if (Grid.GetColumnSpan(ComboBoxBorder) == 1 && TextBoxHandler.isDefaultText(AdditionalInformation))
					{
						MessageBox.Show("No conclusion is set!");
					}
					else
					{
						intervention.Completed();
                        intervention.setConclusionTime(endTime);
						interventionForm.CompleteIntervention(offset);
					}
				}

			}
			catch (FormatException ex)
			{
				MessageBox.Show("The text inserted in the time boxes is not valid");
			}
		}

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
					AdditionalInformation.Text = "Hospital?";

					//If the ambulance requires an ambulance, show the ambulance form
					MainGrid.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);
					MainGrid.RowDefinitions[2].Height = new GridLength(1, GridUnitType.Star);
					MainGrid.RowDefinitions[3].Height = new GridLength(1, GridUnitType.Star);
					MainGrid.RowDefinitions[4].Height = new GridLength(1, GridUnitType.Star);
				}
				else if (item.Name.Equals("other"))
				{
					AdditionalInformation.Text = "Conclusion?";
				}
				else if (item.Name.Equals("doctor"))
				{
					AdditionalInformation.Text = "Hospital?";
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

		public void PersistencyUpdate()
		{
			String conclusion;
			if (ConclusionBox.SelectedIndex == -1)
			{
				conclusion = "notSet";
			}
			else
			{
				conclusion = ConclusionBox.SelectedItem.ToString();
			}
			intervention.setConclusion(conclusion);

			int conclusionhh = 0;
			int conclusionmm = 0;
			if(!Endhh.Text.Equals("hh") && !Endmm.Text.Equals("mm"))
			{
				conclusionhh = int.Parse(Endhh.Text);
				conclusionmm = int.Parse(Endmm.Text);
			}
			DateTime conclusionTime = DateTime.Now;
			conclusionTime = conclusionTime.Date + new TimeSpan(conclusionhh, conclusionmm, conclusionTime.Second);
			intervention.setConclusionTime(conclusionTime);

			if(selectionChanged)
			{
				ComboBoxItem item = (ComboBoxItem)ConclusionBox.SelectedItem;
				if (item.Name.Equals("call911"))
				{
					intervention.setConclusionAdditionalInfo(AdditionalInformation.Text);
					int call911hh = 0;
					int call911mm = 0;
					if (!Call911hh.Text.Equals("hh") && !Call911mm.Text.Equals("mm"))
					{
						call911hh = int.Parse(Call911hh.Text);
						call911mm = int.Parse(Call911mm.Text);
					}
					DateTime call911Time = DateTime.Now;
					call911Time = call911Time.Date + new TimeSpan(call911hh, call911mm, 0);
					intervention.setCall911Time(call911Time);

                    intervention.setMeetingPoint(MeetingPoint.Text);

					intervention.setFirstResponderCompany(FirstResponderCompany.Text);
					intervention.setFirstResponderVehicle(FirstResponderVehicle.Text);
					int firstResponderArrivalhh = 0;
					int firstResponderArrivalmm = 0;
					if (!FirstResponderArrivalhh.Text.Equals("hh") && !FirstResponderArrivalmm.Text.Equals("mm"))
					{
						firstResponderArrivalhh = int.Parse(FirstResponderArrivalhh.Text);
						firstResponderArrivalmm = int.Parse(FirstResponderArrivalmm.Text);
					}
					DateTime firstResponderArrival = DateTime.Now;
					firstResponderArrival = firstResponderArrival.Date + new TimeSpan(firstResponderArrivalhh, firstResponderArrivalmm, 0);
					intervention.setFirstResponderArrivalTime(firstResponderArrival);

					intervention.setAmbulanceCompany(AmbulanceCompany.Text);
					intervention.setAmbulanceVehicle(AmbulanceVehicle.Text);
					int ambulanceArrivalhh = 0;
					int ambulanceArrivalmm = 0;
					if (!AmbulanceArrivalhh.Text.Equals("hh") && !AmbulanceArrivalmm.Text.Equals("mm"))
					{
						ambulanceArrivalhh = int.Parse(AmbulanceArrivalhh.Text);
						ambulanceArrivalmm = int.Parse(AmbulanceArrivalmm.Text);
					}
					DateTime ambulanceArrivalTime = DateTime.Now;
					ambulanceArrivalTime = ambulanceArrivalTime.Date + new TimeSpan(ambulanceArrivalhh, ambulanceArrivalmm, 0);
					intervention.setAmbulanceArrivalTime(ambulanceArrivalTime);
				}
				else if (item.Name.Equals("other"))
				{
					intervention.setConclusionAdditionalInfo(AdditionalInformation.Text);
				}
				else if (item.Name.Equals("doctor"))
				{
					if(!TextBoxHandler.isDefaultText(AdditionalInformation))
					{
						intervention.setConclusionAdditionalInfo(AdditionalInformation.Text);
					}
				}
			}
			
		}

		private void End_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Endhh, Endmm);
            try
            {
                int hh = int.Parse(Endhh.Text);
                int mm = int.Parse(Endmm.Text);
                DateTime concTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hh, mm, DateTime.Now.Second);
                intervention.setConclusionTime(concTime);
            }
            catch { }
		}

		private void Call_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Call911hh, Call911mm);
            
            try
            {
                int hh = int.Parse(Call911hh.Text);
                int mm = int.Parse(Call911mm.Text);
                DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hh, mm, DateTime.Now.Second);
                intervention.setCall911Time(startTime);
                int offset = (int)DateTime.Now.Subtract(startTime).TotalSeconds;
                if (offset < 0)
                {
                    MessageBox.Show("The time inserted is in the future!");
                }
                else
                {
                    //interventionForm.CreateTimer(11, "911", "Call", offset);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("The text inserted in the time boxes is not valid");
            }
            
		}

		private void FirstResponder_Click(object sender, RoutedEventArgs e)
		{
            if (interventionForm.IsTimerRunning(11))
            {
                TextBoxHandler.setNow(FirstResponderArrivalhh, FirstResponderArrivalmm);

                try
                {
                    int hh = int.Parse(FirstResponderArrivalhh.Text);
                    int mm = int.Parse(FirstResponderArrivalmm.Text);
                    DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hh, mm, DateTime.Now.Second);
                    intervention.setFirstResponderArrivalTime(startTime);
                    int offset = (int)DateTime.Now.Subtract(startTime).TotalMinutes;
                    if (offset < 0)
                    {
                        MessageBox.Show("The time inserted is in the future!");
                    }
                    else
                    {
                        //interventionForm.RenameTimer(11, "911", "First Responder"); TODO
                        //interventionForm.CloneTimer(12, "911", "Ambulance", offset, 11); TODO
                        //interventionForm.StopTimer(11, offset); TODO
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("The text inserted in the time boxes is not valid");
                }
            }
		}

		private void Ambulance_Click(object sender, RoutedEventArgs e)
		{
            if (interventionForm.IsTimerRunning(11) || interventionForm.IsTimerRunning(12))
            {
                TextBoxHandler.setNow(AmbulanceArrivalhh, AmbulanceArrivalmm);              
                try
                {
                    int hh = int.Parse(AmbulanceArrivalhh.Text);
                    int mm = int.Parse(AmbulanceArrivalmm.Text);
                    DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hh, mm, DateTime.Now.Second);
                    intervention.setAmbulanceArrivalTime(startTime);
                    int offset = (int)DateTime.Now.Subtract(startTime).TotalMinutes;
                    if (offset < 0)
                    {
                        MessageBox.Show("The time inserted is in the future!");
                    }
                    else
                    {
						if (!interventionForm.IsTimerRunning(12))
						{
							//interventionForm.RenameTimer(11, "911", "Ambulance"); TODO
							//interventionForm.StopTimer(11, offset); TODO
						}
						else
						{
							//interventionForm.StopTimer(12, offset); TODO
						}

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("The text inserted in the time boxes is not valid");
                }
            }
		}
	}
}
