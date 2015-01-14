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
using ETD.Models.Objects;

namespace ETD.ViewsPresenters.InterventionsSection
{
	/// <summary>
	/// Interaction logic for InterventionSectionPage.xaml
	/// </summary>
	public partial class InterventionSectionPage : Page
	{
		private MainWindow mainWindow;

		public InterventionSectionPage(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
		}

		//Adjusting the intervention section width
		public void setInterventionsSectionWidth(Border InterventionsSection)
		{
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
		public void CompleteIntervention(int interventionNumber)
		{
			//Setting the intervention as completed and hiding it so it doesn't show in the ongoing intervention list
			foreach(Frame frame in InterventionsList.Children)
			{
				if(frame.Name.Equals("Intervention_" + interventionNumber))
				{
					frame.Tag = "Completed";
					frame.Visibility = Visibility.Collapsed;
				}
			}

			//Deleting the intervention pin
			mainWindow.DeletePin("intervention_" + interventionNumber);
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

		private InterventionFormPage getInterventionPage(String interventionName)
		{
			foreach (Frame intervention in InterventionsList.Children)
			{
				if (intervention.Name.Equals(interventionName))
				{
					InterventionFormPage interventionFormPage = (InterventionFormPage)intervention.Content;
					return interventionFormPage;
				}
			}
			return null;
		}

		internal void AddResource(String teamName, String interventionName)
		{
			getInterventionPage(interventionName).AddResource(teamName);
		}

		internal void ReportArrival(String teamName, String interventionName)
		{
			getInterventionPage(interventionName).ReportArrival(teamName);
		}

        internal void ReportArrived(InterventionFormPage caller, int rowNumber)
        {
            mainWindow.ReportArrived(getInterventionName(caller), rowNumber);
        }
        
		public static void setInterventionDeadline(int deadline)
		{
			InterventionFormPage.setInterventionDeadline(deadline);
		}

		public static void setMovingDeadline(int deadline)
		{
			InterventionFormPage.setMovingDeadline(deadline);
		}
		
        private string getInterventionName(InterventionFormPage caller)
        {
            foreach (Frame intervention in InterventionsList.Children)
            {
                if (intervention.Content.Equals(caller))
                {
                    return intervention.Name;
                }
            }
            return null;
        }
	}
}
