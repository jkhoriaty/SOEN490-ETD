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

namespace ETD.ViewsPresenters.InterventionsSection
{
	/// <summary>
	/// Interaction logic for InterventionSectionPage.xaml
	/// </summary>
	public partial class InterventionSectionPage : Page
	{
		private MainWindow mainWindow;
        List<String[]> addResource_Buffer = new List<String[]>();
        private DispatcherTimer addResource_DispatcherTimer;
        List<String[]> reportArrival_Buffer = new List<String[]>();
        private DispatcherTimer reportArrival_DispatcherTimer;

		public InterventionSectionPage(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;

            addResource_DispatcherTimer = new DispatcherTimer();
            addResource_DispatcherTimer.Tick += new EventHandler(AddResource_Refresh);
            addResource_DispatcherTimer.Interval = new TimeSpan(0, 0, 1); //Update every second

            reportArrival_DispatcherTimer = new DispatcherTimer();
            reportArrival_DispatcherTimer.Tick += new EventHandler(ReportArrival_Refresh);
            reportArrival_DispatcherTimer.Interval = new TimeSpan(0, 0, 1); //Update every second
		}

		//Adjusting the intervention section width
		public void setInterventionsSectionWidth(Border InterventionsSection)
		{
			Scroller.MaxWidth = InterventionsSection.ActualWidth - InterventionsLabel.ActualWidth - CreateInterventionButton.ActualWidth - 10;
		}

		private void CreateIntervention_Click(object sender, RoutedEventArgs e)
		{
            CreateIntervention();
		}

        internal void CreateIntervention()
        {
            Intervention intervention = new Intervention();

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

        /* Due to the fact that "frame.Content = object" is an asynchronous call, and causes a race condition that crashes the program when a team is
         * added to an intervention and set as arrived in the same program-flow as creating the intervention, the method call is made to be retried
         * every second until the frame.Content is actually set and it can access the correct InterventionForm
         * */
		internal void AddResource(String teamName, String interventionName)
		{
            addResource_Buffer.Add(new String[] { teamName, interventionName });
            addResource_DispatcherTimer.Start();
		}

        private void AddResource_Refresh(object sender, EventArgs e)
        {
            if(addResource_Buffer.Count == 0)
            {
                addResource_DispatcherTimer.Stop();
            }
            else
            {
                try
                {
                    getInterventionPage(addResource_Buffer[0][1]).AddResource(addResource_Buffer[0][0]);
                    addResource_Buffer.RemoveAt(0);
                }
                catch(Exception ex)
                {

                }
            }
        }

		internal void ReportArrival(String teamName, String interventionName)
		{
            reportArrival_Buffer.Add(new String[] { teamName, interventionName });
            reportArrival_DispatcherTimer.Start();
		}

        private void ReportArrival_Refresh(object sender, EventArgs e)
        {
            if (reportArrival_Buffer.Count == 0)
            {
                reportArrival_DispatcherTimer.Stop();
            }
            else
            {
                try
                {
                    getInterventionPage(reportArrival_Buffer[0][1]).ReportArrival(reportArrival_Buffer[0][0]);
                    reportArrival_Buffer.RemoveAt(0);
                }
                catch (Exception ex)
                {

                }
            }
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
