﻿using System;
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
        private List<InterventionFormPage> pages;

		public InterventionSectionPage(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
            pages = new List<InterventionFormPage>();

            Observable.RegisterClassObserver(typeof(Intervention), this);
		}

		//Adjusting the intervention section width
		public void setInterventionsSectionWidth(Border InterventionsSection)
		{
			//If the items were not rendered yet, wait for the page to be loaded and then recall this method
			if (InterventionsLabel.ActualWidth == 0 && CreateInterventionButton.ActualWidth == 0)
			{
				Loaded += delegate { setInterventionsSectionWidth(InterventionsSection); };
				return;
			}
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
		//Mutators
		//Sets the intervention deadline
		public static void setInterventionDeadline(int deadline)
		{
			InterventionFormPage.setInterventionDeadline(deadline);
		}

		//Sets the moving deadline for an intervention
		public static void setMovingDeadline(int deadline)
		{
			InterventionFormPage.setMovingDeadline(deadline);
		}
	

		//Called when a field on the intervention page was modified
        public void Update()
        {
            foreach(InterventionFormPage form in pages)
            {
                form.DeregisterFormFromObserver();
            }
            pages.Clear();
            InterventionsList.Children.Clear();

            foreach (Intervention intervention in Intervention.getActiveInterventionList())
            {
                Frame frame = new Frame();
                InterventionFormPage form = new InterventionFormPage(this, intervention);
                pages.Add(form);
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
