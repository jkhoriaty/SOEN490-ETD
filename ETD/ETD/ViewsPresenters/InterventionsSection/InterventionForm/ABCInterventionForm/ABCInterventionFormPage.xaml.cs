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
using ETD.Models.Objects;

namespace ETD.ViewsPresenters.InterventionsSection.InterventionForm.ABCInterventionForm
{
	/// <summary>
	/// Interaction logic for ABCInterventionFormPage.xaml
	/// </summary>
	public partial class ABCInterventionFormPage : Page
	{
		private InterventionFormPage interventionForm;
		private Intervention intervention;

		public ABCInterventionFormPage(InterventionFormPage interventionForm, Intervention intervention)
		{
			InitializeComponent();
			this.interventionForm = interventionForm;
			this.intervention = intervention;
		}

		public void PersistencyUpdate()
		{
			String consciousness;
			if(ConsciousnessBox.SelectedIndex == -1)
			{
				consciousness = "notSet";
			}
			else
			{
				consciousness = ConsciousnessBox.Text;
			}
			bool disoriented = (bool)Disoriented.IsChecked;

			String airways;
			if(AirwaysBox.SelectedIndex == -1)
			{
				airways = "notSet";
			}
			else
			{
				airways = AirwaysBox.Text;
			}

			String breathing;
			if (BreathingBox.SelectedIndex == -1)
			{
				breathing = "notSet";
			}
			else
			{
				breathing = BreathingBox.Text;
			}
			int breathingFrequency = -1;
			try { breathingFrequency = int.Parse(BreathingFrequency.Text); }
			catch (Exception e) { }

			String circulation;
			if(CirculationBox.SelectedIndex == -1)
			{
				circulation = "notSet";
			}
			else
			{
				circulation = CirculationBox.Text;
			}
			int circulationFrequency = -1;
			try { circulationFrequency = int.Parse(CirculationFrequency.Text); }
			catch (Exception e) { }

			intervention.setABC(new ABC(consciousness, disoriented, airways, breathing, breathingFrequency, circulation, circulationFrequency));
		}
	}
}
