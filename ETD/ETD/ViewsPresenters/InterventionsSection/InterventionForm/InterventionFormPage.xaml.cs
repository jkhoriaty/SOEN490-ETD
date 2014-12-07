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
using ETD.ViewsPresenters.InterventionsSection;
using ETD.ViewsPresenters.InterventionsSection.InterventionForm.TimersInterventionForm;
using ETD.ViewsPresenters.InterventionsSection.InterventionForm.DetailsInterventionForm;
using ETD.ViewsPresenters.InterventionsSection.InterventionForm.ResourcesInterventionForm;
using ETD.ViewsPresenters.InterventionsSection.InterventionForm.ABCInterventionForm;
using ETD.ViewsPresenters.InterventionsSection.InterventionForm.AdditionalInfoInterventionForm;
using ETD.ViewsPresenters.InterventionsSection.InterventionForm.EndInterventionForm;
using ETD.Models;

namespace ETD.ViewsPresenters.InterventionsSection.InterventionForm
{
    /// <summary>
    /// Interaction logic for InterventionForm.xaml
    /// </summary>
    public partial class InterventionFormPage : Page
    {
		private InterventionSectionPage interventionSection;
		private Intervention intervention;
		private TimersInterventionFormPage timersPage;
		private DetailsInterventionFormPage detailsPage;
		private ResourcesInterventionFormPage resourcesPage;
		private ABCInterventionFormPage abcPage;
		private AdditionalInfoInterventionFormPage additionalInfoPage;
		private EndInterventionFormPage endPage;

		public InterventionFormPage(InterventionSectionPage interventionSection, Intervention intervention)
        {
            InitializeComponent();
			this.interventionSection = interventionSection;

			this.intervention = intervention;
			setInterventionNumber(intervention.getInterventionNumber());

			timersPage = new TimersInterventionFormPage(this);
			detailsPage = new DetailsInterventionFormPage(this);
			resourcesPage = new ResourcesInterventionFormPage(this);
			abcPage = new ABCInterventionFormPage(this);
			additionalInfoPage = new AdditionalInfoInterventionFormPage(this);
			endPage = new EndInterventionFormPage(this);

			Frame timersFrame = new Frame();
			timersFrame.Content = timersPage;
			timers.Content = timersFrame;

			Frame detailsFrame = new Frame();
			detailsFrame.Content = detailsPage;
			details.Content = detailsFrame;

			Frame resourcesFrame = new Frame();
			resourcesFrame.Content = resourcesPage;
			resources.Content = resourcesFrame;

			Frame abcFrame = new Frame();
			abcFrame.Content = abcPage;
			abc.Content = abcFrame;

			Frame additionalInfoFrame = new Frame();
			additionalInfoFrame.Content = additionalInfoPage;
			additionalInfo.Content = additionalInfoFrame;

			Frame endFrame = new Frame();
			endFrame.Content = endPage;
			end.Content = endFrame;
		}

		public void setInterventionNumber(int interventionNum)
		{
			interventionNumber.Text = "" + interventionNum;
		}
    }
}