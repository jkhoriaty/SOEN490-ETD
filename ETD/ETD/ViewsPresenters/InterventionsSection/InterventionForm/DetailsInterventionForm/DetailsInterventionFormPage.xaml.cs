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

		public DetailsInterventionFormPage(InterventionFormPage interventionForm)
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

		private void SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBox comboBox = (ComboBox)sender;
			ComboBoxItem item = (ComboBoxItem)comboBox.SelectedItem;
			if (item.Content.Equals("Other"))
			{
				Grid.SetColumnSpan(complaint, 1);
				AdditionalInformation.Visibility = Visibility.Visible;
			}
			else
			{
				AdditionalInformation.Visibility = Visibility.Collapsed;
				Grid.SetColumnSpan(complaint, 2);
			}
		}
	}
}
