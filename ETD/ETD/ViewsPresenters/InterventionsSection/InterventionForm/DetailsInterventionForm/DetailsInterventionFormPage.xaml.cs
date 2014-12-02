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

namespace ETD.ViewsPresenters.InterventionsSection.InterventionForm.DetailsInterventionForm
{
	/// <summary>
	/// Interaction logic for DetailsInterventionFormPage.xaml
	/// </summary>
	public partial class DetailsInterventionFormPage : Page
	{
		private InterventionFormPage interventionForm;

		public DetailsInterventionFormPage(InterventionFormPage interventionForm)
		{
			InitializeComponent();
			this.interventionForm = interventionForm;
		}
	}
}
