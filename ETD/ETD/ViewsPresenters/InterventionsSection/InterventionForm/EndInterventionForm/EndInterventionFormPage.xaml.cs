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
using ETD.Models.Services;

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
		}

		private void TextBoxes_GotFocus(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.GotFocus(sender, e);
		}

		private void TextBoxes_LostFocus(object sender, RoutedEventArgs e)
		{
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
				if (offset < 0)
				{
					MessageBox.Show("The time inserted is in the future!");
				}
				else
				{
					ComboBoxItem conclusion = (ComboBoxItem)ConclusionBox.SelectedItem;

					if ((conclusion.Name.Equals("call911") || conclusion.Name.Equals("other")) && AdditionalInformation.Text.Equals(""))
					{
						throw new Exception();
					}
					else
					{
						interventionForm.CompleteIntervention(offset);
					}
				}

			}
			catch (FormatException ex)
			{
				MessageBox.Show("The text inserted in the time boxes is not valid");
			}
			catch(Exception ex)
			{
				MessageBox.Show("No conclusion is set!");
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
			Conclusions conclusion;
			if (ConclusionBox.SelectedIndex == -1)
			{
				conclusion = (Conclusions)8;
			}
			else
			{
				conclusion = (Conclusions)ConclusionBox.SelectedIndex;
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
			conclusionTime = conclusionTime.Date + new TimeSpan(conclusionhh, conclusionmm, 0);
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
		}

		private void Call_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Call911hh, Call911mm);
		}

		private void FirstResponder_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(FirstResponderArrivalhh, FirstResponderArrivalmm);
		}

		private void Ambulance_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(AmbulanceArrivalhh, AmbulanceArrivalmm);
		}
	}
}
