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

namespace ETD.ViewsPresenters.InterventionsSection.InterventionForm.AdditionalInfoInterventionForm
{
	/// <summary>
	/// Interaction logic for AdditionalInfoInterventionFormPage.xaml
	/// </summary>
	public partial class AdditionalInfoInterventionFormPage : Page
	{
		private InterventionFormPage interventionForm;

		public AdditionalInfoInterventionFormPage(InterventionFormPage interventionForm)
		{
			InitializeComponent();
			this.interventionForm = interventionForm;
		}
	}
}
