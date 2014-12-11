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
using ETD.Models;

namespace ETD.ViewsPresenters.InterventionsSection.InterventionForm.DetailsInterventionForm
{
	/// <summary>
	/// Interaction logic for DetailsInterventionFormPage.xaml
	/// </summary>
	public partial class DetailsInterventionFormPage : Page
	{
		private InterventionFormPage interventionForm;
		private Intervention intervention;

		public DetailsInterventionFormPage(InterventionFormPage interventionForm, Intervention intervention)
		{
			InitializeComponent();
			this.interventionForm = interventionForm;
			this.intervention = intervention;
			TextBoxHandler.setNow(Callhh, Callmm);
		}

		private void TextBoxes_GotFocus(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.GotFocus(sender, e);
		}

		private void TextBoxes_LostFocus(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.LostFocus(sender, e);
		}

		private void SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			ComboBoxItem item = (ComboBoxItem)comboBox.SelectedItem;
			if (item.Content.Equals("Other"))
			{
				Grid.SetColumnSpan(Complaint, 1);
				OtherChiefComplaint.Visibility = Visibility.Visible;
			}
			else
			{
				OtherChiefComplaint.Visibility = Visibility.Collapsed;
				Grid.SetColumnSpan(Complaint, 2);
			}
		}

		public void PersistencyUpdate()
		{
			try
			{
				DateTime callTime = intervention.getTimeOfCall();
				int hours = int.Parse(Callhh.Text);
				int minutes = int.Parse(Callmm.Text);
				callTime = callTime.Date + (new TimeSpan(hours, minutes, 0));
				intervention.setTimeOfCall(callTime);
			}
			catch (Exception e) { }

			if(!TextBoxHandler.isDefaultText(CallerName))
			{
				intervention.setCallerName(CallerName.Text);
			}
			if(!TextBoxHandler.isDefaultText(Location))
			{
				intervention.setLocation(Location.Text);
			}
			if (!TextBoxHandler.isDefaultText(NatureOfCall))
			{
				intervention.setNatureOfCall(NatureOfCall.Text);
			}

			try
			{
				ComboBoxItem priorityItem = (ComboBoxItem)Priority.SelectedItem;
				int code = int.Parse("" + priorityItem.Content);
				intervention.setCode(code);
			}
			catch (Exception e) { }

			try
			{
				ComboBoxItem genderItem = (ComboBoxItem)Gender.SelectedItem;
				intervention.setGender("" + genderItem.Content);
			}
			catch (Exception e) { }

			intervention.setAge("" + Age.Text);

			try
			{
				ComboBoxItem chiefComplaint = (ComboBoxItem)Complaint.SelectedItem;
				String complaint = "" + chiefComplaint.Content;
				intervention.setChiefComplaint(complaint);
				if(complaint.Equals("Other"))
				{
					intervention.setOtherChiefComplaint(OtherChiefComplaint.Text);
				}
			}
			catch (Exception e) { }
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Callhh, Callmm);
		}
	}
}
