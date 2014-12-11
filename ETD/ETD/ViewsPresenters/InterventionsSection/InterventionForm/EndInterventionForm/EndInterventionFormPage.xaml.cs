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

namespace ETD.ViewsPresenters.InterventionsSection.InterventionForm.EndInterventionForm
{
	/// <summary>
	/// Interaction logic for EndInterventionFormPage.xaml
	/// </summary>
	public partial class EndInterventionFormPage : Page
	{
		private InterventionFormPage interventionForm;
		private Intervention intervention;

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
			Endhh.Text = "" + DateTime.Now.Hour;
			if(DateTime.Now.Minute < 10)
			{
				Endmm.Text = "0" + DateTime.Now.Minute;
			}
			else
			{
				Endmm.Text = "" + DateTime.Now.Minute;
			}
			interventionForm.HideInterventionForm();
		}

		private void SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			ComboBoxItem item = (ComboBoxItem)comboBox.SelectedItem;
			if (item.Content.Equals("Hospital") || item.Content.Equals("Other"))
			{
				Grid.SetColumnSpan(ComboBoxBorder, 1);
				AdditionalInformation.Visibility = Visibility.Visible;
			}
			else
			{
				AdditionalInformation.Visibility = Visibility.Collapsed;
				Grid.SetColumnSpan(ComboBoxBorder, 2);
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
