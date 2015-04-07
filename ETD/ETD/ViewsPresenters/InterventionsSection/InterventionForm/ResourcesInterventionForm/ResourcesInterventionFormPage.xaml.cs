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
using ETD.Services;
using ETD.CustomObjects.CustomUIObjects;

namespace ETD.ViewsPresenters.InterventionsSection.InterventionForm.ResourcesInterventionForm
{
	/// <summary>
	/// Interaction logic for ResourcesInterventionForm.xaml
	/// </summary>
	public partial class ResourcesInterventionFormPage : Page
	{
		private InterventionFormPage interventionForm;
		private Intervention intervention;
		private Dictionary<String, TextBox[]> movingMap = new Dictionary<String, TextBox[]>();
		private Dictionary<String, TextBox[]> arrivalMap = new Dictionary<String, TextBox[]>();
		private Dictionary<String, TextBox[]> informationMap = new Dictionary<String, TextBox[]>();
		private List<Button> movingButtons = new List<Button>();
		private List<Button> arrivalButtons = new List<Button>();

		public ResourcesInterventionFormPage(InterventionFormPage interventionForm, Intervention intervention)
		{
			InitializeComponent();
			this.interventionForm = interventionForm;
			this.intervention = intervention;
            PopulateForm();
		}

		private void PopulateForm()
        {
			int position = 1;
			foreach (Resource resource in intervention.getResourceList())
            {
				//Creating line and adding the items to the main Grid
				ResourceLine resourceLine = new ResourceLine(resource, intervention);
				RowDefinition rowDefinition = new RowDefinition();
				rowDefinition.Height = new GridLength(25);
				Grid_Main.RowDefinitions.Add(rowDefinition);

				Grid_Main.Children.Add(resourceLine.getResourceNameTextBox());
				Grid.SetColumn(resourceLine.getResourceNameTextBox(), 0);
				Grid.SetRow(resourceLine.getResourceNameTextBox(), position);

				Grid_Main.Children.Add(resourceLine.getTeamNameLabel());
				Grid.SetColumn(resourceLine.getTeamNameLabel(), 1);
				Grid.SetRow(resourceLine.getTeamNameLabel(), position);

				Grid_Main.Children.Add(resourceLine.getMovingBorder());
				Grid.SetColumn(resourceLine.getMovingBorder(), 2);
				Grid.SetRow(resourceLine.getMovingBorder(), position);

				Grid_Main.Children.Add(resourceLine.getArrivalBorder());
				Grid.SetColumn(resourceLine.getArrivalBorder(), 3);
				Grid.SetRow(resourceLine.getArrivalBorder(), position);

				//Setting default text to the first resource textbox
				if (position == 1 && resourceLine.getResourceNameTextBox().Text.Equals(""))
				{
					resourceLine.getResourceNameTextBox().Text = "Prise en charge";
					resource.setResourceName("Prise en charge");
					intervention.ResourceModified();
				}

				position++;
            }
        }
	}
}
