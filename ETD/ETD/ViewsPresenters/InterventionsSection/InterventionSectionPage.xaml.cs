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
using ETD.ViewsPresenters.InterventionsSection.InterventionForm;
using ETD.Models;

namespace ETD.ViewsPresenters.InterventionsSection
{
	/// <summary>
	/// Interaction logic for InterventionSectionPage.xaml
	/// </summary>
	public partial class InterventionSectionPage : Page
	{
		private MainWindow mainWindow;
		private double interventionSizeDifference = 0;

		public InterventionSectionPage(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
		}

		//Adjusting the intervention section width
		public void setInterventionsSectionWidth(Border InterventionsSection)
		{
			/*if (interventionSizeDifference == 0)
			{
				//By getting the position of the button, we're able to now how much the label, and button are occupying space
				var position = CreateInterventionButton.PointToScreen(new Point(0, 0));
				interventionSizeDifference = position.X;
			}*/
			Scroller.MaxWidth = InterventionsSection.ActualWidth - InterventionsLabel.ActualWidth - CreateInterventionButton.ActualWidth - 10;
		}

		private void CreateInterventionForm(object sender, RoutedEventArgs e)
		{
			Intervention intervention = new Intervention();
			mainWindow.CreateInterventionPin(intervention.getInterventionNumber());

			Frame frame = new Frame();
			InterventionFormPage form = new InterventionFormPage(this, intervention);
			frame.Content = form;
			frame.Name = "Intervention_" + form.getInterventionNumber();
			frame.Tag = "Ongoing";
			if(!InterventionFilterLabel.Content.Equals("Ongoing"))
			{
				frame.Visibility = Visibility.Collapsed;
			}
			InterventionsList.Children.Add(frame);
		}

		//Hiding intervention form after completion
		public void HideInterventionForm(int interventionNumber)
		{
			foreach(Frame frame in InterventionsList.Children)
			{
				if(frame.Name.Equals("Intervention_" + interventionNumber))
				{
					frame.Tag = "Completed";
					frame.Visibility = Visibility.Collapsed;
				}
			}
		}

		//Showing and hiding the appropriate intervention forms
		private void FilterInterventions(object sender, RoutedEventArgs e)
		{
			MenuItem mi = (MenuItem)sender;
			ContextMenu cm = (ContextMenu)mi.Parent;

			foreach(MenuItem item in cm.Items)
			{
				if(item.IsChecked)
				{
					item.IsChecked = false;
				}
			}
			mi.IsChecked = true;
			InterventionFilterLabel.Content = mi.Tag;

			foreach (Frame frame in InterventionsList.Children)
			{
				if(frame.Tag.Equals(mi.Tag))
				{
					frame.Visibility = Visibility.Visible;
				}
				else
				{
					frame.Visibility = Visibility.Collapsed;
				}
			}
		}
	}
}
