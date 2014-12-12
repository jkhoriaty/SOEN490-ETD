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
					ComboBoxItem conclusion = (ComboBoxItem)Conclusion.SelectedItem;
					String conc = "" + conclusion.Content;

					if ((conc.Equals("Hospital") || conc.Equals("Other")) && AdditionalInformation.Text.Equals(""))
					{
						throw new Exception();
					}
					else
					{
						interventionForm.HideInterventionForm(offset);
					}
				}

			}
			catch (FormatException ex)
			{
				MessageBox.Show("The text inserted in the time boxes is not valid");
			}
			catch(Exception ex2)
			{
				MessageBox.Show("No conclusion is set!");
			}
		}

		private void SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			ComboBoxItem item = (ComboBoxItem)comboBox.SelectedItem;
			if (item.Content.Equals("Hospital") || item.Content.Equals("Other"))
			{
				Grid.SetColumnSpan(ComboBoxBorder, 1);
				AdditionalInformationBorder.Visibility = Visibility.Visible;
			}
			else
			{
				AdditionalInformationBorder.Visibility = Visibility.Collapsed;
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
