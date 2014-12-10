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

		public EndInterventionFormPage(InterventionFormPage interventionForm)
		{
			InitializeComponent();
			this.interventionForm = interventionForm;
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
			Endmm.Text = "" + DateTime.Now.Minute;

			interventionForm.HideInterventionForm();
		}

		private void Call911(object sender, RoutedEventArgs e)
		{
			Call911hh.Text = "" + DateTime.Now.Hour;
			Call911mm.Text = "" + DateTime.Now.Minute;
		}

		private void FirstResponderArrival(object sender, RoutedEventArgs e)
		{
			FirstResponderCompany.Text = "SIM";
			FirstResponderArrivalhh.Text = "" + DateTime.Now.Hour;
			FirstResponderArrivalmm.Text = "" + DateTime.Now.Minute;
		}

		private void AmbulanceArrival(object sender, RoutedEventArgs e)
		{
			AmbulanceCompany.Text = "US";
			AmbulanceArrivalhh.Text = "" + DateTime.Now.Hour;
			AmbulanceArrivalmm.Text = "" + DateTime.Now.Minute;
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
	}
}
