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
using System.Threading;
using System.Windows.Threading;
using ETD.Models.ArchitecturalObjects;
using ETD.Services;

namespace ETD.ViewsPresenters.InterventionsSection
{
	/// <summary>
	/// Interaction logic for InterventionSectionPage.xaml
	/// </summary>
	public partial class InterventionSectionPage : Page, Observer
	{
		private MainWindow mainWindow;

		public InterventionSectionPage(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;

            Observable.RegisterClassObserver(typeof(Intervention), this);
		}

		//Adjusting the intervention section width
		public void setInterventionsSectionWidth(Border InterventionsSection)
		{
			Scroller.MaxWidth = InterventionsSection.ActualWidth - InterventionsLabel.ActualWidth - CreateInterventionButton.ActualWidth - 10;
		}

		private void CreateIntervention_Click(object sender, RoutedEventArgs e)
		{
            Intervention intervention = new Intervention(); 
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

            if (mi.Header.ToString().Equals(ETD.Properties.Resources.MenuItem_CompletedInterventions))
            {
                InterventionFilterLabel.Content = ETD.Properties.Resources.Label_InterventionFilterCompleted;
            }
            else if (mi.Header.ToString().Equals(ETD.Properties.Resources.MenuItem_OngoingInterventions))
            {
                InterventionFilterLabel.Content = ETD.Properties.Resources.Label_InterventionFilterOngoing;
            }
            else
            {
                InterventionFilterLabel.Content = mi.Tag;
            }

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

        public void Update()
        {
            InterventionsList.Children.Clear();

            foreach (Intervention intervention in Intervention.getActiveInterventionList())
            {
                Frame frame = new Frame();
                InterventionFormPage form = new InterventionFormPage(this, intervention);
                frame.Content = form;
                frame.Name = "Intervention_" + form.getInterventionNumber();
                frame.Tag = "Ongoing";
                if (!InterventionFilterLabel.Content.Equals(ETD.Properties.Resources.Label_InterventionFilterOngoing))
                {
                    frame.Visibility = Visibility.Collapsed;
                }
                InterventionsList.Children.Add(frame);
            }

            foreach (Intervention intervention in Intervention.getCompletedInterventionList())
            {
                Frame frame = new Frame();
                InterventionFormPage form = new InterventionFormPage(this, intervention);
                //form.CompleteIntervention(0);
                frame.Content = form;
                frame.Name = "Intervention_" + form.getInterventionNumber();
                frame.Tag = "Completed";
                if (!InterventionFilterLabel.Content.Equals(ETD.Properties.Resources.Label_InterventionFilterCompleted))
                {
                    frame.Visibility = Visibility.Collapsed;
                }
                InterventionsList.Children.Add(frame);
            }
        }
	}
}
