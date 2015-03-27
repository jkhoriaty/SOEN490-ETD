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
		private ABC abc;

		public ABCInterventionFormPage(InterventionFormPage interventionForm, ABC abc)
		{
			InitializeComponent();
			this.interventionForm = interventionForm;
            this.abc = abc;
            FillForm(this.abc);
		}

        public void FillForm(ABC abc)
        {
            String consciousness = abc.getConsciousness();
            switch (consciousness)
            {
                case "Alert": ConsciousnessBox.SelectedIndex = 0;
                    break;
                case "Verbal": ConsciousnessBox.SelectedIndex = 1;
                    break;
                case "Painful": ConsciousnessBox.SelectedIndex = 2;
                    break;
                case "Unconscious": ConsciousnessBox.SelectedIndex = 3;
                    break;
                default: ConsciousnessBox.SelectedIndex = -1;
                    break;
            }

            Disoriented.IsChecked = abc.getDisoriented();

            String airways = abc.getAirways();
            switch (airways)
            {
                case "Clear": AirwaysBox.SelectedIndex = 0;
                    break;
                case "Partially Obstructed": AirwaysBox.SelectedIndex = 1;
                    break;
                case "Completely Obstructed": AirwaysBox.SelectedIndex = 2;
                    break;
                default: AirwaysBox.SelectedIndex = -1;
                    break;
            }

            String breathing = abc.getBreathing();
            switch (breathing)
            {
                case "Normal": BreathingBox.SelectedIndex = 0;
                    break;
                case "Difficulty": BreathingBox.SelectedIndex = 1;
                    break;
                case "Absent": BreathingBox.SelectedIndex = 2;
                    break;
                default: BreathingBox.SelectedIndex = -1;
                    break;
            }

            BreathingFrequency.Text = (abc.getBreathingFrequency() >=  0) ? abc.getBreathingFrequency().ToString() : "";

            String circulation = abc.getCirculation();
            switch (circulation)
            {
                case "Normal": CirculationBox.SelectedIndex = 0;
                    break;
                case "Chest Pain": CirculationBox.SelectedIndex = 1;
                    break;
                case "Hemorrhage": CirculationBox.SelectedIndex = 2;
                    break;
                case "No Pulse": CirculationBox.SelectedIndex = 3;
                    break;
                default: CirculationBox.SelectedIndex = -1;
                    break;
            }
            CirculationFrequency.Text = (abc.getCirculationFrequency() >= 0) ? abc.getCirculationFrequency().ToString() : "";
        }

        //No longer needed
/*		public void PersistencyUpdate()
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

			//intervention.setABC(new ABC(consciousness, disoriented, airways, breathing, breathingFrequency, circulation, circulationFrequency));
            //FillForm(abc);
		}
		*/
        private void ConsciousnessBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String consciousness;
            ComboBox cb = (ComboBox)sender;
            if (cb.SelectedIndex == -1)
            {
                consciousness = "notSet";
            }
            else
            {
                consciousness = ((ComboBoxItem)e.AddedItems[0]).Content as string;
            }

            abc.setConsciousness(consciousness);
        }

        private void Disoriented_Checked(object sender, RoutedEventArgs e)
        {
            abc.setDisoriented((bool)((CheckBox)sender).IsChecked);
        }

        private void AirwaysBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String airways;
            ComboBox cb = (ComboBox)sender;
            if (cb.SelectedIndex == -1)
            {
                airways = "notSet";
            }
            else
            {
                airways = ((ComboBoxItem)e.AddedItems[0]).Content as string;
            }

            abc.setAirways(airways);
        }

        private void BreathingBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String breathing;
            ComboBox cb = (ComboBox)sender;
            if (cb.SelectedIndex == -1)
            {
                breathing = "notSet";
            }
            else
            {
                breathing = ((ComboBoxItem)e.AddedItems[0]).Content as string;
            }

            abc.setBreathing(breathing);
        }

        private void BreathingFrequency_LostFocus(object sender, RoutedEventArgs e)
        {
            int breathingFrequency = -1;
            try { breathingFrequency = int.Parse(((TextBox)sender).Text); }
            catch (Exception exc) { }
            abc.setBreathingFrequency(breathingFrequency);
        }

        private void CirculationBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String circulation;
            ComboBox cb = (ComboBox)sender;
            if (cb.SelectedIndex == -1)
            {
                circulation = "notSet";
            }
            else
            {
                circulation = ((ComboBoxItem)e.AddedItems[0]).Content as string;
            }

            abc.setCirculation(circulation);
        }

        private void CirculationFrequency_LostFocus(object sender, RoutedEventArgs e)
        {
            int circulationFrequency = -1;
            try { circulationFrequency = int.Parse(((TextBox)sender).Text); }
            catch (Exception exc) { }
            abc.setCirculationFrequency(circulationFrequency);
        }

        
	}
}
