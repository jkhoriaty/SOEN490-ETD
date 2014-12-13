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
			Consciousness consciousness;
			if(ConsciousnessBox.SelectedIndex == -1)
			{
				consciousness = (Consciousness)4;
			}
			else
			{
				consciousness = (Consciousness)ConsciousnessBox.SelectedIndex;
			}
			bool disoriented = (bool)Disoriented.IsChecked;

			Airways airways;
			if(AirwaysBox.SelectedIndex == -1)
			{
				airways = (Airways)3;
			}
			else
			{
				airways = (Airways)AirwaysBox.SelectedIndex;
			}

			Breathing breathing;
			if (BreathingBox.SelectedIndex == -1)
			{
				breathing = (Breathing)3;
			}
			else
			{
				breathing = (Breathing)BreathingBox.SelectedIndex;
			}
			int breathingFrequency = -1;
			try { breathingFrequency = int.Parse(BreathingFrequency.Text); }
			catch (Exception e) { }

			Circulation circulation;
			if(CirculationBox.SelectedIndex == -1)
			{
				circulation = (Circulation)4;
			}
			else
			{
				circulation = (Circulation)CirculationBox.SelectedIndex;
			}
			int circulationFrequency = -1;
			try { circulationFrequency = int.Parse(CirculationFrequency.Text); }
			catch (Exception e) { }

			intervention.setABC(new Intervention.ABC(consciousness, disoriented, airways, breathing, breathingFrequency, circulation, circulationFrequency));
		}
	}
}
